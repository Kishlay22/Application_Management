using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Appointment_Management.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetByid([FromRoute] Guid Id)
        {
            var appointmentDto = await _appointmentService.GetAppointmentByIdAsync(Id);

            if (appointmentDto == null)
            {
                return BadRequest();
            }

            return Ok(appointmentDto);
        }

        [HttpGet]
        [Route("searchName/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var appointmentDto = await _appointmentService.GetAppointmentByNameAsync(name);

            if (appointmentDto == null)
            {
                return BadRequest();
            }

            return Ok(appointmentDto);
        }

        [HttpPost]
        public IActionResult AddAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                var newAppointmentId = _appointmentService.AddAppointmentAsync(appointmentDto).Result;
                return CreatedAtAction(nameof(GetAll), new { id = newAppointmentId }, new { Id = newAppointmentId });
            }
            catch (Exception)
            {
                return BadRequest("Invalid Doctor or Consumer.");
            }
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Updatedetail([FromRoute] Guid Id, [FromBody] UpdateAppointmentDto updatedAppointmentDto)
        {
            var updatedAppointment = _appointmentService.UpdateAppointmentAsync(Id, updatedAppointmentDto).Result;

            if (updatedAppointment == null)
            {
                return NotFound();
            }

            return Ok(updatedAppointment);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteConsumer([FromRoute] Guid id)
        {
            var deletedAppointment = _appointmentService.DeleteAppointmentAsync(id).Result;

            if (deletedAppointment == null)
            {
                return NotFound();
            }

            return Ok(deletedAppointment);
        }
    }
}
