namespace com.mobiquity.packer.repository
{
    public class Line : ILine
    {
        public int LineNumber { get; set; }
        public int PackageWeight { get; set; } = 0;
        public IList<Item>? Items { get; set;}
    }
}
