using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    //[Collection("Sequential")]
    public class Aufgabe1Test
    {
        private AppointmentContext GetEmptyDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureCreated();
            return db;
        }

        // TODO: Add some more unit tests to cover the requirements

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
        }

        [Fact]
        public void PersistRichTypesSuccessTest()
        {

        }
        [Fact]
        public void PersistEnumSuccessTest()
        {

        }

        [Fact]
        public void GetConfirmedAppointmentsSuccessTest()
        {

            using var db = GetEmptyDbContext();
            Patient p = new Patient("Amin", "B", "Amin@gmail.com", new Phone("12345"), new Address("alterlaa", "1230", "Wien"));
            db.Patients.Add(p);
            db.SaveChanges();

            Appointment b = new Appointment(DateTime.Now.AddDays(2), DateTime.Now, p, new List<AppointmentState>());

            Appointment a = new Appointment(DateTime.Now, DateTime.Now, p, new List<AppointmentState>());
            a.AppointmentStates.Add(new AppointmentState(DateTime.Now, AppointmentStateType.Confirmed, null));
            db.Appointments.Add(a);
            db.Appointments.Add(b);
            db.SaveChanges();


            var result = db.GetConfirmedAppointments(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));

            
            Assert.Single(result);

        }
    }
}