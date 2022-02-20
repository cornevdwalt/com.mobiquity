using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.Packer
{
    public interface IPackerService
    {
        string ReadAndProcessPackerData(string filePath, string unitTestDataLine = null);
    }
}