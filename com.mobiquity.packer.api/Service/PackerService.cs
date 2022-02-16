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

            // Process the information 
            results = ProcessPackerData(data);

            // Write out the results in output file (todo)

            return results;
        }

        //public string ReadAndProcessPackerData(string filePath, IPackerRepository packerRepository)
        //{
        //    //_packerRepositoryService = packerRepository;

        //    return filePath;
        //}


        private string ProcessPackerData(DataFile dataFileContent)
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
                                      orderby item.Cost descending                       // Looking for items costing the most
                                      select item).Take(15);                             // Considering only up to 15 items



                var test = filteringQuery.ToList();


                // Loop through all the items in the test case and keep
                // including items in the package while the total weight for the package is less than 100 (constrain)
                // and not greater than the toal weight for this packaged allowd
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
                        itemsSelectedForLine += "," + candidate.Index + " ";

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

            // Do final selection - if package with same weight - use one with smallest cost 
            var finalFilteringQuery = (from x in selectedItems
                                       orderby x.TotalCost
                                       select x);


            // Prepare and return the final results
            foreach (var x in selectedItems) { results += x.Items + " | "; };
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
