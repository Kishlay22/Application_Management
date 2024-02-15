using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Data.Dto
{
    public class DoctorDto
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public string? Specialization { get; set; }
        public bool Active { get; set; }
    }
}
