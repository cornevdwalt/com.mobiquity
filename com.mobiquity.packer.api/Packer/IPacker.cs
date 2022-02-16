using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.api
{
    public interface IPacker
    {
        string Pack(string filePath);
        string Pack(string filePath, bool ExtendedLogging);
    }
}