using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Infrastructure
{
    public class StickerContext : DbContext
    {
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sticker> Stickers => Set<Sticker>();
        public DbSet<TollPayment> TollPayments => Set<TollPayment>();
        public DbSet<TollStation> TollStations => Set<TollStation>();

        public StickerContext()
        { }

        public StickerContext(DbContextOptions options) : base(options)
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

        public void Seed()
        {
            Randomizer.Seed = new Random(1309);
            var customers = new Faker<Customer>("de")
                .CustomInstantiator(f =>
                {
                    var registrationDate = f.Date.Between(new DateTime(2023, 1, 1), new DateTime(2025, 1, 1));
                    registrationDate = new DateTime(registrationDate.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
                    return new Customer(
                        f.Name.FirstName(), f.Name.LastName(),
                        f.Internet.Email(),
                        registrationDate);
                })
                .Generate(5)
                .ToList();
            Customers.AddRange(customers);
            SaveChanges();
            var cities = new string[] { "W", "MD", "BN", "GF" };
            var tollStations = new TollStation[]
            {
                new TollStation("Bosruck"),
                new TollStation("Gleinalm"),
                new TollStation("Arlberg"),
            };
            TollStations.AddRange(tollStations);
            SaveChanges();
            var possibleDaysValid = new int[] { 10, 60, 365 };
            var pricesMotorcycle = new Dictionary<int, decimal> { { 10, 4.9M }, { 60,  12.4M}, { 365, 41.5M } };
            var pricesPkw = new Dictionary<int, decimal> { { 10, 12.4M }, { 60, 31.4M }, { 365, 103.8M } };
            var vehicles = new Faker<Vehicle>("de")
                .CustomInstantiator(f =>
                {
                    var customer = f.Random.ListItem(customers);
                    var numberplate = $"{f.Random.ListItem(cities)}{f.Random.Int(1000, 99999)}{f.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")}";
                    var vehicle = new Vehicle(
                        numberplate, customer, f.Random.Enum<VehicleType>(),
                        f.Date.BetweenDateOnly(new DateOnly(2024, 1, 1), new DateOnly(2026, 1, 1)));
                    var minValidFrom = customer.RegistrationDate;
                    var stickers = new Faker<Sticker>("de")
                        .CustomInstantiator(f =>
                        {
                            var daysValid = f.Random.ListItem(possibleDaysValid);
                            var price = vehicle.VehicleType switch
                            {
                                VehicleType.Motorcycle => pricesMotorcycle[daysValid],
                                VehicleType.PassengerCar => pricesPkw[daysValid],
                                _ => 0
                            };
                            var purchaseDate = minValidFrom.AddDays(f.Random.Int(1, 10));
                            var validFrom = purchaseDate.AddDays(f.Random.Int(14, 30));
                            minValidFrom = validFrom.AddDays(daysValid);
                            return new Sticker(vehicle, purchaseDate, DateOnly.FromDateTime(validFrom), daysValid, price);
                        })
                        .Generate(f.Random.Int(0,4))
                        .ToList();
                    var tollPayments = new Faker<TollPayment>("de")
                        .CustomInstantiator(f =>
                        {
                            var tollStation = f.Random.ListItem(tollStations);
                            var timestamp = f.Date.Between(customer.RegistrationDate, new DateTime(2025, 1, 1));
                            timestamp = new DateTime(timestamp.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
                            var tollPayment = new TollPayment(
                                vehicle, tollStation, f.Random.Int(1000, 3000) / 100M,
                                timestamp);
                            return tollPayment;
                        })
                        .Generate(f.Random.Int(0, 3))
                        .ToList();
                    vehicle.TollPayments.AddRange(tollPayments);
                    vehicle.Stickers.AddRange(stickers);
                    return vehicle;
                })
                .Generate(10)
                .ToList();
            Vehicles.AddRange(vehicles);
            SaveChanges();
        }
    }
}