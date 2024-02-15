
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
    public class ConsumerController : ControllerBase
    {

        private readonly AppointmentContext _context;

        public ConsumerController(AppointmentContext context)
        {
            _context = context;
        }

        /* -----------------------------------------Method to Gell the list of all the Consumers----------------------------------------- */

        [HttpGet]
        public IActionResult GetAll()
        {
            var consumers = _context.Consumer.ToList();
            var consumerDto = new List<ConsumerDto>();

            foreach (var consumer in consumers)
            {
                consumerDto.Add(new ConsumerDto()
                {
                    Id = consumer.Id,
                    Name = consumer.Name,
                    Email = consumer.Email,
                    PhoneNo = consumer.PhoneNo
                });

            }

            return Ok(consumerDto);
        }


        /* ------------------------------------------Method to Gell the Consumer by it's ID----------------------------------------- */

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetByid([FromRoute] int Id)
        {
            var consumer = _context.Consumer.Find(Id);

            var consumerDto = new ConsumerDto
            {
                Id = consumer.Id,
                Name = consumer.Name,
                Email = consumer.Email,
                PhoneNo = consumer.PhoneNo
            };

            return Ok(consumerDto);
        }


        /* ------------------------------------------------Method to add a new Consumer------------------------------------------------- */

        [HttpPost]
        public IActionResult AddConsumer([FromBody] ConsumerDto consumerDto)
        {
            var newconsumerdetails = new Consumer
            {
                Id = consumerDto.Id,
                Name = consumerDto.Name,
                Email = consumerDto.Email,
                PhoneNo = consumerDto.PhoneNo
            };

            _context.Consumer.Add(newconsumerdetails);
            _context.SaveChanges();

            var createconsumerdto = new ConsumerDto
            {

                Id = newconsumerdetails.Id,
                Name = newconsumerdetails.Name,
                Email = newconsumerdetails.Email,
                PhoneNo = newconsumerdetails.PhoneNo
            };

            return CreatedAtAction(nameof(GetAll), new { id = createconsumerdto.Id }, createconsumerdto);
        }


        /* --------------------------------------------Method to Update the Consumer Details-------------------------------------------- */

        [HttpPut]
        [Route("{Id}")]
        public IActionResult Updatedetail([FromRoute] int Id, [FromBody] UpdateConsumerDto updateConsumerDto)
        {
            var consumer = _context.Consumer.Find(Id);

            if (consumer == null)
            {
                return NotFound();
            }

            consumer.Name = updateConsumerDto.Name;
            consumer.Email = updateConsumerDto.Email;
            consumer.PhoneNo = updateConsumerDto.PhoneNo;

            _context.Update(consumer);
            _context.SaveChanges();

            var updatedConsumerDto = new Consumer
            {
                Id = consumer.Id,
                Name = consumer.Name,
                Email = updateConsumerDto.Email,
                PhoneNo = updateConsumerDto.PhoneNo
            };

            return Ok(updatedConsumerDto);
        }


        /* -------------------------------------------Method to Update a single attribute of Consumer------------------------------------------- */

        [HttpPatch]
        [Route("{Id}/UpdatePartial")]
        public IActionResult UpdateConsumerStatus([FromRoute] int Id, [FromBody] JsonPatchDocument<ConsumerDto> updateConsumerDto)
        {
            var consumer = _context.Consumer.Find(Id);
            if (consumer == null)
                return BadRequest();

            var updateConsumerStatusDto = new ConsumerDto
            {
                Id = consumer.Id,
                Name = consumer.Name,
                Email = consumer.Email,
                PhoneNo = consumer.PhoneNo
            };

            updateConsumerDto.ApplyTo(updateConsumerStatusDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            consumer.Name = updateConsumerStatusDto.Name;
            consumer.Email = updateConsumerStatusDto.Email;
            consumer.PhoneNo = updateConsumerStatusDto.PhoneNo;

            _context.SaveChanges();

            return Ok(updateConsumerStatusDto);

        }

    }
}


