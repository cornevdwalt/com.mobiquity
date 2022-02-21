using com.mobiquity.packer.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Packer
{
    /// <summary>
    /// Class to validate the data line for the input file (test case)
    /// </summary>
    public static class PackerLineValidator
    {
        // -----------------------------------------------------------------
        // Validating the following business rules for the packer solution
        //
        // 1) Max weight of package <= 100
        // 2) Max weight and cost of an item <= 100
        //
        // -----------------------------------------------------------------

        public static string ValidatePackerDataLine(DataLine thisDataLine, int lineNumber)
        {
            string validationResults = string.Empty;

            validationResults = DataLineHasAtLeastOneItem(thisDataLine);
            validationResults += DataLineTotalWeightGreaterThanZero(thisDataLine);
            validationResults += LineItemsValidValues(thisDataLine);

            if (validationResults != string.Empty)
                validationResults = "Line# " + lineNumber + validationResults;

            return validationResults;
        }

        // ---------------------------------------------------------------------
        // Keeping the separate test methods public to allow unit testing from xunit
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

                // For each item in the test case validate the item according to the applicable business rules
                //
                foreach (var thisItem in thisDataLine.Items)
                {
                    if (thisItem.Index <= 0) { itemErrorCode += PACKER_LINE_VALIDATION_CODES.ItemIndexGreatherThanZero + " "; };
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
