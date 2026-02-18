using Enquiry.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enquiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public AppointmentController(AppointmentDbContext context)
        {
            _context = context;
        }

        [Route("GetAllAppointment")]
        [HttpGet]
        public IActionResult GetAllAppointment()
        {
            var  list = (from appointment in _context.Appointments join Patient in _context.Patients on appointment.patientId  equals Patient.patientId
                         orderby appointment.appointmentDate descending
                         select new
                         {
                             patientId = Patient.patientId,
                             patientName = Patient.patientName,
                             mobileNo = Patient.mobileNo,
                             city = Patient.city,
                             appointmentId = appointment.appointmentId,
                             appointmentDate = appointment.appointmentDate,
                             isDone = appointment.isDone
                           
                         }).ToList();

            return Ok(list);
        }

        [Route("GetNewAppointmnt")]
        [HttpGet]
        public IActionResult GetNewAppointmnt()
        {
            var  list = (from appointment in _context.Appointments where appointment.isDone ==false join Patient in _context.Patients on appointment.patientId  equals Patient.patientId
                         orderby appointment.appointmentDate ascending
                         select new
                         {
                             patientId = Patient.patientId,
                             patientName = Patient.patientName,
                             mobileNo = Patient.mobileNo,
                             city = Patient.city,
                             appointmentId = appointment.appointmentId,
                             appointmentDate = appointment.appointmentDate,
                             isDone = appointment.isDone
                           
                         }).ToList();

            return Ok(list);
        }

        [Route("GetDoneAppointment")]
        [HttpGet]
        public IActionResult GetDoneAppointment()
        {
            var list = (from appointment in _context.Appointments
                        where appointment.isDone == true
                        join Patient in _context.Patients on appointment.patientId equals Patient.patientId
                        orderby appointment.appointmentDate descending
                        select new
                        {
                            patientId = Patient.patientId,
                            patientName = Patient.patientName,
                            mobileNo = Patient.mobileNo,
                            city = Patient.city,
                            appointmentId = appointment.appointmentId,
                             appointmentDate = appointment.appointmentDate,
                             isDone = appointment.isDone

                        }).ToList();

            return Ok(list);
        }


        [HttpPut("ChangeStatus/{appointmentId}")]
        public IActionResult ChangeStatus(int appointmentId)
        {
            var data = _context.Appointments
                .SingleOrDefault(m => m.appointmentId == appointmentId);

            if (data == null)
                return NotFound(new { message = "Appointment not found" });

            data.isDone = true;
            _context.SaveChanges();

            return Ok(new { message = "Status Updated" });
        }


        [Route("GetAllPatient")]
        [HttpGet]
        public IActionResult GetAllPatient()
        {
            var list = _context.Patients.ToList();
            return Ok(list);
        }


        [HttpPost("CreateNewAppointment")]
        public IActionResult CreateNewAppointment(NewAppointmentModel obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data" });

            // Date must be today → next 7 days
            if (obj.appointmentDate.Date < DateTime.Today ||
                obj.appointmentDate.Date > DateTime.Today.AddDays(7))
            {
                return BadRequest(new { message = "Appointment date must be within next 7 days" });
            }

            // Check patient
            var patient = _context.Patients
                .SingleOrDefault(m => m.mobileNo == obj.mobileNo);

            if (patient == null)
            {
                patient = new Patient
                {
                    mobileNo = obj.mobileNo,
                    address = obj.address,
                    city = obj.city,
                    email = obj.email,
                    patientName = obj.patientName
                };

                _context.Patients.Add(patient);
                _context.SaveChanges();
            }

            // Prevent duplicate appointment same day
            bool alreadyBooked = _context.Appointments.Any(a =>
                a.patientId == patient.patientId &&
                a.appointmentDate.Date == obj.appointmentDate.Date);

            if (alreadyBooked)
                return BadRequest(new { message = "Patient already has appointment on this date" });
             
            // Create appointment of existing patient
            var appointment = new Appointment
            {
                appointmentDate = obj.appointmentDate,
                patientId = patient.patientId,
                isDone = false
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Appointment Created",
                appointmentId = appointment.appointmentId
            });
        }


    }
}


