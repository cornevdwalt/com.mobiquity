using com.mobiquity.packer.domain;
using com.mobiquity.packer.Packer;
using com.mobiquity.packer.repository;
using Xunit;

namespace com.mobiquity.packer.test
{
    public class DataLineTests
    {
        private const string dataFilePath = Constants.PACKER_TEST_FILE_PATH;                        // Use the test/mock file for testing
        private const int allowedPackageWeight = 100;

        private int lineNumber = 0;
        private DataFile dataFileContent = new DataFile();

        [Fact]
        public void DataLineTotalWeightGreaterThanZero()
        {
            // Arrange
            bool testPassed = false;
            var dataFileContent = GetPackFileData();

            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                // Act
                string testResult = PackerLineValidator.DataLineTotalWeightGreaterThanZero(thisLine);

                testPassed = testResult == string.Empty;

                Assert.True(testPassed, $"The total weight of a package allowed must be greater than zero. Line# {lineNumber}. Error code return = {testResult}");

                lineNumber++;
            }
        }

        [Fact]
        public void DataLineHasAtLeastOneItem()
        {
            // Arrange
            bool testPassed = false;
            var dataFileContent = GetPackFileData();

            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                // Act
                string testResult = PackerLineValidator.DataLineHasAtLeastOneItem(thisLine);

                testPassed = testResult == string.Empty;

                Assert.True(testPassed, $"There must be at least one item in a package. Line# {lineNumber}. Error code return = {testResult}");

                lineNumber++;
            }
        }

        [Fact]
        public void LineItemsNotValid()
        {
            // Arrange
            bool testPassed = false;
            var dataFileContent = GetPackFileData();

            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                if (thisLine.Items != null)
                {
                    // Confirm all the items in the test case are valid 
                    //
                    foreach (var thisItem in thisLine.Items)
                    {
                        // Act
                        string testResult = PackerLineValidator.LineItemsValidValues(thisLine);

                        testPassed = testResult == string.Empty;

                        Assert.True(testPassed, $"The datafile and items needs to contains well formatted data to parse succesfully for each test case. Line# {lineNumber}. Error code return = {testResult}");
                    }
                }
            }
        }

        private static DataFile GetPackFileData(bool useMockData = false)
        {
            if (useMockData)
            {
                // Return Mock repository data
                return new MockPackerRepository(dataFilePath).GetParsedFileContent();
            }
            else
            {
                // Return repository data
                return new PackerRepository(dataFilePath).GetParsedFileContent();
            }
        }
    }
}