using com.mobiquity.packer.api;
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
        private const string dataFilePath = @"c:\temp\example_input.txt";

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
        public void CheckProcessingOfTestCaseOne(string testInput)
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
        public void CheckProcessingOfTestCaseTwo(string testInput)
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
        public void CheckProcessingOfTestCaseThree(string testInput)
        {
            // Arrange
            var results = new PackerService().ReadAndProcessPackerData(dataFilePath, testInput);        // check against test line 3
            results = results.TrimEnd();

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "Unit test for case three did not return any value and cannot be empty");
            Assert.Equal("2,4", results);
        }

        [Theory]
        [InlineData("56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)")]
        public void CheckProcessingOfTestCaseFour(string testInput)
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
                return new Packer().Pack(dataFilePath);
            }
        }
    }
}
