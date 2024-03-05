using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Appointment_Management.Data.Contexts;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Appointment_Management.Repositories
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly AppointmentContext _context;

        public ConsumerRepository(AppointmentContext context)
        {
            _context = context;
        }

        public async Task<List<ConsumerDto>> GetAllConsumersAsync()
        {
            var consumers = await _context.Consumer.ToListAsync();
            var consumerDtoList = consumers.Select(consumer => new ConsumerDto
            {
                Id = consumer.Id,
                Name = consumer.Name,
                Email = consumer.Email,
                PhoneNo = consumer.PhoneNo
            }).ToList();

            return consumerDtoList;
        }

        public async Task<ConsumerDto> GetConsumerByIdAsync(int Id)
        {
            var consumer = await _context.Consumer.FindAsync(Id);

            if (consumer == null)
            {
                return null;
            }

            var consumerDto = new ConsumerDto
            {
                Id = consumer.Id,
                Name = consumer.Name,
                Email = consumer.Email,
                PhoneNo = consumer.PhoneNo
            };

            return consumerDto;
        }

        public async Task<int> AddConsumerAsync(ConsumerDto consumerDto)
        {
            var newConsumer = new Consumer
            {
                Id = consumerDto.Id,
                Name = consumerDto.Name,
                Email = consumerDto.Email,
                PhoneNo = consumerDto.PhoneNo
            };

            _context.Consumer.Add(newConsumer);
            await _context.SaveChangesAsync();

            return newConsumer.Id;
        }

        public async Task<Consumer> UpdateConsumerAsync(int Id, UpdateConsumerDto updateConsumerDto)
        {
            var consumer = await _context.Consumer.FindAsync(Id);

            if (consumer == null)
            {
                return null;
            }

            consumer.Name = updateConsumerDto.Name;
            consumer.Email = updateConsumerDto.Email;
            consumer.PhoneNo = updateConsumerDto.PhoneNo;

            _context.Update(consumer);
            await _context.SaveChangesAsync();

            return consumer;
        }

        public async Task<Consumer> PartialUpdateConsumerAsync(int Id, JsonPatchDocument<ConsumerDto> patchDocument)
        {
            var consumer = await _context.Consumer.FindAsync(Id);

            if (consumer == null)
            {
                return null;
            }

            var consumerDto = new ConsumerDto
            {
                Id = consumer.Id,
                Name = consumer.Name,
                Email = consumer.Email,
                PhoneNo = consumer.PhoneNo
            };

            patchDocument.ApplyTo(consumerDto);

            consumer.Name = consumerDto.Name;
            consumer.Email = consumerDto.Email;
            consumer.PhoneNo = consumerDto.PhoneNo;

            await _context.SaveChangesAsync();

            return consumer;
        }

        public async Task<Consumer> DeleteConsumerAsync(int id)
        {
            var consumer = await _context.Consumer.FindAsync(id);

            if (consumer == null)
            {
                return null;
            }

            _context.Consumer.Remove(consumer);
            await _context.SaveChangesAsync();

            return consumer;
        }
    }
}
