using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record SaleDto();

    public class StickerService
    {
        private readonly StickerContext _db;

        public StickerService(StickerContext db)
        {
            _db = db;
        }

        public Dictionary<string, List<SaleDto>> GetSales(int year)
        {
            throw new NotImplementedException();
        }
    }
}