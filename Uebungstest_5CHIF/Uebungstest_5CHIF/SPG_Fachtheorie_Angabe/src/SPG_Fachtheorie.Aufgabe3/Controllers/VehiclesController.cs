using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly StickerContext _db;

        public VehiclesController(StickerContext db)
        {
            _db = db;
        }

        private static Regex _numberplateCheck = new Regex(@"^[A-Z]{1,2}\d{2,7}[A-Z]{2,3}$", RegexOptions.Compiled);


        [HttpGet("{numberpalte}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(string numberpalte, [FromQuery] bool includeStickers = false)
        {
            var vehicle = await SearchVehicle(numberpalte, includeStickers);
            if (vehicle == null) return Problem("No Car found", statusCode: 404);
            if (!_numberplateCheck.IsMatch(vehicle.numberplate)) return Problem("fehlerhaftes Kennzeichen (inkl. RFC-9457 ProblemDetail im Body)", statusCode: 400);
            return Ok(vehicle);
        }

        private async Task<VehicleDto?> SearchVehicle(string numberpalte, bool includeStickers) {
            return await _db.Vehicles
                .Where(v => v.Numberplate == numberpalte)
                .Include(v => v.Stickers)
                .Select(v => new VehicleDto(
                    v.Numberplate, 
                    v.Customer.Firstname, 
                    v.Customer.Lastname, 
                    v.VehicleType,
                    includeStickers 
                    ? v.Stickers.Select(s => new StickerDto(
                        s.PurchaseDate.ToString(), 
                        s.ValidFrom.ToString(), 
                        s.ValidFrom.AddDays(s.DaysValid).ToString(), 
                        s.Price
                        )).ToList() 
                        : new List<StickerDto>()
                        ))
                .FirstOrDefaultAsync();
        }

        [HttpGet("{numberpalte}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(string numberpalte, [FromQuery] bool includeStickers = false)
        {
            var vehicle = await SearchVehicle(numberpalte, includeStickers);
            if (vehicle == null) return Problem("No Car found", statusCode: 404);
            if (!_numberplateCheck.IsMatch(vehicle.numberplate)) return Problem("fehlerhaftes Kennzeichen (inkl. RFC-9457 ProblemDetail im Body)", statusCode: 400);
            return Ok(vehicle);
        }
    }
}
