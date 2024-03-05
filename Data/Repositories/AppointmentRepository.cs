using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointment_Management.Data.Contexts;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentContext _context;

        public AppointmentRepository(AppointmentContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.Include("Doctor").Include("consumer").ToListAsync();
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(Guid Id)
        {
            var appointment = await _context.Appointments.Include(a => a.consumer).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.Id == Id);

            if (appointment == null)
            {
                return null;
            }

            return new AppointmentDto
            {
                Id = appointment.Id,
                ConsumerID = appointment.consumer.Id,
                DateTime = appointment.DateTime,
                DoctorID = appointment.Doctor.EmpID,
                Issue = appointment.Issue
            };
        }

        public async Task<AppointmentDto> GetAppointmentByNameAsync(string name)
        {
            var appointment = await _context.Appointments.Include(a => a.consumer).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.consumer.Name == name);

            if (appointment == null)
            {
                return null;
            }

            return new AppointmentDto
            {
                Id = appointment.Id,
                ConsumerID = appointment.consumer.Id,
                DateTime = appointment.DateTime,
                DoctorID = appointment.Doctor.EmpID,
                Issue = appointment.Issue
            };
        }

        public async Task<Guid> AddAppointmentAsync(AppointmentDto appointmentDto)
        {
            var doctor = _context.Doctors.FirstOrDefault(option => option.EmpID == appointmentDto.DoctorID);
            var consumer = _context.Consumer.FirstOrDefault(option => option.Id == appointmentDto.ConsumerID);

            if (doctor == null || consumer == null)
            {
                throw new InvalidOperationException("Invalid Doctor or Consumer.");
            }

            var newAppointment = new Appointment
            {
                Id = appointmentDto.Id,
                consumer = consumer,
                DateTime = appointmentDto.DateTime,
                Doctor = doctor,
                Issue = appointmentDto.Issue
            };

            _context.Appointments.Add(newAppointment);
            await _context.SaveChangesAsync();

            return newAppointment.Id;
        }

        public async Task<Appointment> UpdateAppointmentAsync(Guid Id, UpdateAppointmentDto updatedAppointmentDto)
        {
            var doctor = _context.Doctors.FirstOrDefault(option => option.EmpID == updatedAppointmentDto.Doctor.EmpID);
            var consumer = _context.Consumer.FirstOrDefault(option => option.Id == updatedAppointmentDto.consumer.Id);
            var appointment = await _context.Appointments.FindAsync(Id);

            if (appointment == null)
            {
                return null;
            }

            appointment.consumer = consumer;
            appointment.DateTime = updatedAppointmentDto.DateTime;
            appointment.Issue = updatedAppointmentDto.Issue;
            appointment.Doctor = doctor;

            _context.Update(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment> DeleteAppointmentAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return null;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}

