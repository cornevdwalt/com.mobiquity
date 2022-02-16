using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public class MockPackerRepository : IPackerRepository
    {
        private string thisFilePath;

        public MockPackerRepository(string filePath)
        {
            thisFilePath = filePath;
        }

        public string ReadRawFileContent()
        {
            string fileContent = "81 : (1,in,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)";

            return fileContent;
        }

        public DataFile GetParsedFileContent()
        {
            #region Mock Items
            var mockItemA = new DataItem
            {
                Cost = 5,
                Weight = 15,
                Index = 1
            };

            var mockItemB = new DataItem
            {
                Cost = 15,
                Weight = 25,
                Index = 2
            };

            var mockItemC = new DataItem
            {
                Cost = 25,
                Weight = 10,
                Index = 3
            };
            #endregion

            #region Mock Lines
            DataLine mockLineA = new DataLine
            {
                LineNumber = 1,
                PackageWeight = 100,
            };
            mockLineA.Items = new List<DataItem>();
            mockLineA.Items.Add(mockItemA);
            mockLineA.Items.Add(mockItemB);
            mockLineA.Items.Add(mockItemC);

            DataLine mockLineB = new DataLine
            {
                LineNumber = 2,
                PackageWeight = 10,
            };
            mockLineB.Items = new List<DataItem>();
            mockLineB.Items.Add(mockItemA);

            DataLine mockLineC = new DataLine
            {
                LineNumber = 3,
                PackageWeight = 50,
            };
            mockLineC.Items = new List<DataItem>();
            mockLineC.Items.Add(mockItemB);
            mockLineC.Items.Add(mockItemC); 
            #endregion

            var result = new DataFile
            {
                FilePath = thisFilePath,
            };
            result.DataLines = new List<DataLine>();
            result.DataLines.Add(mockLineA);
            result.DataLines.Add(mockLineB);
            result.DataLines.Add(mockLineC);

            return result;
        }
    }
}
