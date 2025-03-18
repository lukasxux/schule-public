using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe3.Commands;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Spg.Fachtheorie.Aufgabe3.API.Test
{
    [Collection("Sequential")]
    public class VehiclesControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public VehiclesControllerTests(TestWebApplicationFactory factory)
        {

            _factory = factory;
        }

        [Theory]
        [InlineData("AB1234CD", HttpStatusCode.OK)]
        [InlineData("1234", HttpStatusCode.BadRequest)]
        [InlineData("AB1234CE", HttpStatusCode.NotFound)]
        public async Task GetVehicleTest(string numberplate, HttpStatusCode expectedStatusCode) {
            _factory.InitializeDatabase(db =>
            {
                db.Database.EnsureDeleted();  // Löscht die gesamte Datenbank
                db.Database.EnsureCreated();  // Erstellt sie neu

                db.Database.Migrate();
                var Vehicle1 = new Vehicle(
                    "AB1234CD",
                    new Customer("Lukas", "Hainzl", "Lukas@gmail.com", DateTime.Now),
                    VehicleType.Motorcycle,
                    new DateOnly(2025, 3, 20)
                    );
                db.Vehicles.Add(Vehicle1);
                db.SaveChanges();
            });

            var (statusCode, VehicleWithSticker) = await _factory.GetHttpContent<VehicleDto>($"/Vehicles/{numberplate}?includeStickers={false}");

            Assert.True(statusCode == expectedStatusCode);
        }

        [Theory]
        [InlineData("AB1234CD", "2025 - 03 - 20", HttpStatusCode.OK)]
        [InlineData("1234", "2025 - 03 - 20", HttpStatusCode.BadRequest)]
        [InlineData("AB1234CE", "2025 - 03 - 20", HttpStatusCode.NotFound)]
        [InlineData("AB1234CD", "2025 - 03 - 16", HttpStatusCode.BadRequest)]
        public async Task PatchVehicleTest(string numberplate, string DateString, HttpStatusCode expectedStatusCode)
        {
            _factory.InitializeDatabase(db =>
            {
                db.Database.EnsureDeleted();  
                db.Database.EnsureCreated();

                db.Database.Migrate();

                var Vehicle1 = new Vehicle(
                    "AB1234CD",
                    new Customer("Lukas", "Hainzl", "Lukas@gmail.com", DateTime.Now),
                    VehicleType.Motorcycle,
                    new DateOnly(2025, 3, 18)
                    );
                db.Vehicles.Add(Vehicle1);
                db.SaveChanges();
            });
            DateOnly Date = DateOnly.Parse(DateString);
            var updatedVehicleMto = new UpdateVehicleCommand(Date);

            var (statusCode, VehicleWithSticker) = await _factory.PatchHttpContent($"/Vehicles/{numberplate}", updatedVehicleMto);
            
            Assert.True(statusCode == expectedStatusCode);
            if (expectedStatusCode == HttpStatusCode.OK)
            {
                var vehiclesFromDb = _factory.QueryDatabase(db => db.Vehicles.First());
                Assert.True(vehiclesFromDb.MotValidUntil == updatedVehicleMto.motValidUntil);
            }
        }
    }
}
