using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.Packer
{
    public interface IPackerService
    {
        string ReadAndProcessPackerData_OLD(string filePath, string unitTestDataLine = null);

        //string ReadAndProcessPackerData(string filePath, IPackerRepository packerRepository);
    }
}