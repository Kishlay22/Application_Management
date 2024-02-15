using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointment_Management.Data.Models
{
    public class Consumer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Please write your Email ID")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please provide your Phone No")]
        public string PhoneNo { get; set; }
    }
}
