using Appointment_Management.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Data.Dto
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public int ConsumerID { get; set; }
        public DateTime? DateTime { get; set; }
        public int DoctorID { get; set; }
        public string Issue { get; set; }
    }
}
