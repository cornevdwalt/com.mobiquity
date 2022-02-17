using com.mobiquity.packer.data;
using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.api
{
    public class Packer : IPacker
    {
        public string Pack(string filePath)
        {
            try
            {
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath);

                return results.ToString();
            }
            catch (Exception ex)
            {
                // Return user friendly error message  
                throw new Exception("An internal error occured. The details are: " + ex.Message);                    
            }
        }

        public string Pack(string filePath, bool ExtendedLogging)                               // Override with extended logging included
        {
            try
            {
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath);

                return results.ToString();
            }
            catch (Exception ex)
            {
                // Write the technical exeption details to error file
                //DataService.WritePackerError(ex.StackTrace);

                return "An internal error occured. The details are: " + ex.Message;             // Return user friendly error message
            }
        }
    }
}
