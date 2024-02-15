using Appointment_Management.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Data.Contexts
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consumer> Consumer { get; set; }
    }
}
