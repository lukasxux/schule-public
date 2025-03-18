using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static SPG_Fachtheorie.Aufgabe2.Services.StickerService;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class StickerServiceTests
    {
        private StickerContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("DataSource=stickers.db")
                .Options;
            var db = new StickerContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            using var db = GetEmptyDbContext();
            db.Seed();
            Assert.True(db.Stickers.Count() > 0);
        }

        /// <summary>
        /// Test für die Methode GetSales.
        /// Verkäufe für das Jahr 2024:
        /// SELECT c.Firstname, c.Lastname, v.Numberplate, s.PurchaseDate, s.Price, 'Sticker' AS Type
        /// FROM Stickers s INNER JOIN Vehicles v ON (s.VehicleId = v.Id)
        /// INNER JOIN Customers c ON (v.CustomerId = c.Id)
        /// WHERE strftime('%Y', s.PurchaseDate) = '2024'
        /// UNION
        /// SELECT c.Firstname, c.Lastname, v.Numberplate, tp."Timestamp", tp.Price, 'TollPayment' AS Type
        /// FROM TollPayments tp INNER JOIN Vehicles v ON (tp.VehicleId = v.Id)
        /// INNER JOIN Customers c ON (v.CustomerId = c.Id)
        /// WHERE strftime('%Y', tp.timestamp) = '2024'
        /// ORDER BY Type, PurchaseDate;
        /// 
        /// DATEN für 2024:
        /// 
        /// Firstname	Lastname	Numberplate	PurchaseDate	Price	Type
        /// Cecilia	Büttner	BN94598EHP	2024-04-26 03:49:20	4.9	Sticker
        /// Lotte	Rietscher	BN89995FNM	2024-05-12 05:04:35	103.8	Sticker
        /// Lotte	Rietscher	GF20574HBV	2024-05-17 05:04:35	41.5	Sticker
        /// Lotte	Rietscher	GF14080GCO	2024-05-18 05:04:35	103.8	Sticker
        /// Cecilia	Büttner	BN94598EHP	2024-06-05 03:49:20	12.4	Sticker
        /// Cecilia	Büttner	BN94598EHP	2024-09-06 03:49:20	41.5	Sticker
        /// Tessa	Pohl	MD76829KSW	2024-11-11 12:02:04	12.4	Sticker
        /// Tessa	Pohl	W42769VFW	2024-11-14 12:02:04	103.8	Sticker
        /// Tiago	Häber	MD1979GBP	2024-12-29 06:24:02	41.5	Sticker
        /// Nina	Röse	BN94387GDH	2024-05-02 10:01:35	19.03	TollPayment
        /// Lotte	Rietscher	GF83488CBZ	2024-05-17 15:26:36	23.13	TollPayment
        /// Lotte	Rietscher	GF20574HBV	2024-06-05 14:14:29	14.68	TollPayment
        /// Lotte	Rietscher	GF20574HBV	2024-06-16 15:52:23	26.36	TollPayment
        /// Lotte	Rietscher	GF14080GCO	2024-07-25 16:29:15	14.62	TollPayment
        /// Lotte	Rietscher	GF14080GCO	2024-07-27 10:05:14	25.53	TollPayment
        /// Nina	Röse	BN94387GDH	2024-09-08 23:09:25	22.59	TollPayment
        /// Cecilia	Büttner	BN94598EHP	2024-10-28 12:37:16	24.94	TollPayment
        /// Cecilia	Büttner	BN94598EHP	2024-11-19 16:44:06	26.29	TollPayment
        /// Tessa	Pohl	MD76829KSW	2024-12-03 14:31:11	25.28	TollPayment
        /// Lotte	Rietscher	GF14080GCO	2024-12-15 19:31:20	13.01	TollPayment
        /// Tessa	Pohl	MD76829KSW	2024-12-18 12:21:16	17.0	TollPayment
        /// Tessa	Pohl	MD76829KSW	2024-12-25 01:15:03	22.8	TollPayment
        /// </summary>
        [Fact]
        public void GetSalesSuccessTest()
        {
            using var db = GetEmptyDbContext();
            // TODO: Schreibe Asserts auf Basis der oben angegebenen Daten.
            db.Seed();
            var service = new StickerService(db);
            var sales = service.GetSales(2024);

            Assert.True(sales.ContainsKey("Motorcycle"));
            Assert.True(sales.ContainsKey("PassengerCar"));

            Assert.True(sales["Motorcycle"].Count + sales["PassengerCar"].Count == 22);

        }
    }
}