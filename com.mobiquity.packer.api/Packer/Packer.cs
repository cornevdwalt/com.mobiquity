namespace com.mobiquity.packer.api
{
    public class Packer : IPacker
    {
        public string Pack(string filePath)
        {
            return filePath;
        }

        public string Pack(string filePath, bool ExtendedLogging)           // Override with extended logging included
        {
            return filePath;
        }
    }
}
