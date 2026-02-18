using Enquiry.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryMasterController : ControllerBase
    {

        private readonly EnquiryDbContext _context;

        public EnquiryMasterController(EnquiryDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllStatus")]

        public List<EnquiryStatus> GetEnquiryStatus()
        {
            var list = _context.EnquiryStatus.ToList();
            return list;
        }

        [HttpGet("GetAllTpes")]
        public List<EnquiryType> GetEnquiryTypes()
        {
            var list = _context.EnquiryType.ToList();
            return list;
        }



    }


}
