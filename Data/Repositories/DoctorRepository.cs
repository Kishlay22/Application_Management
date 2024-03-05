using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointment_Management.Data.Contexts;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppointmentContext _context;

        public DoctorRepository(AppointmentContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();
            var doctorDtoList = doctors.Select(doctor => new DoctorDto
            {
                EmpID = doctor.EmpID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Active = doctor.Active
            }).ToList();

            return doctorDtoList;
        }

        public async Task<DoctorDto> GetDoctorByEmpIdAsync(int EmpID)
        {
            var doctor = await _context.Doctors.FindAsync(EmpID);

            if (doctor == null)
            {
                return null;
            }

            var doctorDto = new DoctorDto
            {
                EmpID = doctor.EmpID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Active = doctor.Active
            };

            return doctorDto;
        }

        public async Task<int> AddDoctorAsync(DoctorDto doctorDto)
        {
            var newDoctor = new Doctor
            {
                EmpID = doctorDto.EmpID,
                Name = doctorDto.Name,
                Specialization = doctorDto.Specialization,
                Active = doctorDto.Active
            };

            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            return newDoctor.EmpID;
        }

        public async Task<Doctor> UpdateDoctorAsync(int EmpID, UpdateDoctorDto updateDoctorDto)
        {
            var doctor = await _context.Doctors.FindAsync(EmpID);

            if (doctor == null)
            {
                return null;
            }

            doctor.Name = updateDoctorDto.Name;
            doctor.Specialization = updateDoctorDto.Specialization;
            doctor.Active = updateDoctorDto.Active;

            _context.Update(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctor> PartialUpdateDoctorAsync(int EmpID, JsonPatchDocument<DoctorDto> patchDocument)
        {
            var doctor = await _context.Doctors.FindAsync(EmpID);

            if (doctor == null)
            {
                return null;
            }

            var doctorDto = new DoctorDto
            {
                EmpID = doctor.EmpID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Active = doctor.Active
            };

            patchDocument.ApplyTo(doctorDto);

            doctor.Name = doctorDto.Name;
            doctor.Specialization = doctorDto.Specialization;
            doctor.Active = doctorDto.Active;

            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctor> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return null;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
    }
}
