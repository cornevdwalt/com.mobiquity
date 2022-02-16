using com.mobiquity.packer.api;
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
        public void ProcessDataFileIsNotEmpty()
        {
            // Arrange
            string results = CallPackerAPI(false);

            // Act
            bool hasContent = results.Length > 0;

            // Assert
            Assert.True(hasContent, "The result back from the API needs to contains data and cannot be empty");
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
