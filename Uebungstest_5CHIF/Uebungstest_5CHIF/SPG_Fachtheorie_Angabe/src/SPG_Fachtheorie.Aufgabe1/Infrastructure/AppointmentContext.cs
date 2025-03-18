using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Patient> Patients => Set<Patient>();

        public record ConfirmedAppoinemtDto(string firstname, string lastname, string email, Phone phone);

        public AppointmentContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Generic config for all entities
                // ON DELETE RESTRICT instead of ON DELETE CASCADE
                foreach (var key in entityType.GetForeignKeys())
                    key.DeleteBehavior = DeleteBehavior.Restrict;

                foreach (var prop in entityType.GetDeclaredProperties())
                {
                    // Define Guid as alternate key. The database can create a guid fou you.
                    if (prop.Name == "Guid")
                    {
                        modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                        prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    }
                    // Default MaxLength of string Properties is 255.
                    if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                    // Seconds with 3 fractional digits.
                    if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                    if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
                }
            }

        }

        public List<ConfirmedAppoinemtDto> GetConfirmedAppointments(DateTime from, DateTime to)
        {
            var patients = Appointments
                .Include(a => a.AppointmentStates)
                .Where(a => a.Date >= from && a.Date <= to && a.AppointmentStates.Any(a => a.Type == AppointmentStateType.Confirmed))
                .Select(p => new ConfirmedAppoinemtDto(p.Patient.Firstname, p.Patient.Lastname, p.Patient.Email, p.Patient.Phone));
            return patients.ToList();
        }
    }
}