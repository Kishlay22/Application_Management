using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Appointment_Management.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet]
        [Route("{EmpID}")]
        public async Task<IActionResult> GetByid([FromRoute] int EmpID)
        {
            var doctorDto = await _doctorService.GetDoctorByEmpIdAsync(EmpID);

            if (doctorDto == null)
            {
                return NotFound();
            }

            return Ok(doctorDto);
        }

        [HttpPost]
        public IActionResult AddDoctor([FromBody] DoctorDto doctorDto)
        {
            try
            {
                var newDoctorId = _doctorService.AddDoctorAsync(doctorDto).Result;
                return CreatedAtAction(nameof(GetAll), new { id = newDoctorId }, new { EmpID = newDoctorId });
            }
            catch
            {
                return BadRequest("Invalid Doctor details.");
            }
        }

        [HttpPut]
        [Route("{EmpID}")]
        public IActionResult Updatedetail([FromRoute] int EmpID, [FromBody] UpdateDoctorDto updateDoctorDto)
        {
            var updatedDoctor = _doctorService.UpdateDoctorAsync(EmpID, updateDoctorDto).Result;

            if (updatedDoctor == null)
            {
                return NotFound();
            }

            return Ok(updatedDoctor);
        }

        [HttpPatch]
        [Route("{EmpID}/UpdatePartial")]
        public IActionResult UpdateDoctorStatus([FromRoute] int EmpID, [FromBody] JsonPatchDocument<DoctorDto> patchDocument)
        {
            
            var updatedDoctor = _doctorService.PartialUpdateDoctorAsync(EmpID, patchDocument).Result;

            if (updatedDoctor == null)
            {
                return NotFound();
            }

            return Ok(updatedDoctor);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDoctor([FromRoute] int id)
        {
            var deletedDoctor = _doctorService.DeleteDoctorAsync(id).Result;

            if (deletedDoctor == null)
            {
                return NotFound();
            }

            return Ok(deletedDoctor);
        }
    }
}
