using Microsoft.EntityFrameworkCore;

namespace Enquiry.Model
{
    public class EnquiryDbContext:DbContext
    {
        public EnquiryDbContext(DbContextOptions<EnquiryDbContext> options) : base(options) { }


        public DbSet<EnquiryStatus> EnquiryStatus { get; set; }
        public DbSet< EnquiryModel> EnquiryModels { get; set; }
        public DbSet< EnquiryType> EnquiryType { get; set; }
        
    }
}
