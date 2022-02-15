namespace com.mobiquity.packer.api
{
    public interface IDataFile
    {
        string pack(string filePath);
        string pack(string filePath, bool extraLogging);
    }
}