using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Data.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }
        public Consumer consumer { get; set; }
        public DateTime? DateTime { get; set; }
        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Please write about your health condition")]
        public string Issue { get; set; }
    }
}
