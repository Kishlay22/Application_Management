using Appointment_Management.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Data.Dto
{
    public class UpdateAppointmentDto
    {
        public Consumer consumer { get; set; }
        public DateTime? DateTime { get; set; }
        public Doctor Doctor { get; set; }
        public string Issue { get; set; }
    }
}
