
namespace com.mobiquity.packer.repository.File
{
    public interface IDataFile
    {
        string FilePath { get; set; }
        List<Line> DataLines { get; set; }
        
        DataFile GetDataFileContent();
    }
}