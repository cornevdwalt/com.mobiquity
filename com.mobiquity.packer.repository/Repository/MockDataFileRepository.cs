using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public class MockDataFileRepository : IDataFileRepository
    {
        private string thisFilePath;

        public MockDataFileRepository(string filePath)
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
            var mockItem = new DataItem
            {
                Cost = 5,
                Weight = 15,
                Index = 1
            };

            DataLine mockLine = new DataLine
            {
                LineNumber = 1,
                PackageWeight = 100,
            };
            mockLine.Items = new List<DataItem>();
            mockLine.Items.Add(mockItem);

            var result = new DataFile
            {
                FilePath = thisFilePath,
            };
            result.DataLines = new List<DataLine>();
            result.DataLines.Add(mockLine);

            return result;
        }
    }
}
