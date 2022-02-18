using com.mobiquity.packer.Packer;
using com.mobiquity.packer.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace com.mobiquity.packer.test
{
    public class APITests
    {
        private const string dataFilePath = Constants.PACKER_TEST_FILE_PATH;                        // Use the test/mock file for testing

        [Fact]
        public void ConfirmInputDataFileIsNotEmpty()
        {
            // Arrange
            string results = CallPackerAPI(false);

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "The result back from the API needs to contains data and cannot be empty");
        }

        [Theory]
        [InlineData("81 : (1,5,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)")]
        public void CheckProcessingLogicOfTestCaseOne(string testInput)
        {
            // Arrange
            var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 1
            results = results.TrimEnd();

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "Unit test for case one did not return any value and cannot be empty");
            Assert.Equal("4", results);
        }

        [Theory]
        [InlineData("8 : (1,15.3,€34)")]
        public void CheckProcessingLogicOfTestCaseTwo(string testInput)
        {
            // Arrange
            var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 2
            results = results.TrimEnd();

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "Unit test for case two did not return any value and cannot be empty");
            Assert.Equal("-", results);
        }

        [Theory]
        [InlineData("75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)")]
        public void CheckProcessingLogicOfTestCaseThree(string testInput)
        {
            // Arrange
            var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 3
            results = results.TrimEnd();

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "Unit test for case three did not return any value and cannot be empty");
            Assert.Equal("2,7", results);
        }

        [Theory]
        [InlineData("56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)")]
        public void CheckProcessingLogicOfTestCaseFour(string testInput)
        {
            // Arrange
            var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 4
            results = results.TrimEnd();

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "Unit test for case four did not return any value and cannot be empty");
            Assert.Equal("8,9", results);
        }

        //[Theory]
        //[InlineData("23 : (1,22.10,€90) (2, 22.10, €45)")]
        //public void CheckProcessingLogicOfTestCaseFive(string testInput)
        //{
        //    // Arrange
        //    var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 5
        //    results = results.TrimEnd();

        //    // Act
        //    bool hasContent = results.Length > 0;

        //    // Assert
        //    Assert.True(hasContent, "Unit test for case five did not return any value and cannot be empty");
        //    Assert.Equal("1", results);
        //}

        //[Theory]
        //[InlineData("45 : (1,22.10,€90) (2, 22.10, €45)")]
        //public void CheckProcessingLogicOfTestCaseSix(string testInput)
        //{
        //    // Arrange
        //    var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 6
        //    results = results.TrimEnd();

        //    // Act
        //    bool hasContent = results.Length > 0;

        //    // Assert
        //    Assert.True(hasContent, "Unit test for case six did not return any value and cannot be empty");
        //    Assert.Equal("1,2", results);
        //}


        [Fact]
        public void ConfirmNumberOfItemsinPackageLessEqualTo15()
        {
            // Arrange
            var results = new PackerService().ReadAndProcessPackerData(dataFilePath);        // check against test data file
            results = results.TrimEnd();

            string[] checkItems = results.Split(new char[] { ',' });

            // Act
            bool notMoreThan15Items = checkItems.Length <= 15;

            // Assert
            Assert.True(notMoreThan15Items, "There cannot be more than 15 items in a single package");
        }


        //[Fact]
        //public void TotalPackageWeightLessEqualTo100()
        //{
        //    // Arrange
        //    bool testPassed = false;
        //    var dataFileContent = GetPackFileData();

        //    foreach (var thisLine in dataFileContent.DataLines)                                     // Loop through all available lines and check each line 
        //    {
        //        // Act
        //        string testResult = PackerLineValidator.TotalPackageWeightLessEqualTo100(thisLine);

        //        testPassed = testResult == string.Empty;

        //        Assert.True(testPassed, $"The total weight of a package may not be more then {allowedPackageWeight}. Line# {lineNumber}. Error code return = {testResult}");

        //        lineNumber++;
        //    }
        //}


        private static string CallPackerAPI(bool useMockData = false)
        {
            if (useMockData)
            {
                // Return Mock repository data
                return  MockPacker.Pack(dataFilePath);
            }
            else
            {
                // Return repository data
                return new Packer.Packer().pack(dataFilePath);
            }
        }
    }
}
