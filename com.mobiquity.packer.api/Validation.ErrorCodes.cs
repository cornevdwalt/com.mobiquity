namespace com.mobiquity.packer.Packer
{
    public static class PACKER_LINE_VALIDATION_CODES
    {
        public const string DataLineTotalWeightGreaterThanZero = "ErrCode=1";
        public const string DataLineHasAtLeastOneItem = "ErrCode=2";
        public const string NumberOfItemsinDataLineLessEqualTo15 = "ErrCode=3";
        public const string TotalPackageWeightLessEqualTo100 = "ErrCode=4";
        public const string ItemWeightAndCostLessEqualTo100 = "ErrCode=5";
        public const string ItemIndexLessEqualTo100 = "ErrCode=6";
        public const string ItemWeightLessEqualTo100 = "ErrCode=7";
        public const string ItemCostLessEqualTo100 = "ErrCode=8";
        public const string ItemCostGreaterThanMaxAllowed = "ErrCode=9";
        public const string ItemWeightGreaterThanMaxAllowed = "ErrCode=10";
    }
}
