using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe3.Commands;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http;

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


        [HttpGet("{numberplate}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(string numberplate, [FromQuery] bool includeStickers = false) {
        
        if (!_numberplateCheck.IsMatch(numberplate)) return Problem("fehlerhaftes Kennzeichen", statusCode: 400);
        var vehicle = await SearchVehicle(numberplate, includeStickers);
            if (vehicle == null) return NotFound("No Car found");

            return Ok(vehicle);
        }

        private async Task<VehicleDto?> SearchVehicle(string numberplate, bool includeStickers)
        {
            return await _db.Vehicles
                .Where(v => v.Numberplate == numberplate)
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

        public record MotValidDto(string motValidUntis);

        [HttpPatch("{numberplate}")]
        public async Task<ActionResult<VehicleDto>> PatchPickerl(string numberplate, UpdateVehicleCommand updateVehicleCommand)
        {
            if (!_numberplateCheck.IsMatch(numberplate)) return Problem("fehlerhaftes Kennzeichen (inkl. RFC-9457 ProblemDetail im Body)", statusCode: 400);
            
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(a => a.Numberplate == numberplate);
            if (vehicle == null) return Problem("unbekanntes Kennzeichen", statusCode: 404);
            if (updateVehicleCommand.motValidUntil < vehicle.MotValidUntil) return Problem("das neue Datum ist kleiner als das gespeicherte MotValidUntil Datum", statusCode: 400);
            vehicle.MotValidUntil = updateVehicleCommand.motValidUntil;
            await _db.SaveChangesAsync();
            return Ok(new MotValidDto(vehicle.MotValidUntil.ToString()));
        }

    }
}
