namespace com.mobiquity.packer.repository
{
    public class Item : IItem
    {
        public int Index { get; set; }
        public decimal Weight { get; set; } = 0;
        public decimal Cost { get; set; } = 0;
    }
}
