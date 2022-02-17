using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.Packer
{
    public interface IPacker
    {
        string pack(string filePath);
        string pack(string filePath, bool ExtendedLogging);
    }
}