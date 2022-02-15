using Xunit;

namespace com.mobiquity.packer.test
{
    public class DataLineTests
    {
        int lineNumber = 0;

        [Fact]
        public void DataLineTotalWeightGreaterThanZero()
        {
            // Arrange
            lineNumber = 10;

            // Act
            bool totalPackageWeightGreaterThanZero = false;

            // Assert
            Assert.True(totalPackageWeightGreaterThanZero, $"The total weight of a package allowed must be greater than zero. Line# {lineNumber}");
        }

        [Fact]
        public void DataLineHasAtLeastOneItem()
        {
            // Arrange
            lineNumber = 10;
            int numberOfItemsInLine = 0;

            // Act
            bool atLeastOneItemInLine = numberOfItemsInLine > 0;

            // Assert
            Assert.True(atLeastOneItemInLine, $"There must be at least one item in a package. Line# {lineNumber}");
        }

        [Fact]
        public void NumberOfItemsinDataLineLessEqualTo15()
        {
            // Arrange
            lineNumber = 10;
            int numberOfItemsInLine = 20;

            // Act
            bool itemsInRange = numberOfItemsInLine <= 15;

            // Assert
            Assert.True(itemsInRange, $"There cannot be more than 15 items in a single package. Line# {lineNumber}");
        }

        [Fact]
        public void ItemWeightLessEqualTo100()
        {
            // Arrange
            lineNumber = 10;
            int itemWeight = 200;
            int allowedWeight = 100;

            // Act
            bool itemCorrectWeight = itemWeight <= allowedWeight;

            // Assert
            Assert.True(itemCorrectWeight, $"An item may not weight more then {allowedWeight}. Line# {lineNumber}");
        }

        [Fact]
        public void ItemCostLessEqualTo100()
        {
            // Arrange
            lineNumber = 10;
            int itemCost = 200;
            int allowedCost = 100;

            // Act
            bool itemCorrectCost = itemCost <= allowedCost;

            // Assert
            Assert.True(itemCorrectCost, $"The cost of an item may not be more then {allowedCost}. Line# {lineNumber}");
        }

        [Fact]
        public void TotalPackageWeightLessEqualTo100()
        {
            // Arrange
            lineNumber = 10;
            int totalPackageWeight = 200;
            int allowedWeight = 100;

            // Act
            bool packageCorrectWeight = totalPackageWeight <= allowedWeight;

            // Assert
            Assert.True(packageCorrectWeight, $"The total weight of a package may not be more then {allowedWeight}. Line# {lineNumber}");
        }
    }
}