using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enquiry.Model
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options) 
        { 
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }

    

    [Table("patient")]
    public class Patient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int patientId { get; set; }

        [Required]
        public string patientName { get; set; } = string.Empty;

        [Required]
        public string mobileNo { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        [Required]
        public string city { get; set; } = string.Empty;

        [Required]
        public string address { get; set; } = string.Empty;
    }


    public class NewAppointmentModel
    {
        public int patientId { get; set; }
        public string patientName { get; set; } = string.Empty;

        [Required]
        public string mobileNo { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        [Required]
        public string city { get; set; } = string.Empty;

        [Required]
        public string address { get; set; } = string.Empty;

        public DateTime appointmentDate { get; set; }

    }



    [Table("appointment")]
    public class Appointment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int appointmentId { get; set; }

        [Required]
        public int patientId { get; set; }

        [ForeignKey("patientId")]
        public Patient? Patient { get; set; }   // ✅ Add this

        [Required]
        public DateTime appointmentDate { get; set; }

        [Required]
        public bool isDone { get; set; }

        public double? fees { get; set; }
    }

}

