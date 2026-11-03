using ComputerShopApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("osystems")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        public ComputerShopDbContext _context = new ComputerShopDbContext();

        [HttpGet("all")]
        public object GetAll()
        {
            var users = _context.Osystems.ToList();

            if (users != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = users });
            }
            else if (users.Count == 0)
            {
                return StatusCode(404, new { message = "Üres adatbázis", result = users });
            }
            else
            {
                return new { message = "Hiba a lekérdezésben", result = users };
            }

        }

        [HttpGet("byId")]
        public object GetById([FromQuery] Guid id)
        {
            var user = _context.Osystems.SingleOrDefault(o => o.Id == id);

            if (user != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = user });
            }

            return StatusCode(404, new { message = "Nincs ilyen Id", result = user });
        }
    }
}
