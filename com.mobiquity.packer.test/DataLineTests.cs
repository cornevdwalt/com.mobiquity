using com.mobiquity.packer.repository;
using Xunit;

namespace com.mobiquity.packer.test
{
    public class DataLineTests
    {
        private const string dataFilePath = "";
        private const int allowedPackageWeight = 100;
        private const int allowedItemWeight = 100;
        private const int allowedItemCost = 100;

        private int lineNumber = 0;

        [Fact]
        public void DataLineTotalWeightGreaterThanZero()
        {
            bool totalPackageWeightGreaterThanZero = false;

            // Arrange                      
            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();

            // Act
            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                // Confirm the package weight exists
                totalPackageWeightGreaterThanZero = thisLine.PackageWeight > 0;

                // Assert
                Assert.True(totalPackageWeightGreaterThanZero, $"The total weight of a package allowed must be greater than zero. Line# {lineNumber}");

                lineNumber++;
            }
        }

        [Fact]
        public void DataLineHasAtLeastOneItem()
        {
            // Arrange 
            bool atLeastOneItemInLine = false;

            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();

            // Act
            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                atLeastOneItemInLine = thisLine.Items.Count > 0;

                // Assert
                Assert.True(atLeastOneItemInLine, $"There must be at least one item in a package. Line# {lineNumber}");

                lineNumber++;
            }
        }

        [Fact]
        public void NumberOfItemsinDataLineLessEqualTo15()
        {
            //
            // TODO - Confirm this is a constrain (cvdw) - 15/2/2022
            //

            // Arrange 
            bool itemsInRange = false;

            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();

            // Act
            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                itemsInRange = thisLine.Items.Count <= 15;

                // Assert
                Assert.True(itemsInRange, $"There cannot be more than 15 items in a single package. Line# {lineNumber}");

                lineNumber++;
            }
        }

        [Fact]
        public void TotalPackageWeightLessEqualTo100()
        {
            // Arrange 
            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();

            // Act
            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                // Assert
                Assert.True(thisLine.PackageWeight <= allowedPackageWeight, $"The total weight of a package may not be more then {allowedPackageWeight}. Line# {lineNumber}");

                lineNumber++;
            }
        }

        [Fact]
        public void ItemWeightAndCostLessEqualTo100()
        {
            // Arrange 

            var dataFileContent = new MockDataFileRepository(dataFilePath).GetParsedFileContent();

            // Act
            foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
            {
                // Confirm all the items in the test case are valid 
                //
                foreach (var thisItem in thisLine.Items)
                {
                    Assert.True(thisItem.Index > 0, $"The datafile needs to contains well formatted data to parse succesfully and the item needs an index in the test case. The line containing the invalid data is in line# {lineNumber}");

                    Assert.True(thisItem.Weight > 0, $"The datafile needs to contains well formatted data to parse succesfully and the item needs a weight in the test case. The line containing the invalid data is in line# {lineNumber}");
                    Assert.True(thisItem.Weight <= allowedItemWeight, $"The datafile needs to contains well formatted data to parse succesfully and the item needs a weight less or equal than 100 the test case. The line containing the invalid data is in line# {lineNumber}");

                    Assert.True(thisItem.Cost > 0, $"The datafile needs to contains well formatted data to parse succesfully and the item needs a cost in the test case. The line containing the invalid data is in line# {lineNumber}");
                    Assert.True(thisItem.Cost <= allowedItemCost, $"The datafile needs to contains well formatted data to parse succesfully and the item needs a cost less or equal than 100 the test case. The line containing the invalid data is in line# {lineNumber}");
                }

                lineNumber++;
            }
        }
    }
}