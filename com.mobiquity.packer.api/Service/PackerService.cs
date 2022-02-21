using com.mobiquity.packer.data;
using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;
using System.Collections;

namespace com.mobiquity.packer.Packer
{
    public class PackerService : IPackerService
    {
        /// <summary>
        /// Use either a path to a text file to be read and parse or send a text line directly
        /// to be parsed into the data structures and process the data accordingly
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="unitTestDataLine"></param>
        /// <returns></returns>
        public string ReadAndProcessPackerData(string filePath, string unitTestDataLine = null)
        {
            string results = string.Empty;
            int lineNumber = 1;
            DataFile dataFileContent = new DataFile();

            bool fileParsedSuccessfully = true;
            string filePassValidationMsg = String.Empty;

            // For unit testing allow this method to receive a dataline  
            // directly as an input parameter, otherwise retrieve file content from file
            if (unitTestDataLine != null)
            {
                // Parse the unit test data
                string[] testLineDirectly = new string[1];
                testLineDirectly[0] = unitTestDataLine;
                dataFileContent = new PackerRepository(filePath).GetParsedFileContent(testLineDirectly);
            }
            else
            {
                try
                {
                    // Try to get and parse the data file content  
                    dataFileContent = new PackerRepository(filePath).GetParsedFileContent();
                }
                catch (Exception)
                {
                    filePassValidationMsg = "<error " + PACKERFILE_VALIDATION_CODES.DataFileCouldNotBeFound + "> ";
                    fileParsedSuccessfully = false;
                }
            }

            // Test if the file content valid
            //
            int dataFileValidationResults = PackerFileValidator.DataFileNeedsAtLeastOneLine(dataFileContent);
            if (dataFileValidationResults != 0)
            {
                filePassValidationMsg += "<error " + dataFileValidationResults +"> ";
                fileParsedSuccessfully = false;
            }

            // If file content passed validation process each line in the file
            if (fileParsedSuccessfully)
            {
                // Validate and process each data line 
                //
                foreach (var thisLine in dataFileContent.DataLines)
                {
                    results += ProcessPackerDataLine(thisLine, lineNumber);
                    lineNumber++;
                }
            }
            else
                results = filePassValidationMsg;

            // If the file did not pass validation raise an exception to notify the calling application
            if (!fileParsedSuccessfully) 
                throw new Exception("File error: " + filePassValidationMsg);

            return results;
        }

        /// <summary>
        /// Process each dataline in a data input file and determine the correct items to be added into the package
        /// to be send
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string ProcessPackerDataLine(DataLine line, int lineNumber)
        {
            string results = string.Empty;

            List<SelectedItem> selectedItems = new List<SelectedItem>();
            string itemsSelectedForLine = string.Empty;
            decimal totalWeightForItemsSelected = 0;
            int totalCostForItemsSelected = 0;
            List<int> pricesForItemsForLineAlreadySelected = new List<int>();

            // Validate and parse each line in the input file
            //
            #region Validate the data line (business rules)
            string validateLineItemResults = string.Empty;
            bool lineValidatedSuccessfully = true;

            validateLineItemResults = PackerLineValidator.ValidatePackerDataLine(line, lineNumber);
            if (validateLineItemResults != string.Empty)
            {
                selectedItems.Add(new SelectedItem
                {
                    ItemsListToDisplay = validateLineItemResults,                       // Show the error codes in the output 
                });
                lineValidatedSuccessfully = false;
            }
            #endregion

            if (lineValidatedSuccessfully)
            {
                #region Initialization for new data line
                itemsSelectedForLine = string.Empty;
                totalWeightForItemsSelected = 0;
                totalCostForItemsSelected = 0;
                pricesForItemsForLineAlreadySelected = new List<int>();
                #endregion

                // ----------------------------------------------------------------
                // Business rules for picking items for the package
                //
                // Only up to 15 items
                // and until total wight of parcel = 100
                //
                // Filer out any items cost/weight > 100
                // ----------------------------------------------------------------

                // Filter the items in the line for potential candidates
                //
                var filteringQuery = (from item in line.Items
                                      where item.Weight <= Constrain.MAX_ITEM_COST      // Exclude items weighting more than allowed weight  
                                             && item.Cost <= Constrain.MAX_ITEM_COST    // Exclude items costing more than allowed cost 
                                             && line.PackageWeight >= item.Weight        // Excluding items weigthing more than the allowed package weight

                                      orderby item.Cost descending,                      // Looking for items costing the most
                                              item.Weight                                // then looking for items that weight less first
                                                                                         // 
                                      select item).Take(15);                             // Considering only up to 15 items



                var test = filteringQuery.ToList();         // for testing only (cvdw)


                // Loop through all the items in the test case and keep including items in the package
                // while the total weight for the package is less than 100 (constrain)
                // and the item's weight is not greater than the total weight allowed for this package
                //
                foreach (var candidate in filteringQuery)
                {
                    // Check if we have not already selected a simular item with the same cost/prize
                    //
                    bool costAlreadyIncluded = false;
                    foreach (var i in pricesForItemsForLineAlreadySelected)
                    {
                        if (candidate.Cost == i)
                        {
                            costAlreadyIncluded = true;
                            break;
                        }
                    }
                    if (!costAlreadyIncluded)
                    {
                        // Check that the total weight for the package is not over-weighted or more than allowed for this package
                        decimal checkWeight = totalWeightForItemsSelected + candidate.Weight;
                        if (checkWeight > Constrain.MAX_PACKAGE_WEIGHT || checkWeight > line.PackageWeight)
                            break;

                        #region Add this item to the items for this line
                        if (itemsSelectedForLine == "")
                            itemsSelectedForLine += candidate.Index + " ";
                        else
                        {
                            itemsSelectedForLine = itemsSelectedForLine.Remove(itemsSelectedForLine.Length - 1, 1);     // Remove last space
                            itemsSelectedForLine += "," + candidate.Index + " ";                                        // Add new Item index
                        }

                        totalWeightForItemsSelected += candidate.Weight;
                        totalCostForItemsSelected += candidate.Cost;
                        pricesForItemsForLineAlreadySelected.Add(candidate.Cost);
                        #endregion
                    }
                }

                // Add the item to the test case(s) for the final results
                selectedItems.Add(new SelectedItem
                {
                    ItemsListToDisplay = String.IsNullOrEmpty(itemsSelectedForLine) ? "-" : itemsSelectedForLine,
                    TotalWeight = totalWeightForItemsSelected,
                    TotalCost = totalCostForItemsSelected,
                });
            }

            // Save results to the text file and append the results to the output file  
            //
            List<string> output = new List<string>();
            foreach (var i in selectedItems) { output.Add(i.ItemsListToDisplay); };
            DataService.WritePackerOutputFile(output);

            // Return the final results to calling clients
            foreach (var i in selectedItems) { results += i.ItemsListToDisplay + System.Environment.NewLine; };
            return results;
        }

        /// <summary>
        /// Private structure to parse and track the selected item in a test case (line)
        /// </summary>
        private class SelectedItem : IEnumerable
        {
            public string ItemsListToDisplay { get; set; }
            public decimal TotalWeight { get; set; }
            public int TotalCost { get; set; }
            public List<decimal>? SelectedItemIndexes { get; set; }

            List<SelectedItem> itemList = new List<SelectedItem>();

            public SelectedItem this[int index]
            {
                get { return itemList[index]; }
                set { itemList.Insert(index, value); }
            }

            public IEnumerator<SelectedItem> GetEnumerator()
            {
                return itemList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
