using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.api
{
    public class Packer : IPacker
    {
        
        public string Pack(string filePath)
        {
            var packerService = new PackerService();
            var results = packerService.ReadAndProcessPackerData(filePath);

            return results.ToString();
        }

        public string Pack(string filePath, bool ExtendedLogging)           // Override with extended logging included
        {
            return filePath;
        }
    }
}
