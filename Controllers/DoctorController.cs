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
    public class DoctorController : ControllerBase
    {
        private readonly AppointmentContext _context;

        public DoctorController(AppointmentContext context)
        {
            _context = context;
        }

        /* -----------------------------------------Method to Gell the list of all the Doctors----------------------------------------- */

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _context.Doctors.ToList();
            var doctorDto = new List<DoctorDto>();

            foreach (var doctor in doctors)
            {
                doctorDto.Add(new DoctorDto()
                {
                    EmpID = doctor.EmpID,
                    Name = doctor.Name,
                    Specialization = doctor.Specialization,
                    Active = doctor.Active
                });

            }

            return Ok(doctorDto);
        }


        /* ------------------------------------------Method to Gell the Doctors by it's EmpID----------------------------------------- */

        [HttpGet]
        [Route("{EmpID}")]
        public IActionResult GetByid([FromRoute] int EmpID)
        {
            var doctor = _context.Doctors.Find(EmpID);

            var doctorDto = new DoctorDto
            {
                EmpID = doctor.EmpID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Active = doctor.Active
            };

            return Ok(doctorDto);
        }


        /* ------------------------------------------------Method to add a new Doctor------------------------------------------------- */

        [HttpPost]
        public IActionResult AddDoctor([FromBody] DoctorDto doctorDto)
        {
            var newdoctordetails = new Doctor
            {
                EmpID = doctorDto.EmpID,
                Name = doctorDto.Name,
                Specialization = doctorDto.Specialization,
                Active = doctorDto.Active
            };

            _context.Doctors.Add(newdoctordetails);
            _context.SaveChanges();

            var createddoctordto = new DoctorDto
            {

                EmpID = newdoctordetails.EmpID,
                Name = newdoctordetails.Name,
                Specialization = newdoctordetails.Specialization,
                Active = newdoctordetails.Active
            };

            return CreatedAtAction(nameof(GetAll), new { id = createddoctordto.EmpID }, createddoctordto);
        }


        /* --------------------------------------------Method to Update the Doctor Details-------------------------------------------- */

        [HttpPut]
        [Route("{EmpID}")]
        public IActionResult Updatedetail([FromRoute] int EmpID, [FromBody] UpdateDoctorDto updateDoctorDto)
        {

            var doctor = _context.Doctors.Find(EmpID);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Name = updateDoctorDto.Name;
            doctor.Specialization = updateDoctorDto.Specialization;
            doctor.Active = updateDoctorDto.Active;

            _context.Update(doctor);
            _context.SaveChanges();

            var updatedDoctorDto = new DoctorDto
            {
                EmpID = doctor.EmpID,
                Name = doctor.Name,
                Specialization = updateDoctorDto.Specialization,
                Active = updateDoctorDto.Active
            };

            return Ok(updatedDoctorDto);
        }


        /* ---------------------------------------Method to Update a single attribute of Doctor--------------------------------------- */

        [HttpPatch]
        [Route("{EmpID}/UpdatePartial")]
        public IActionResult UpdateDoctorStatus([FromRoute] int EmpID, [FromBody] JsonPatchDocument<DoctorDto> updateDoctorDto)
        {
            var doctor = _context.Doctors.Find(EmpID);
            if (doctor == null)
                return BadRequest();

            var updateDoctorStatusDto = new DoctorDto
            {
                EmpID = doctor.EmpID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Active = doctor.Active
            };

            updateDoctorDto.ApplyTo(updateDoctorStatusDto,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            doctor.Name = updateDoctorStatusDto.Name;
            doctor.Specialization = updateDoctorStatusDto.Specialization;
            doctor.Active = updateDoctorStatusDto.Active;

            _context.SaveChanges();

            return Ok(updateDoctorStatusDto);

        }

    }
}

