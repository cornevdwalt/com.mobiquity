namespace com.mobiquity.packer.api
{
    public static class MockPacker
    {
        public static string Pack(string filePath)
        {
            return "AAA";
        }

        public static string Pack(string filePath, bool ExtendedLogging)           // Override with extended logging included
        {
            return "BBB";
        }

    }
}