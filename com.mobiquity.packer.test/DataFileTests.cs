using Xunit;

namespace com.mobiquity.packer.test
{
    public class DataFileTests
    {
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
            // TODO - Call service to parse file and confirm all data structures were successfully created
            bool parseDataFile = false;

            // Act
            bool hasParseSuccesfully = parseDataFile;


            // Assert
            Assert.True(hasParseSuccesfully, "The datafile needs to contains well formatted data to parse succesfully");

            // Assert
        }
    }
}
