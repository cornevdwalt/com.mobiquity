namespace com.mobiquity.packer.Packer
{
    public static class PACKER_LINE_VALIDATION_CODES
    {
        public const string DataLineTotalWeightGreaterThanZero = "<Error code=1> ";
        public const string DataLineHasAtLeastOneItem = "<Error code=2> ";
        public const string NumberOfItemsinDataLineLessEqualTo15 = "<Error code=3> ";
        public const string TotalPackageWeightLessEqualTo100 = "<Error code=4> ";
        public const string ItemWeightAndCostLessEqualTo100 = "<Error code=5> ";
        public const string ItemIndexLessEqualTo100 = "<Error code=6> ";
        public const string ItemWeightLessEqualTo100 = "<Error code=7> ";
        public const string ItemCostLessEqualTo100 = "<Error code=8> ";
        public const string ItemCostGreaterThanMaxAllowed = "<Error code=9> ";
        public const string ItemWeightGreaterThanMaxAllowed = "<Error code=10> ";
    }
}
