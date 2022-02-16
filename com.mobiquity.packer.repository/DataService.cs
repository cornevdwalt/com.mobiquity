namespace com.mobiquity.packer.repository
{

    /// <summary>
    /// TODO - Extract this to a separate layer to be called elsewhere as well
    /// </summary>

    internal static class DataService
    {
        public static string ReadAllDataInFile(string filePath)
        {
            return System.IO.File.ReadAllText(@"c:\temp\example_input.txt");            // TODO - Call as separate thread (asynch) ... while
            //return System.IO.File.ReadAllText(filePath);                                // TODO - Call as separate thread (asynch)
        }

        public static string[] RetrieveDataFileContent(string filePath)
        {
            return System.IO.File.ReadAllLines(@"c:\temp\example_input.txt");           // TODO - put in while
        }
    }
}
