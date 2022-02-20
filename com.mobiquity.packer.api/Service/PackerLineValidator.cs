using com.mobiquity.packer.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Packer
{
    public static class PackerLineValidator
    {
        // -----------------------------------------------------------------
        // Validating the following business rules for the packer solution
        //
        // 1) Max weight of package <= 100
        // 2) Max weight and cost of an item <= 100
        //
        // -----------------------------------------------------------------

        public static string ValidatePackerDataLine(DataLine thisDataLine)
        {
            string validationResults = string.Empty;

            validationResults = DataLineHasAtLeastOneItem(thisDataLine);
            validationResults += DataLineTotalWeightGreaterThanZero(thisDataLine);
            validationResults += LineItemsValidValues(thisDataLine);

            return validationResults;
        }

        // ---------------------------------------------------------------------
        // Keeping the separate test public to allow unit testing from xunit
        // ---------------------------------------------------------------------
        public static string DataLineTotalWeightGreaterThanZero(DataLine thisDataLine)
        {
            bool totalPackageWeightGreaterThanZero;

            try
            {
                if (thisDataLine.Items == null) return PACKER_LINE_VALIDATION_CODES.DataLineTotalWeightGreaterThanZero;

                totalPackageWeightGreaterThanZero = totalPackageWeightGreaterThanZero = thisDataLine.PackageWeight > 0;

                if (totalPackageWeightGreaterThanZero)
                    return "";
                else
                    return PACKER_LINE_VALIDATION_CODES.DataLineTotalWeightGreaterThanZero;
            }
            catch (Exception)
            {
                return PACKER_LINE_VALIDATION_CODES.DataLineTotalWeightGreaterThanZero;
            }
        }

        public static string DataLineHasAtLeastOneItem(DataLine thisDataLine)
        {
            bool atLeastOneItemInLine;

            try
            {
                if (thisDataLine.Items == null) return PACKER_LINE_VALIDATION_CODES.DataLineHasAtLeastOneItem;

                atLeastOneItemInLine = thisDataLine.Items.Count > 0;

                if (atLeastOneItemInLine)
                    return "";
                else
                    return PACKER_LINE_VALIDATION_CODES.DataLineHasAtLeastOneItem;
            }
            catch (Exception)
            {
                return PACKER_LINE_VALIDATION_CODES.DataLineHasAtLeastOneItem;
            }
        }

        public static string LineItemsValidValues(DataLine thisDataLine)
        {
            bool dataFileContent;
            string itemErrorCode = string.Empty;

            try
            {
                if (thisDataLine.Items == null) return PACKER_LINE_VALIDATION_CODES.ItemWeightAndCostLessEqualTo100;

                foreach (var thisItem in thisDataLine.Items)
                {
                    if (thisItem.Index <= 0) { itemErrorCode += PACKER_LINE_VALIDATION_CODES.ItemIndexLessEqualTo100 + " "; };
                    if (thisItem.Weight <= 0) { itemErrorCode += PACKER_LINE_VALIDATION_CODES.ItemWeightLessEqualTo100 + " "; };
                    if (thisItem.Cost <= 0) { itemErrorCode += PACKER_LINE_VALIDATION_CODES.ItemCostLessEqualTo100 + " "; };
                    if (thisItem.Weight > Constrain.MAX_PACKAGE_WEIGHT) { itemErrorCode += PACKER_LINE_VALIDATION_CODES.ItemWeightGreaterThanMaxAllowed + " "; };
                    if (thisItem.Cost > Constrain.MAX_ITEM_COST) { itemErrorCode += PACKER_LINE_VALIDATION_CODES.ItemCostGreaterThanMaxAllowed + " "; };
                }
                return itemErrorCode;
            }
            catch (Exception)
            {
                return itemErrorCode;
            }
        }

    }
}
