using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Appointment_Management.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumerService;

        public ConsumerController(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var consumers = await _consumerService.GetAllConsumersAsync();
            return Ok(consumers);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetByid([FromRoute] int Id)
        {
            var consumerDto = await _consumerService.GetConsumerByIdAsync(Id);

            if (consumerDto == null)
            {
                return NotFound();
            }

            return Ok(consumerDto);
        }

        [HttpPost]
        public IActionResult AddConsumer([FromBody] ConsumerDto consumerDto)
        {
            try
            {
                var newConsumerId = _consumerService.AddConsumerAsync(consumerDto).Result;
                return CreatedAtAction(nameof(GetAll), new { id = newConsumerId }, new { Id = newConsumerId });
            }
            catch
            {
                return BadRequest("Invalid Consumer details.");
            }
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Updatedetail([FromRoute] int Id, [FromBody] UpdateConsumerDto updateConsumerDto)
        {
            var updatedConsumer = _consumerService.UpdateConsumerAsync(Id, updateConsumerDto).Result;

            if (updatedConsumer == null)
            {
                return NotFound();
            }

            return Ok(updatedConsumer);
        }

        [HttpPatch]
        [Route("{Id}/UpdatePartial")]
        public IActionResult UpdateConsumerStatus([FromRoute] int Id, [FromBody] JsonPatchDocument<ConsumerDto> patchDocument)
        {
            var updatedConsumer = _consumerService.PartialUpdateConsumerAsync(Id, patchDocument).Result;

            if (updatedConsumer == null)
            {
                return NotFound();
            }

            return Ok(updatedConsumer);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteConsumer([FromRoute] int id)
        {
            var deletedConsumer = _consumerService.DeleteConsumerAsync(id).Result;

            if (deletedConsumer == null)
            {
                return NotFound();
            }

            return Ok(deletedConsumer);
        }
    }
}
