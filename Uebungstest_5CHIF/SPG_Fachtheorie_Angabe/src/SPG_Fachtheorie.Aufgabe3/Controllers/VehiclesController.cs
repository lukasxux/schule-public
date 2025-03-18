using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

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


    }
}
