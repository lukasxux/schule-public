using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    

    public class StickerService
    {
        private readonly StickerContext _db;

        public StickerService(StickerContext db)
        {
            _db = db;
        }

        public record SaleDto(string CustomerFirstname, string CustomerLastname, string VehicleNumberplate, DateTime PurchaseDate, decimal Price, VehicleType Type);

        public Dictionary<string, List<SaleDto>> GetSales(int year)
        {
            var motorrad = _db.Stickers
                .Where(x => x.PurchaseDate.Year == year && x.Vehicle.VehicleType == VehicleType.Motorcycle)
                .Select(x => new SaleDto(
                    x.Vehicle.Customer.Firstname,
                    x.Vehicle.Customer.Lastname,
                    x.Vehicle.Numberplate,
                    x.PurchaseDate,
                    x.Price,
                    x.Vehicle.VehicleType
                    ))
                .ToList();

            var autoSticker = _db.Stickers
                .Where(x => x.PurchaseDate.Year == year && x.Vehicle.VehicleType == VehicleType.PassengerCar)
                .Select(x => new SaleDto(
                    x.Vehicle.Customer.Firstname,
                    x.Vehicle.Customer.Lastname,
                    x.Vehicle.Numberplate,
                    x.PurchaseDate,
                    x.Price,
                    x.Vehicle.VehicleType
                    ))
                .ToList();

            var autoTollPayment = _db.TollPayments
                .Where(x => x.Timestamp.Year == year && x.Vehicle.VehicleType == VehicleType.PassengerCar)
                .Select(x => new SaleDto(
                    x.Vehicle.Customer.Firstname,
                    x.Vehicle.Customer.Lastname,
                    x.Vehicle.Numberplate,
                    x.Timestamp,
                    x.Price,
                    x.Vehicle.VehicleType
                    ))
                .ToList();

            var bikeTollPayment = _db.TollPayments
               .Where(x => x.Timestamp.Year == year && x.Vehicle.VehicleType == VehicleType.Motorcycle)
               .Select(x => new SaleDto(
                    x.Vehicle.Customer.Firstname,
                    x.Vehicle.Customer.Lastname,
                    x.Vehicle.Numberplate,
                    x.Timestamp,
                    x.Price,
                    x.Vehicle.VehicleType
                    ))
               .ToList();

            motorrad.AddRange(bikeTollPayment);
            autoSticker.AddRange(autoTollPayment);

            var d = new Dictionary<string, List<SaleDto>>();
            d.Add("Motorcycle", motorrad);
            d.Add("PassengerCar", autoSticker);

            return d;
        }
    }
}