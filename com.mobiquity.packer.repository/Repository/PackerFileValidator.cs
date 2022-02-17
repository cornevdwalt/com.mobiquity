﻿using com.mobiquity.packer.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.repository
{
    public static class PackerFileValidator
    {
        public static int DataFileIsNotEmpty(string dataFileContent)
        {
            try
            {
                bool hasContent = dataFileContent.Length > 0;

                if (hasContent)
                    return 0;
                else
                    return PACKERFILE_VALIDATION_CODES.DataFileIsNotEmpty;
            }
            catch (Exception)
            {
                return PACKERFILE_VALIDATION_CODES.DataFileIsNotEmpty;
            }
            
        }

        public static int DataFileNeedsAtLeastOneLine(string[] dataLines)
        {
            try
            {
                // Act
                bool hasItems = dataLines.Count() > 0;

                if (hasItems)
                    return 0;
                else
                    return PACKERFILE_VALIDATION_CODES.DataFileNeedsAtLeastOneLine;
            }
            catch (Exception)
            {
                return PACKERFILE_VALIDATION_CODES.DataFileNeedsAtLeastOneLine;
            }
        }

        public static int DataFileNeedsAtLeastOneLine(DataFile dataFileContent)
        {
            try
            {
                // Arrange
                DataLine firstDataLine = dataFileContent.DataLines[0];

                // Act
                bool hasItems = firstDataLine.Items.Count > 0;

                if (hasItems)
                    return 0;
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
