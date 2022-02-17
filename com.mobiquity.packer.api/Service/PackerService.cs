using com.mobiquity.packer.data;
using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;
using System.Collections;

namespace com.mobiquity.packer.Packer
{
    public class PackerService : IPackerService
    {
        public string ReadAndProcessPackerData(string filePath, string unitTestDataLine = null)
        {
            string results = string.Empty;
            DataFile data = new DataFile();
            string[] testLineDirectly = new string[1];

            // Get the data file content  
            var packerRepository = new PackerRepository(filePath);                                              //  need to change to di

            if (unitTestDataLine != null) testLineDirectly[0] = unitTestDataLine;    // For unit testing allow this method to receive directly an input dataline   
            data = packerRepository.GetParsedFileContent(testLineDirectly);

            // Confirm that the data was available
            if (data != null)
            {
                // Process the information and generate the output
                results = ProcessPackerDataAsync(data).Result;                                                  // todo - for now force sync
            }
            else
            {
                // Write out exeption to file (todo)

                throw new Exception("The Packer input file was not available for processing");
            }

            return results;
        }

        private async Task<string> ProcessPackerDataAsync(DataFile dataFileContent)
        {
            string results = string.Empty;

            List<SelectedItem> selectedItems = new List<SelectedItem>();

            string itemsSelectedForLine = string.Empty;
            decimal totalWeightForItemsSelected = 0;
            int totalCostForItemsSelected = 0;
            List<int> prizesForItemsForLineAlreadySelected = new List<int>();

            foreach (var line in dataFileContent.DataLines)
            {
                #region Initialize for new data line
                itemsSelectedForLine = string.Empty;
                totalWeightForItemsSelected = 0;
                totalCostForItemsSelected = 0;
                prizesForItemsForLineAlreadySelected = new List<int>();
                #endregion

                // Filter the items in the line for potential candidates
                //
                var filteringQuery = (from item in line.Items
                                      where item.Weight <= Constrains.MAX_ITEM_COST      // Exclude items weighting more than allowed weight  
                                             && item.Cost <= Constrains.MAX_ITEM_COST    // Exclude items costing more than allowed cost 
                                             && line.PackageWeight >= item.Weight        // Excluding items weigthing more than the allowed package weight
                                      orderby item.Cost descending,                      // Looking for items costing the most
                                              item.Weight                                // then looking for packages that weight the less first    
                                      select item).Take(15);                             // Considering only up to 15 items



                var test = filteringQuery.ToList();         // testing


                // Loop through all the items in the test case and keep including items in the package
                // while the total weight for the package is less than 100 (constrain)
                // and the item's weight is not greater than the total weight allowed for this package
                //
                foreach (var candidate in filteringQuery)
                {
                    // Check if we have not already selected a simular item with the same cost/prize
                    //
                    bool costAlreadyIncluded = false;
                    foreach (var i in prizesForItemsForLineAlreadySelected)
                    { 
                        if (candidate.Cost == i)
                        {
                            costAlreadyIncluded = true;
                            break;
                            }
                    }
                    if (!costAlreadyIncluded)
                    {
                        // Check total weight for the package already selected and first confirm package is not over-weighted
                        decimal checkWeight = totalWeightForItemsSelected + candidate.Weight;
                        if (checkWeight > Constrains.MAX_PACKAGE_WEIGHT || checkWeight > line.PackageWeight) 
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
                        prizesForItemsForLineAlreadySelected.Add(candidate.Cost);
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


            // Save results to the text file
            List<string> output = new List<string>();
            foreach (var line in selectedItems)
            {
                output.Add(line.ItemsListToDisplay);
            };
            await DataService.WritePackerOutputFile(output);                // Write out the output file (Async)

            // Return the final results to calling clients
            foreach (var line in selectedItems) { results += line.ItemsListToDisplay + System.Environment.NewLine;};      
            return results;
        }

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
