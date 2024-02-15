using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Data.Dto
{
    public class UpdateDoctorDto
    {
        public string Name { get; set; }
        public string? Specialization { get; set; }
        public bool Active { get; set; }
    }
}
