namespace com.mobiquity.packer.repository
{
    public interface ILine
    {
        int LineNumber { get; set; }
        int PackageWeight { get; set; }
    }
}