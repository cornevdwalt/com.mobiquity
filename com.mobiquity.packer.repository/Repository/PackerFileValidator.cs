using com.mobiquity.packer.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.repository
{
    public static class PackerFileValidator
    {
        public static string DataFileIsNotEmpty(string dataFileContent)
        {
            try
            {
                bool hasContent = dataFileContent.Length > 0;

                if (hasContent)
                    return String.Empty;
                else
                    return PACKERFILE_VALIDATION_CODES.DataFileIsNotEmpty;
            }
            catch (Exception)
            {
                return PACKERFILE_VALIDATION_CODES.DataFileIsNotEmpty;
            }
            
        }

        public static string DataFileNeedsAtLeastOneLine(string[] dataLines)
        {
            try
            {
                // Act
                bool hasItems = dataLines.Count() > 0;

                if (hasItems)
                    return String.Empty;
                else
                    return PACKERFILE_VALIDATION_CODES.DataFileNeedsAtLeastOneLine;
            }
            catch (Exception)
            {
                return PACKERFILE_VALIDATION_CODES.DataFileNeedsAtLeastOneLine;
            }
        }

        public static string DataFileNeedsAtLeastOneLine(DataFile dataFileContent)
        {
            try
            {
                // Arrange
                DataLine firstDataLine = dataFileContent.DataLines[0];

                // Act
                bool hasItems = firstDataLine.Items.Count > 0;

                if (hasItems)
                    return String.Empty;
                else
                    return PACKERFILE_VALIDATION_CODES.DataFileNeedsAtLeastOneLine;
            }
            catch (Exception)
            {
                return PACKERFILE_VALIDATION_CODES.DataFileNeedsAtLeastOneLine;
            }
        }
    }
}
