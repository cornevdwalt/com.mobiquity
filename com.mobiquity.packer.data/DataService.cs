namespace com.mobiquity.packer.data
{
    public static class DataService
    {
        public static string[] RetrieveDataFileContent(string filePath)
        {
            var fileContent = System.IO.File.ReadAllLines(filePath);

            if (fileContent.Length > 0) return fileContent;
            else 
                return null;
        }
        public static string ReadAllRawDataInFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);                             
        }
        public static async Task WritePackerOutputFile(List<string> output)
        {
            using StreamWriter file = new(Constants.PACKER_OUTPUT_PATH, append: true);
            foreach (string line in output)
            {
                await file.WriteLineAsync(line);
            }
        }
        public static async Task WritePackerError(string output)
        {
            using StreamWriter file = new(Constants.PACKER_ERRORFILE_PATH, append: true);
            {
                await file.WriteLineAsync("-- " + DateTime.Now + " ----------------------------------------------------------");
                await file.WriteLineAsync(output);
            }
        }
    }
}