﻿using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.api
{
    public interface IPackerService
    {
        string ReadAndProcessPackerData(string filePath, string unitTestDataLine = null);

        //string ReadAndProcessPackerData(string filePath, IPackerRepository packerRepository);
    }
}