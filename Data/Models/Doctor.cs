using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointment_Management.Data.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }
        public string Name { get; set; }
        public string? Specialization { get; set; }

        [Display(Name = "Is Active?")]
        public bool Active { get; set; }


    }
}
