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
            // TODO - call service to retrieve the content of the data file
            string dataFileContent = string.Empty;


            // Act
            bool hasContent = dataFileContent.Length > 0;


            // Assert
            Assert.True(hasContent, "The datafile needs to contains information");
        }

        [Fact]
        public void DataFileParseSuccessfully()
        {
            // Arrange                      
            DataFile dataFile = new DataFile(dataFilePath);                     // TODO - Call service to parse file and confirm all data structures were successfully created
            var dataLines = dataFile.DataLines;
            bool parseDataFile = false;

            // Act
            bool hasParseSuccesfully = parseDataFile;

            // Assert
            Assert.True(hasParseSuccesfully, "The datafile needs to contains well formatted data to parse succesfully");

            // Assert
        }

        [Fact]
        public void DataFileNeedsAtLeastOneLine()
        {
            // Arrange
            string dataFileContent = string.Empty;                              // TODO - call service to retrieve the content of the data file

            DataFile dataFile = new DataFile(dataFilePath);                     // TODO - call service to parse the content of the data file
            var dataLines = dataFile.DataLines;

            // Act
            bool hasItems = dataLines.Count > 0;

            // Assert
            Assert.True(hasItems, "The datafile needs to contains at least one test case/line");
        }
    }
}
