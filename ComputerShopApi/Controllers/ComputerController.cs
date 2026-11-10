using ComputerShopApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("computers")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public ComputerShopDbContext _context = new ComputerShopDbContext();

        [HttpGet("allComputer")]
        public object AllComputer()
        {
            var computers = _context.Computers.ToList();

            if (computers != null)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = computers });
            }
            else if (computers.Count == 0)
            {
                return StatusCode(404, new { message = "Üres adatbázis", result = computers });
            }
            else
            {
                return new { message = "Hiba a lekérdezésben", result = computers };
            }
        }

        [HttpGet("byId")]
        public object ComputerById([FromQuery] Guid id)
        {
            var computer = _context.Computers.FirstOrDefault(c => c.Id == id);

            if (computer != null)
            {
                return Ok(new { message = "Sikeres lekérdezés", result = computer });
            }

            return NotFound(new { message = "Sikeres lekérdezés", result = computer });
        }
    }
}