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
                bool raiseException = true;
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath, raiseException);
                return results.ToString();
            }
            catch (Exception ex)
            {
                return HandleErrorCondition(ex);
            }
        }

        /// <summary>
        /// Override method with option to not raise an exception when a error condition occurred but to 
        /// rather write out the validation or exception details as part of the results back to the calling client
        /// and generate a separate error text file with the details of the exception.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public string pack(string filePath, bool raiseException = false)                                            
        {
            try
            {
                var packerService = new PackerService();
                var results = packerService.ReadAndProcessPackerData(filePath, raiseException);
                return results.ToString();
            }
            catch (Exception ex)
            {
                return HandleErrorCondition(ex, raiseException);
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
        private string HandleErrorCondition(Exception ex, bool raiseException = true)
        {
            if (raiseException)
                return "Packer File Exception raised. Error description: " + ex.Message + System.Environment.NewLine;                              
            else
            {
                // Write the technical exeption details to error file  (TODO)


                throw new Exception("Packer File Exception raised. Error description: " + ex.Message);            // Throw the error as an API error
            }
        }
    }
}
