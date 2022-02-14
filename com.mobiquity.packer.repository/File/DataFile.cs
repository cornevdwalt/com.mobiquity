using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.repository.File
{
    public class DataFile : IDataFile
    {
        public string FilePath { get; set; } 
        public List<Line> DataLines { get; set; }
        
        private int lineNumber = 1;
        private int allowedPackageWeight = 0;

        public DataFile(string filePath)                        // Constructor
        {
            FilePath = filePath;
        }

        public DataFile GetDataFileContent()
        {
            string fileContent = ReadFileContent();

            bool fileParsedSuccessfully = ParseFileContent();

            if (fileParsedSuccessfully)
            {
                return new DataFile(FilePath)
                {
                    DataLines = DataLines
                };
            }

            return new DataFile(FilePath);              // TODO - return error code
        }

        private string ReadFileContent()
        {

            // Call services to retrieve the file

            // Check for empty file

            return "";
        }

        private bool ParseFileContent()
        {
            bool fileParseSuccessfull = true;

            List<Line>  dataLines = new List<Line>();

            while (true)                                                // Loop until end of file
            {

                // Get the allowed package weight
                allowedPackageWeight = GetAllowedPackageWeight();

                // Get the Item(s)
                var items = GetListOfItemsInLine();

                // Add the new line
                DataLines.Add(new Line
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

        private List<Item> GetListOfItemsInLine()
        {
            int index = 0;
            decimal weight = 0;
            int cost = 0;                                       // Keep as Integer to conform to the current format in file, but consider to change to decimal (amount)

            List<Item> itemsInLine = new List<Item>();

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

            while (index < DataLines.Count)                                     // Loop until end of line
            {
                Item item = new Item
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
    }
}
