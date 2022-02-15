using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public class DataFileRepository : IDataFileRepository
    {
        private int allowedPackageWeight = 0;

        private string thisFilePath;
        List<DataLine> thisDataLines = new List<DataLine>();

        public DataFileRepository(string filePath)
        {
            thisFilePath = filePath;
        }

        public string ReadRawFileContent()
        {
            return System.IO.File.ReadAllText(@"c:\temp\example_input.txt");           // TODO - // Call services to retrieve the file
        }

        public DataFile GetParsedFileContent()
        {
            bool fileParsedSuccessfully = GetAndParseFileContent();

            if (fileParsedSuccessfully)
            {
                return new DataFile()
                {
                    DataLines = thisDataLines,
                    FilePath = thisFilePath
                };
            }

            return new DataFile();                           // TODO - return error code
        }

        #region Private methods
        private bool GetAndParseFileContent()
        {
            bool fileParseSuccessfull = true;
            int lineCounter = 1;

            string[] dataLines = System.IO.File.ReadAllLines(@"c:\temp\example_input.txt");         // TODO - call service to retrieve file content

            try
            { 
                foreach (string line in dataLines)
                {
                    // Get the allowed package weight
                    allowedPackageWeight = GetAllowedPackageWeight(line);

                    // Get the Item(s)
                    var testCaseItems = GetListOfItemsInTestCase(line);

                    // Add the new line
                    thisDataLines.Add(new DataLine
                    {
                        LineNumber = lineCounter,
                        PackageWeight = allowedPackageWeight,
                        Items = testCaseItems
                    });

                    lineCounter++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fileParseSuccessfull;                // TODO - return breaking parse point details
        }

        private int GetAllowedPackageWeight(string dataLine)
        {
            int packageAllowedWeight = 0;

            int searchEndPosition = dataLine.IndexOf(':', 0);
            string textValue = dataLine.Substring(0, searchEndPosition - 0);
            var value = Int32.TryParse(textValue, out packageAllowedWeight);

            return packageAllowedWeight;
        }

        private List<DataItem> GetListOfItemsInTestCase(string dataLine)
        {
            int index = 0;
            decimal weight = 0;
            int cost = 0;                                    // Keep as Integer to conform to the current format in file, but consider to change to decimal (amount)

            var itemsInLine = new List<DataItem>();

            var itemsText = GetItemsInTestCase(dataLine);

            foreach (var item in itemsText)
            {
                DataItem newDataItem = new DataItem();

                // Get the different segments of the test case item
                //
                string[] itemList = item.Split(",");

                var x = itemList[0].AsSpan(1);                                          // Item index
                var y = itemList[1];                                                    // Item weight
                var z = itemList[2].AsSpan(1, itemList[2].Length - 2);                  // Item cost

                _ = Int32.TryParse(x, out index);
                _ = Decimal.TryParse(y, out weight);                                    // TODO - confirm culture will not cause side effects (cvdw - 15/2/2022)
                _ = Int32.TryParse(z, out cost);     

                DataItem i = new DataItem
                {
                    Index = index,
                    Weight = weight,
                    Cost = cost
                };
                itemsInLine.Add(i);
            }

            return itemsInLine;
        }

        private string[] GetItemsInTestCase(string dataLine)
        {
            // Get items in the text case
            int searchStartPosition = dataLine.IndexOf(':', 0) + 2;
            dataLine = dataLine.Substring(searchStartPosition, dataLine.Length - searchStartPosition);

            string[] itemList = dataLine.Split(" ");

            return itemList;
        }
        #endregion
    }
}
