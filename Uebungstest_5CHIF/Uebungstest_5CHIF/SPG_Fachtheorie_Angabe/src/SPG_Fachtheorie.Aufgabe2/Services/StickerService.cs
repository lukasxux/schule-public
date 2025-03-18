using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record SaleDto(decimal price);

    public class StickerService
    {
        private readonly StickerContext _db;

        public StickerService(StickerContext db)
        {
            _db = db;
        }

        public record StickerDto(int id,DateTime purchaseDate, decimal Price);

        public Dictionary<string, List<SaleDto>> GetSales(int year)
        {
            var motorrad = _db.Stickers
                .Where(x => x.PurchaseDate.Year == year && x.Vehicle.VehicleType == VehicleType.Motorcycle)
                .Select(x => new SaleDto(
                    x.Price))
                .ToList();

            var autoSticker = _db.Stickers
                .Where(x => x.PurchaseDate.Year == year && x.Vehicle.VehicleType == VehicleType.PassengerCar)
                .Select(x => new SaleDto(
                    x.Price))
                .ToList();

            var autoTollPayment = _db.TollPayments
                .Where(x => x.Timestamp.Year == year && x.Vehicle.VehicleType == VehicleType.PassengerCar)
                .Select(x => new SaleDto (x.Price)).ToList();

            var bikeTollPayment = _db.TollPayments
               .Where(x => x.Timestamp.Year == year && x.Vehicle.VehicleType == VehicleType.Motorcycle)
               .Select(x => new SaleDto(x.Price)).ToList();

            motorrad.AddRange(bikeTollPayment);
            autoSticker.AddRange(autoTollPayment);

            var d = new Dictionary<string, List<SaleDto>>();
            d.Add("Motorcycle", motorrad);
            d.Add("PassengerCar", autoSticker);

            return d;
        }
    }
}