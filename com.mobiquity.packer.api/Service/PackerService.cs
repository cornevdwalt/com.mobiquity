using com.mobiquity.packer.domain;
using com.mobiquity.packer.repository;

namespace com.mobiquity.packer.api
{
    public class PackerService : IPackerService
    {
        //private IPackerRepository _packerRepositoryService;         // Not read-only

        public string ReadAndProcessPackerData(string filePath)
        {
            string results = string.Empty;

            var packerRepository = new MockPackerRepository(filePath);                  // Mock for now - need to change to di

            // Get the data file content
            var data = packerRepository.GetParsedFileContent();

            // Process the information 
            results = ProcessPackerData(data);

            // Write out the results in output file (todo)

            return results;
        }

        //public string ReadAndProcessPackerData(string filePath, IPackerRepository packerRepository)
        //{
        //    //_packerRepositoryService = packerRepository;

        //    return filePath;
        //}


        private string ProcessPackerData(DataFile data)
        {
            string results = string.Empty;

            foreach (var line in data.DataLines)
            {
                results += line.LineNumber + " ";
            }

            return results;
        }
    }
}
