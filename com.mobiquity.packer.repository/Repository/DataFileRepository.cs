using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public class DataFileRepository : IDataFileRepository
    {
        private int lineNumber = 1;
        private int allowedPackageWeight = 0;

        private string thisFilePath;
        private string thisFileContent = string.Empty;
        List<DataLine> thisDataLines = new List<DataLine>();

        public DataFileRepository(string filePath)
        {
            thisFilePath = filePath;
        }

        public string ReadRawFileContent()
        {
            thisFileContent = string.Empty;                         // TODO - // Call services to retrieve the file

            // Check for empty file (TODO)

            return thisFileContent;
        }

        public DataFile GetParsedFileContent()
        {
            string fileContent = ReadRawFileContent();

            bool fileParsedSuccessfully = ParseFileContent();

            if (fileParsedSuccessfully)
            {
                return new DataFile()
                {
                    DataLines = thisDataLines
                };
            }

            return new DataFile();                           // TODO - return error code
        }

        #region Private methods
        private bool ParseFileContent()
        {
            bool fileParseSuccessfull = true;

            string fileContent = string.Empty;                          // TODO - Call services to retrieve the raw data

            while (true)                                                // Loop until end of file
            {

                // Get the allowed package weight
                allowedPackageWeight = GetAllowedPackageWeight();

                // Get the Item(s)
                var items = GetListOfItemsInLine();

                // Add the new line
                thisDataLines.Add(new DataLine
                {
                    LineNumber = lineNumber,
                    PackageWeight = allowedPackageWeight,
                    Items = items
                });
            }

            return fileParseSuccessfull;                // TODO - return breaking parse point details
        }

        private int GetAllowedPackageWeight()
        {
            int packageAllowedWeight = 0;

            // start index = 0
            // up to first :
            // return integer

            return packageAllowedWeight;
        }

        private List<DataItem> GetListOfItemsInLine()
        {
            int index = 0;
            decimal weight = 0;
            int cost = 0;                                       // Keep as Integer to conform to the current format in file, but consider to change to decimal (amount)

            List<DataItem> itemsInLine = new List<DataItem>();

            // Loop until end of line
            // start with ( up to first ,   => item number
            // up to next ,                 => item weight
            // up to next ,                 => item cost

            // Find item index
            index = 1;                      // TODO 

            // Find item weight
            weight = 1;                     // TODO 

            // Find item cost
            cost = 1;                       // TODO 

            while (index < thisDataLines.Count)                                     // Loop until end of line
            {
                DataItem item = new DataItem
                {
                    Index = index,
                    Weight = weight,
                    Cost = cost
                };

                // Validate the item
                //
                // Line number  ( > 0 )
                // Weight       ( > 0 )
                // Cost         ( > 0 )

                itemsInLine.Add(item);
            }

            return itemsInLine;
        }
        #endregion
    }
}
