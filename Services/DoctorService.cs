using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Appointment_Management.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Appointment_Management.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<DoctorDto> GetDoctorByEmpIdAsync(int EmpID)
        {
            return await _doctorRepository.GetDoctorByEmpIdAsync(EmpID);
        }

        public async Task<int> AddDoctorAsync(DoctorDto doctorDto)
        {
            return await _doctorRepository.AddDoctorAsync(doctorDto);
        }

        public async Task<Doctor> UpdateDoctorAsync(int EmpID, UpdateDoctorDto updateDoctorDto)
        {
            return await _doctorRepository.UpdateDoctorAsync(EmpID, updateDoctorDto);
        }

        public async Task<Doctor> PartialUpdateDoctorAsync(int EmpID, JsonPatchDocument<DoctorDto> patchDocument)
        {
            return await _doctorRepository.PartialUpdateDoctorAsync(EmpID, patchDocument);
        }

        public async Task<Doctor> DeleteDoctorAsync(int id)
        {
            return await _doctorRepository.DeleteDoctorAsync(id);
        }
    }
}
