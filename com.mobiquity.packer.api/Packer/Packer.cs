using com.mobiquity.packer.data;

namespace com.mobiquity.packer.Packer
{
    public class Packer : IPacker
    {
        /// <summary>
        /// API method to receive a path to a txt file and to parse and process the item information in the file
        /// on separate lines to determine which items to include in a package
        /// </summary>
        /// <param name="filePath">Path to the input data file to use</param>
        /// <returns>Items to include in a package</returns>
        /// <exception cref="Exception"></exception>
        public string pack(string filePath)
        {
            try
            {
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath);
                return results.ToString();
            }
            catch (Exception ex)
            {
                return HandleErrorCondition(ex);
            }
        }

        /// <summary>
        /// Override method with option to write the details of the exception in
        /// a separate error text file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="suppressException"></param>
        /// <returns></returns>
        public string pack(string filePath, bool suppressException)                                            
        {
            try
            {
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath);
                return results.ToString();
            }
            catch (Exception ex)
            {
                return HandleErrorCondition(ex, suppressException);
            }
        }

        /// <summary>
        /// Handle a DataFile or DataItem validation condition, depending on the set condition
        /// write out either the error with an error code or raise an exception to be handled
        /// by the calling system.
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string HandleErrorCondition(Exception ex, bool suppressException = false)
        {
            if (suppressException)
                return "Packer exception raised. Description: " + ex.Message + System.Environment.NewLine;                              
            else
            {
                // Write the technical exeption details to an error file  
                DataService.WritePackerError(ex.Message + "" + ex.StackTrace);

                throw new Exception("Packer exception raised. Description: " + ex.Message);            // Throw the error as an API error
            }
        }
    }
}
