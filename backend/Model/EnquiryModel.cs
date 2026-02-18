using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry
{
    [Table("Enquiry")]
    public class EnquiryModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int enquiryId { get; set; }
        public int enquiryTypeId { get; set; }
        public int enquiryStatusId { get; set; }
        public int customerName { get; set; }
        public string mobileNo { get; set; } = "";
        public string email { get; set; } = "";
        public string message { get; set; } = "";
        public DateTime createdDate { get; set; }
        public string resolution { get; set; } = "";
    }
}
