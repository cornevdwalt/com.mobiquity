using com.mobiquity.packer.domain;
using System.ComponentModel.DataAnnotations;

namespace com.mobiquity.web.Models
{
    public class DataFileViewModel
    {
        [Display(Name = "File path")]
        public string FilePath { get; set; }
        public List<DataLine>? DataLines { get; set; }
        [Display(Name = "Results")]
        public string? ParseResults { get; set; }
    }
}