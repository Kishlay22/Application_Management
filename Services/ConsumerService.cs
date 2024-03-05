using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Appointment_Management.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace Appointment_Management.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerService(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task<List<ConsumerDto>> GetAllConsumersAsync()
        {
            return await _consumerRepository.GetAllConsumersAsync();
        }

        public async Task<ConsumerDto> GetConsumerByIdAsync(int Id)
        {
            return await _consumerRepository.GetConsumerByIdAsync(Id);
        }

        public async Task<int> AddConsumerAsync(ConsumerDto consumerDto)
        {
            return await _consumerRepository.AddConsumerAsync(consumerDto);
        }

        public async Task<Consumer> UpdateConsumerAsync(int Id, UpdateConsumerDto updateConsumerDto)
        {
            return await _consumerRepository.UpdateConsumerAsync(Id, updateConsumerDto);
        }

        public async Task<Consumer> PartialUpdateConsumerAsync(int Id, JsonPatchDocument<ConsumerDto> patchDocument)
        {
            return await _consumerRepository.PartialUpdateConsumerAsync(Id, patchDocument);
        }

        public async Task<Consumer> DeleteConsumerAsync(int id)
        {
            return await _consumerRepository.DeleteConsumerAsync(id);
        }
    }
}
