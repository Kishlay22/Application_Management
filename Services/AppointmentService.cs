using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.HttpResults;
using Appointment_Management.Repositories;

namespace Appointment_Management.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(Guid Id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(Id);
        }

        public async Task<AppointmentDto> GetAppointmentByNameAsync(string name)
        {
            return await _appointmentRepository.GetAppointmentByNameAsync(name);
        }

        public async Task<Guid> AddAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await _appointmentRepository.AddAppointmentAsync(appointmentDto);
        }

        public async Task<Appointment> UpdateAppointmentAsync(Guid Id, UpdateAppointmentDto updatedAppointmentDto)
        {
            return await _appointmentRepository.UpdateAppointmentAsync(Id, updatedAppointmentDto);
        }

        public async Task<Appointment> DeleteAppointmentAsync(Guid id)
        {
            return await _appointmentRepository.DeleteAppointmentAsync(id);
        }
    }
}
