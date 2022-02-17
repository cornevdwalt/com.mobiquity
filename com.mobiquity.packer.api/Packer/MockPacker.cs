namespace com.mobiquity.packer.Packer
{
    public static class MockPacker
    {
        public static string Pack(string filePath)
        {
            return filePath;            // Mock => echo back
        }

        public static string Pack(string filePath, bool ExtendedLogging)           // Override with extended logging included
        {
            return filePath;            // Mock => echo back
        }

    }
}