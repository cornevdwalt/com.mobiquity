using System.Collections;

namespace com.mobiquity.packer.domain
{
    public class DataFile
    {
        public string FilePath { get; set; }
        public IList<DataLine> DataLines { get; set; }

        IEnumerable<DataLine> GetAllDataLines()
        {
            return this.DataLines.ToList();
        }
    }
}
