namespace com.mobiquity.packer.data
{
    public static class DataService
    {
        public static string ReadAllDataInFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);                              // TODO - change to asynch
        }

        public static string[] RetrieveDataFileContent(string filePath)
        {
            using (var reader = File.OpenText(filePath))
            {
                var fileText = reader.ReadToEnd();
                return fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }

            // Change to async (todo)
            //
            //using (var reader = File.OpenText(filePath))
            //{
            //    var fileText = await reader.ReadToEndAsync();
            //    return fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            //}
        }

        public static async Task WritePackerOutputFile(List<string> output)
        {
            using StreamWriter file = new(@"c:\temp\PackerOutput.txt", append: true);
            await file.WriteLineAsync("-- " + DateTime.Now + " ----------------------------------------------------------" );

            foreach (string line in output)
            {
                await file.WriteLineAsync(line);
            }
        }
    }
}