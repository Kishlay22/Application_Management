
using Appointment_Management.Data.Contexts;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Numerics;

namespace Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {


        private readonly AppointmentContext _context;

        public AppointmentController(AppointmentContext context)
        {
            _context = context;
        }

        /* -----------------------------------------Method to Gell the list of all the Appointment----------------------------------------- */

        [HttpGet]
        public IActionResult GetAll()
        {
            var appointments = _context.Appointments.ToList();
            var appointmentDto = new List<AppointmentDto>();

            foreach (var appointment in appointments)
            {
                appointmentDto.Add(new AppointmentDto()
                {
                    Id = appointment.Id,
                    ConsumerID = appointment.consumer.Id,
                    DateTime = appointment.DateTime,
                    DoctorID = appointment.Doctor.EmpID,
                    Issue = appointment.Issue
                });

            }

            return Ok(appointmentDto);
        }


        /* ------------------------------------------Method to Gell the Appointment by it's ID----------------------------------------- */

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetByid([FromRoute] int Id)
        {
            var appointment = _context.Appointments.Find(Id);

            if(appointment == null)
            {
                return BadRequest();
            }

            var appointmentDto = new AppointmentDto
            {
                Id = appointment.Id,
                ConsumerID = appointment.consumer.Id,
                DateTime = appointment.DateTime,
                DoctorID = appointment.Doctor.EmpID,
                Issue = appointment.Issue
            };

            return Ok(appointmentDto);
        }


        /* ------------------------------------------------Method to add a new Appointment------------------------------------------------- */

        [HttpPost]
        public  IActionResult AddAppointment([FromBody] AppointmentDto appointmentDto)
        {
           var doctor=  _context.Doctors.FirstOrDefault(option => option.EmpID==appointmentDto.DoctorID);
           var consumer= _context.Consumer.FirstOrDefault(option=>option.Id==appointmentDto.ConsumerID);

            if(doctor == null || consumer==null) {
               return BadRequest();
            }

            var newappointmentdetails = new Appointment
            {
                Id = appointmentDto.Id,
                consumer = consumer,
                DateTime = appointmentDto.DateTime,
                Doctor = doctor,
                Issue = appointmentDto.Issue
            };

            _context.Appointments.Add(newappointmentdetails);
            _context.SaveChanges();

            var createappointmentdto = new AppointmentDto
            {

                Id = newappointmentdetails.Id,
                ConsumerID = newappointmentdetails.consumer.Id,
                DateTime =newappointmentdetails.DateTime,
                DoctorID = newappointmentdetails.Doctor.EmpID,
                Issue = newappointmentdetails.Issue
            };

            return CreatedAtAction(nameof(GetAll), new { id = createappointmentdto.Id }, createappointmentdto);
        }


        /* --------------------------------------------Method to Update the Appointment Details-------------------------------------------- */

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Updatedetail([FromRoute] int Id, [FromBody] UpdateAppointmentDto updatdAppointmentDto)
        {
            var doctor = _context.Doctors.FirstOrDefault(option => option.EmpID == updatdAppointmentDto.Doctor.EmpID);
            var consumer = _context.Consumer.FirstOrDefault(option => option.Id == updatdAppointmentDto.consumer.Id);
            var appointment = _context.Appointments.Find(Id);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.consumer = consumer;
            appointment.DateTime = updatdAppointmentDto.DateTime;
            appointment.Issue = updatdAppointmentDto.Issue;
            appointment.Doctor = doctor;

            _context.Update(appointment);
            _context.SaveChanges();

            var updatedAppointmentDto = new Appointment
            {
                Id=appointment.Id,
                consumer = appointment.consumer,
                DateTime = appointment.DateTime,
                Doctor = appointment.Doctor,
                Issue = appointment.Issue
            };

            return Ok(updatedAppointmentDto);
        }


    }
}



