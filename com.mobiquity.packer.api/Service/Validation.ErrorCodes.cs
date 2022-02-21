namespace com.mobiquity.packer.Packer
{
    public static class PACKER_LINE_VALIDATION_CODES
    {
        public const string DataLineTotalWeightGreaterThanZero = "<Error1> ";
        public const string DataLineHasAtLeastOneItem = "<Error2> ";
        public const string NumberOfItemsinDataLineLessEqualTo15 = "<Error3> ";
        public const string TotalPackageWeightLessEqualTo100 = "<Error4> ";
        public const string ItemWeightAndCostLessEqualTo100 = "<Error5> ";
        public const string ItemIndexGreatherThanZero = "<Error6> ";
        public const string ItemWeightLessEqualTo100 = "<Error7> ";
        public const string ItemCostLessEqualTo100 = "<Error8> ";
        public const string ItemCostGreaterThanMaxAllowed = "<Error9> ";
        public const string ItemWeightGreaterThanMaxAllowed = "<Error10> ";
    }
}
