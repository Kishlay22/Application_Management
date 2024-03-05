using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Appointment_Management.Repositories
{
    public interface IConsumerRepository
    {
        Task<List<ConsumerDto>> GetAllConsumersAsync();
        Task<ConsumerDto> GetConsumerByIdAsync(int Id);
        Task<int> AddConsumerAsync(ConsumerDto consumerDto);
        Task<Consumer> UpdateConsumerAsync(int Id, UpdateConsumerDto updateConsumerDto);
        Task<Consumer> PartialUpdateConsumerAsync(int Id, JsonPatchDocument<ConsumerDto> patchDocument);
        Task<Consumer> DeleteConsumerAsync(int id);
    }
}
