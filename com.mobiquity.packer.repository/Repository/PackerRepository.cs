using com.mobiquity.packer.data;
using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public class PackerRepository : IPackerRepository
    {
        private int allowedPackageWeight = 0;

        private string thisFilePath;
        List<DataLine> thisDataLines = new List<DataLine>();

        public PackerRepository(string filePath)
        {
            thisFilePath = filePath;
        }

        public DataFile GetParsedFileContent(string[]? unitTestFileContent = null)
        {
            bool fileParseSuccessfull = true;
            string[] fileLines = unitTestFileContent;
            int lineCounter = 1;

            // To allow unit testing allow this method to receive directly a string
            // for testing purchases
            //
            if (unitTestFileContent == null || unitTestFileContent[0] == null)
                fileLines = GetFileContentDataLines();                                      // Get datalines from the input file
            else
                fileLines = unitTestFileContent;                                            // Use dataline pass into from unit test


            // Validate the content of the file (confirming there is at least one line)
            var validateFileCheckValue = PackerFileValidator.DataFileNeedsAtLeastOneLine(fileLines);
            fileParseSuccessfull = validateFileCheckValue == 0;

            if(fileParseSuccessfull)
            {
                foreach (var line in fileLines)
                {
                    // Get the allowed package weight
                    allowedPackageWeight = GetAllowedPackageWeight(line);

                    // Get the Item(s) for the test case (line)
                    var testCaseItems = GetAndParseItemsInTestCase(line);

                    if (testCaseItems.Count > 0)
                    {
                        // Add the new line
                        thisDataLines.Add(new DataLine
                        {
                            LineNumber = lineCounter,
                            PackageWeight = allowedPackageWeight,
                            Items = testCaseItems
                        });

                        lineCounter++;
                    }
                    else
                    {
                        fileParseSuccessfull = false;
                        break;                                          
                    }
                }
            }

            if (fileParseSuccessfull)
            {
                // Data line parsed succesfully and can be addedd 
                return new DataFile()
                {
                    DataLines = thisDataLines,
                    FilePath = thisFilePath
                };
            }
            else
            {
                // Add the new line containing the error code
                thisDataLines.Add(new DataLine
                {
                    LineNumber = validateFileCheckValue,                        // Line number contains the error code
                });

                return new DataFile() 
                { 
                    FilePath = thisFilePath,
                    DataLines = thisDataLines,
                };
            }
        }

        public string ReadRawFileContent()
        {
            return DataService.ReadAllRawDataInFile(thisFilePath);
        }

        #region Private methods
        private string[] GetFileContentDataLines()
        {
            return DataService.RetrieveDataFileContent(thisFilePath);     // Retrieve the content of the data file as separate lines
        }

        private bool GetAndParseCompleteFileContent()
        {
            bool fileParseSuccessfull = true;
            int lineCounter = 1;

            string[] dataLines = DataService.RetrieveDataFileContent(thisFilePath);     // Retrieve the content of the data file as separate lines

            foreach (string line in dataLines)
            {
                // Get the allowed package weight
                allowedPackageWeight = GetAllowedPackageWeight(line);

                // Get the Item(s)
                var testCaseItems = GetAndParseItemsInTestCase(line);

                // Add the new line
                thisDataLines.Add(new DataLine
                {
                    LineNumber = lineCounter,
                    PackageWeight = allowedPackageWeight,
                    Items = testCaseItems
                });

                lineCounter++;
            }

            return fileParseSuccessfull;                // TODO - return breaking parse point details
        }

        private int GetAllowedPackageWeight(string dataLine)
        {
            int packageAllowedWeight = 0;

            try
            {
                int searchEndPosition = dataLine.IndexOf(':', 0);
                string textValue = dataLine.Substring(0, searchEndPosition - 0);
                var value = Int32.TryParse(textValue, out packageAllowedWeight);

                return packageAllowedWeight;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        private List<DataItem> GetAndParseItemsInTestCase(string dataLine)
        {
            int index = 0;
            decimal weight = 0;
            int cost = 0;                                    // Keep as Integer to conform to the current format in file, but consider to change to decimal (amount)

            var itemsInLine = new List<DataItem>();

            var itemsText = GetItemsInTestCase(dataLine);

            foreach (var item in itemsText)
            {
                DataItem newDataItem = new DataItem();

                if (!string.IsNullOrEmpty(item))
                {
                    // Get the different segments of the test case item
                    //
                    string[] itemList = item.Split(",");

                    var x = itemList[0].AsSpan(1);                                          // Item index
                    var y = itemList[1];                                                    // Item weight
                    var z = itemList[2].AsSpan(1, itemList[2].Length - 2);                  // Item cost

                    _ = Int32.TryParse(x, out index);
                    _ = Decimal.TryParse(y, out weight);                                    // TODO - confirm culture will not cause side effects (cvdw - 15/2/2022)
                    _ = Int32.TryParse(z, out cost);
                }

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
            try
            {
                // Get items in the text case
                int searchStartPosition = dataLine.IndexOf(':', 0) + 2;
                dataLine = dataLine.Substring(searchStartPosition, dataLine.Length - searchStartPosition);

                string[] itemList = dataLine.Split(" ");

                return itemList;
            }
            catch (Exception)
            {
                string[] itemList = new string[0];
                return itemList;
            }
        }
        #endregion
    }
}
