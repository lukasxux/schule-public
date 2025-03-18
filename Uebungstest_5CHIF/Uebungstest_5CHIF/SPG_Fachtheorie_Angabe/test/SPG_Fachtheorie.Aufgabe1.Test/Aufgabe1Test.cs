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
            using var db = GetEmptyDbContext();
            var patient = new Patient(
                firstname: "Lukas",
                lastname: "Hainzl",
                email: "Lukas.hainzl2004@gmail.com",
                phone: new Phone(
                    value: "123456789"
                    ),
                address: new Address(
                    street: "Liesing",
                    zip: "1230",
                    city: "Wien"
                    )
                );
            db.Patients.Add( patient );
            db.SaveChanges();
            db.ChangeTracker.Clear();
            
            Assert.True( db.Patients.Include(a => a.Phone).First().Phone.Value == "123456789");
        }
        [Fact]
        public void PersistEnumSuccessTest()
        {
            using var db = GetEmptyDbContext();
            var patient = new Patient(
                firstname: "Lukas",
                lastname: "Hainzl",
                email: "Lukas.hainzl2004@gmail.com",
                phone: new Phone(
                    value: "123456789"
                    ),
                address: new Address(
                    street: "Liesing",
                    zip: "1230",
                    city: "Wien"
                    )
                );
            db.Patients.Add(patient);
            db.SaveChanges();

            var appointment = new Appointment(
                    date: DateTime.Now,
                    time: DateTime.Now,
                    patient: patient
                    );
            AppointmentState appointmentState = new AppointmentState(
                created: DateTime.Now,
                type: AppointmentStateType.Confirmed,
                note: null
                );

            appointment.AppointmentStates.Add( appointmentState );
            db.Appointments.Add( appointment );
            db.SaveChanges();
            db.ChangeTracker.Clear();
            Assert.True(db.Appointments
                .Include(a => a.AppointmentStates)
                .First()
                .AppointmentStates.First().Type == AppointmentStateType.Confirmed);


        }

        [Fact]
        public void GetConfirmedAppointmentsSuccessTest()
        {

            using var db = GetEmptyDbContext();
            Patient p = new Patient("Amin", "B", "Amin@gmail.com", new Phone("12345"), new Address("alterlaa", "1230", "Wien"));
            db.Patients.Add(p);
            db.SaveChanges();

            Appointment b = new Appointment(DateTime.Now.AddDays(2), DateTime.Now, p);

            Appointment a = new Appointment(DateTime.Now, DateTime.Now, p);
            a.AppointmentStates.Add(new AppointmentState(DateTime.Now, AppointmentStateType.Confirmed, null));
            db.Appointments.Add(a);
            db.Appointments.Add(b);
            db.SaveChanges();


            var result = db.GetConfirmedAppointments(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));

            
            Assert.Single(result);

        }
    }
}