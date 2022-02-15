namespace com.mobiquity.packer.domain
{
    public class DataLine 
    {
        public int LineNumber { get; set; }
        public int PackageWeight { get; set; } = 0;
        public IList<DataItem>? Items { get; set;}
    }
}
