using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public interface IDataFileRepository
    {
        DataFile GetParsedFileContent();
        string ReadRawFileContent();
    }
}