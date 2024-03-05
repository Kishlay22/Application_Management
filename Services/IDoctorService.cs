using Appointment_Management.Data.Dto;
using Appointment_Management.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointment_Management.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByEmpIdAsync(int EmpID);
        Task<int> AddDoctorAsync(DoctorDto doctorDto);
        Task<Doctor> UpdateDoctorAsync(int EmpID, UpdateDoctorDto updateDoctorDto);
        Task<Doctor> PartialUpdateDoctorAsync(int EmpID, JsonPatchDocument<DoctorDto> patchDocument);
        Task<Doctor> DeleteDoctorAsync(int id);
    }
}

