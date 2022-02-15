
namespace com.mobiquity.packer.repository
{
    public interface IDataFile
    {
        string FilePath { get; set; }
        List<Line> DataLines { get; set; }
        
        DataFile GetDataFileContent();
    }
}