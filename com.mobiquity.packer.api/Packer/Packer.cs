using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.api
{
    public class Packer : IPacker
    {
        public string Pack(string filePath)
        {

            // Confirm that a file path was received
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            
            try
            {
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath);

                return results.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Pack(string filePath, bool ExtendedLogging)           // Override with extended logging included
        {
            return filePath;
        }
    }
}
