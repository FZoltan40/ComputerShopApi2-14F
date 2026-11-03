using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
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
            var osystem = _context.Osystems.ToList();

            if (osystem != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystem });
            }
            else if (osystem.Count == 0)
            {
                return StatusCode(404, new { message = "Üres adatbázis", result = osystem });
            }
            else
            {
                return new { message = "Hiba a lekérdezésben", result = osystem };
            }

        }

        [HttpGet("byId")]
        public object GetById([FromQuery] Guid id)
        {
            var osystem = _context.Osystems.SingleOrDefault(o => o.Id == id);

            if (osystem != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = osystem });
            }

            return StatusCode(404, new { message = "Nincs ilyen Id", result = osystem });
        }

        [HttpPost("newOs")]
        public object NewOs([FromBody] NewOsystemDto newOsystemDto)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = newOsystemDto.Name,
                Version = newOsystemDto.Version,
                RegisterTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            if (osystem != null)
            {
                _context.Osystems.Add(osystem);
                _context.SaveChanges();

                return StatusCode(201, new { message = "Hozzáadva.", result = osystem });
            }

            return StatusCode(400, new { message = "Hiba a felvételnél.", result = osystem });
        }

        [HttpPut("update")]
        public object UpdateOs([FromQuery] Guid id, [FromBody] UpdateOsystemDto updateOsystemDto)
        {
            var osystem = _context.Osystems.FirstOrDefault(o => o.Id == id);

            if (osystem != null)
            {
                osystem.Name = updateOsystemDto.Name;
                osystem.Version = updateOsystemDto.Version;
                osystem.UpdateTime = DateTime.Now;

                _context.Osystems.Update(osystem);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Frissítve", result = osystem });
            }

            return StatusCode(400, new { message = "Hiba a frissítésnél.", result = osystem });
        }
    }
}
