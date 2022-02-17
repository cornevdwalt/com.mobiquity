using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;
using Xunit;

namespace com.mobiquity.packer.test
{
    public class DataFileTests
    {
        private const string dataFilePath = Constants.PACKER_TEST_FILE_PATH;                        // Use the test/mock file for testing
        private DataFile dataFileContent = new DataFile();

        [Fact]
        public void DataFileIsNotEmpty()
        {
            // Arrange
            string dataFileContent = GetPackFileRawData();

            // Act
            bool hasContent = dataFileContent.Length > 0;

            // Assert
            Assert.True(hasContent, "The datafile needs to contains data and cannot be empty");
        }

        [Fact]
        public void DataFileNeedsAtLeastOneLine()
        {
            // Arrange
            var dataFileContent = GetPackFileData();
            DataLine firstDataLine = dataFileContent.DataLines[0];

            // Act
            bool hasItems = firstDataLine.Items.Count > 0;

            // Assert
            Assert.True(hasItems, "The datafile needs to contains at least one test case/line");
        }

        [Fact]
        public void DataFileParseAllLinesSuccessfully()
        {
            // Arrange                      
            var dataFileContent = new MockPackerRepository(dataFilePath).GetParsedFileContent_OLD();
            int lineNumber = 1;
            bool parseSuccessfully = true;

            // Act
            parseSuccessfully = dataFileContent.DataLines.Count > 0;                                    // Confirm there is at least one line
            Assert.True(parseSuccessfully, $"The datafile needs to contains well formatted data to parse succesfully and need at least one test case/line.");

            if (parseSuccessfully)
            {
                // Loop through all available lines and check each line data
                foreach (var thisLine in dataFileContent.DataLines)
                {

                    // Confirm the package weight exists
                    if (thisLine.PackageWeight <= 0) { parseSuccessfully = false; }
                    Assert.True(parseSuccessfully, $"The datafile needs to contains well formatted data to parse succesfully and need a valid package weight. The line containing the invalid data is in line# {lineNumber}");

                    if (thisLine.Items.Count == 0) { parseSuccessfully = false; }
                    Assert.True(parseSuccessfully, $"The datafile needs to contains well formatted data to parse succesfully and need at least one item in the test case. The line containing the invalid data is in line# {lineNumber}");

                    // Confirm all the item information exist
                    foreach (var thisItem in thisLine.Items)
                    {
                        if (thisItem.Index <= 0) { parseSuccessfully = false; }
                        Assert.True(parseSuccessfully, $"The datafile needs to contains well formatted data to parse succesfully and the item needs an index in the test case. The line containing the invalid data is in line# {lineNumber}");

                        if (thisItem.Weight <= 0) { parseSuccessfully = false; }
                        Assert.True(parseSuccessfully, $"The datafile needs to contains well formatted data to parse succesfully and the item needs a weight in the test case. The line containing the invalid data is in line# {lineNumber}");

                        if (thisItem.Cost <= 0) { parseSuccessfully = false; }
                        Assert.True(parseSuccessfully, $"The datafile needs to contains well formatted data to parse succesfully and the needs a cost in the test case. The line containing the invalid data is in line# {lineNumber}");
                    }
                    lineNumber++;
                }
            }

            bool hasParseSuccesfully = parseSuccessfully;

            // Assert
            Assert.True(hasParseSuccesfully, $"The datafile needs to contains well formatted data to parse succesfully and cannot be processed at this stage.");
        }

        #region Private
        private static DataFile GetPackFileData(bool useMockData = false)
        {
            if (useMockData)
            {
                // Return Mock repository data
                return new MockPackerRepository(dataFilePath).GetParsedFileContent_OLD();
            }
            else
            {
                // Return repository data
                return new PackerRepository(dataFilePath).GetParsedFileContent();
            }
        }

        private static string GetPackFileRawData(bool useMockData = false)
        {
            if (useMockData)
            {
                // Return Mock repository data
                return new MockPackerRepository(dataFilePath).ReadRawFileContent();
            }
            else
            {
                // Return repository data
                return new PackerRepository(dataFilePath).ReadRawFileContent();
            }
        } 
        #endregion
    }
}
