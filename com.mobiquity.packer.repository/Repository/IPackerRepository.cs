using com.mobiquity.packer.domain;

namespace com.mobiquity.packer.repository
{
    public interface IPackerRepository
    {
        DataFile GetParsedFileContent();
        string ReadRawFileContent();
    }
}