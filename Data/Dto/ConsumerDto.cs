using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Data.Dto
{
    public class ConsumerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string PhoneNo { get; set; }
    }
}
