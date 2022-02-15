using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;
using Xunit;

namespace com.mobiquity.packer.test
{
    public class DataFileTests
    {
        string dataFilePath = "";

        [Fact]
        public void DataFileIsNotEmpty()
        {
            // Arrange
            string dataFileContent = new MockDataFileRepository(dataFilePath).ReadRawFileContent();         // Mock

            // Act
            bool hasContent = dataFileContent.Length > 0;

            // Assert
            Assert.True(hasContent, "The datafile needs to contains data and cannot be empty");
        }


        [Fact]
        public void DataFileNeedsAtLeastOneLine()
        {
            // Arrange
            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();
            DataLine firstDataLine = dataFileContent.DataLines[0];

            // Act
            bool hasItems = firstDataLine.Items.Count > 0;

            // Assert
            Assert.True(hasItems, "The datafile needs to contains at least one test case/line");
        }

        [Fact]
        public void DataFileParseSuccessfully()
        {
            // Arrange                      
            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();      //  Parse file and confirm all data structures were successfully created                 
            bool parseSuccessfully = true;
            int lineNumber = 1;

            // Act
            parseSuccessfully = dataFileContent.DataLines.Count > 0;                                    // Confirm there is at least one line

            if (parseSuccessfully)
            {
                // Loop through all available lines and check each line data
                foreach (var thisLine in dataFileContent.DataLines)
                {

                    // Confirm the package weight exists
                    if (thisLine.PackageWeight <= 0) { parseSuccessfully = false; break; }

                    if (thisLine.Items.Count == 0) { parseSuccessfully = false; break; }

                    // Confirm all the item information exist
                    foreach (var thisItem in thisLine.Items)
                    {
                        if (thisItem.Index <= 0) { parseSuccessfully = false; break; }
                        if (thisItem.Weight <= 0) { parseSuccessfully = false; break; }
                        if (thisItem.Cost <= 0) { parseSuccessfully = false; break; }
                    }
                    lineNumber++;
                }
            }

            bool hasParseSuccesfully = parseSuccessfully;

            // Assert
            Assert.True(hasParseSuccesfully, $"The datafile needs to contains well formatted data to parse succesfully. The line containing the invalid data is in line# {lineNumber}");
        }


    }
}
