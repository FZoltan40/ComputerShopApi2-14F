using ComputerShopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Controllers
{
    [Route("oscomp")]
    [ApiController]
    public class OsCompController : ControllerBase
    {
        public ComputerShopDbContext _context = new ComputerShopDbContext();

        //Az összes os és a hozzátartozó összes cmp
        [HttpGet("allOsAndCopmuter")]
        public object AllOsAndCopmuter()
        {
            var result = _context.Osystems.Include(o => o.Computers);

            return new { message = result };
        }

        //Adott os a hozzátartozó computerekkel
        [HttpGet("osByIdWithComputers")]
        public object OsByIdWithComputers([FromQuery] Guid id)
        {
            var result = _context.Osystems.Include(o => o.Computers)
                .Select(o => new
                {
                    o.Id,
                    o.Name,
                    o.Version,
                    Computer = o.Computers
                    .Select(c => new { c.Brand, c.Display })
                })
                .SingleOrDefault(o => o.Id == id);

            return new { message = result };
        }
    }
}
