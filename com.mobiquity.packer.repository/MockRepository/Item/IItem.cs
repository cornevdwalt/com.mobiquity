namespace com.mobiquity.packer.repository
{
    public interface IItem
    {
        decimal Cost { get; set; }
        int Index { get; set; }
        decimal Weight { get; set; }
    }
}