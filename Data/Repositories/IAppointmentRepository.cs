using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;

namespace Appointment_Management.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(Guid Id);
        Task<AppointmentDto> GetAppointmentByNameAsync(string name);
        Task<Guid> AddAppointmentAsync(AppointmentDto appointmentDto);
        Task<Appointment> UpdateAppointmentAsync(Guid Id, UpdateAppointmentDto updatedAppointmentDto);
        Task<Appointment> DeleteAppointmentAsync(Guid id);
    }
}

