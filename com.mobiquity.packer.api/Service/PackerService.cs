using com.mobiquity.packer.data;
using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;
using System.Collections;

namespace com.mobiquity.packer.api
{
    public class PackerService : IPackerService
    {
        //private IPackerRepository _packerRepositoryService;         // Not read-only

        public string ReadAndProcessPackerData(string filePath)
        {
            string results = string.Empty;

            var packerRepository = new PackerRepository(filePath);                  //  need to change to di

            // Get the data file content
            var data = packerRepository.GetParsedFileContent();

            // Process the information and generate the output
            results = ProcessPackerDataAsync(data).Result;                          // todo - check this first...

            return results;
        }

        //public string ReadAndProcessPackerData(string filePath, IPackerRepository packerRepository)
        //{
        //    //_packerRepositoryService = packerRepository;

        //    return filePath;
        //}


        private async Task<string> ProcessPackerDataAsync(DataFile dataFileContent)
        {
            string results = string.Empty;

            List<SelectedItem> selectedItems = new List<SelectedItem>();
            string itemsSelectedForLine = string.Empty;
            decimal totalWeightForItemsSelected = 0;
            int totalCostForItemsSelected = 0;

            foreach (var line in dataFileContent.DataLines)
            {
                #region Initialize for new data line
                //selectedItems = new List<SelectedItem>();
                itemsSelectedForLine = string.Empty;
                totalWeightForItemsSelected = 0;
                totalCostForItemsSelected = 0;
                #endregion

                // Filter the items in the line for potential candidates
                //
                var filteringQuery = (from item in line.Items
                                      where item.Weight <= Constrains.MAX_ITEM_COST      // Exclude items weighting more than allowed weight  
                                             && item.Cost <= Constrains.MAX_ITEM_COST    // Exclude items costing more than allowed cost 
                                             && item.Weight <= line.PackageWeight        // Excluding items weigthing more than the allowed package weight
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
                    // Check total weight for the package already selected
                    decimal checkWeight = totalWeightForItemsSelected + candidate.Weight;
                    if (checkWeight > Constrains.MAX_PACKAGE_WEIGHT || checkWeight > line.PackageWeight) break;

                    // Add this item to the package list
                    //
                    if (itemsSelectedForLine == "")
                        itemsSelectedForLine += candidate.Index + " ";
                    else
                    {
                        itemsSelectedForLine = itemsSelectedForLine.Remove(itemsSelectedForLine.Length - 1, 1);     // Remove last space
                        itemsSelectedForLine += "," + candidate.Index + " ";                                        // Add new Item index
                    }

                    totalWeightForItemsSelected += candidate.Weight;
                    totalCostForItemsSelected += candidate.Cost;
                }

                selectedItems.Add(new SelectedItem
                {
                    Items = String.IsNullOrEmpty(itemsSelectedForLine) ? "-" : itemsSelectedForLine,
                    TotalWeight = totalWeightForItemsSelected,
                    TotalCost = totalCostForItemsSelected
                });
            }


            // Save results to the text file
            List<string> output = new List<string>();
            foreach (var line in selectedItems)
            {
                output.Add(line.Items);
            };
            await DataService.WritePackerOutputFile(output);                // Write out the output Async

            // Return the final results to calling clients
            foreach (var line in selectedItems) { results += line.Items + System.Environment.NewLine;};
            return results;
        }


        private class SelectedItem : IEnumerable
        {
            public string Items { get; set; }
            public decimal TotalWeight { get; set; }
            public int TotalCost { get; set; }

            List<SelectedItem> mylist = new List<SelectedItem>();

            public SelectedItem this[int index]
            {
                get { return mylist[index]; }
                set { mylist.Insert(index, value); }
            }

            public IEnumerator<SelectedItem> GetEnumerator()
            {
                return mylist.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
