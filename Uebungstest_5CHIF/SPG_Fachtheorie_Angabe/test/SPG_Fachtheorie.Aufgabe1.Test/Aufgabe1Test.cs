using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
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

        }
    }
}