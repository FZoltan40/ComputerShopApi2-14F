using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
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

            return NotFound(new { message = "Sikertelen lekérdezés", result = computer });
        }

        [HttpPost("newComputer")]
        public object NewComputer([FromBody] NewComputerDto newComputerDto)
        {
            try
            {
                var computer = new Computer
                {
                    Id = Guid.NewGuid(),
                    Brand = newComputerDto.Brand,
                    Display = newComputerDto.Display,
                    Memory = newComputerDto.Memory,
                    CreatedTime = DateTime.Now,
                    OsId = newComputerDto.OsId
                };

                if (computer != null)
                {
                    _context.Computers.Add(computer);
                    _context.SaveChanges();

                    return StatusCode(201, new { message = "Felvéve", result = computer });
                }

                return StatusCode(404, new { message = "Üres objektum", result = computer });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message });
            }
        }

        [HttpPut("updateComputer")]
        public object UpdateComputer([FromQuery] Guid id, [FromBody] UpdateComputerDto updateComputerDto)
        {
            try
            {
                var computer = _context.Computers.SingleOrDefault(c => c.Id == id);

                if (computer != null)
                {
                    computer.Brand = updateComputerDto.Brand;
                    computer.Display = updateComputerDto.Display;
                    computer.Memory = updateComputerDto.Memory;
                    computer.OsId = updateComputerDto.OsId;

                    _context.Computers.Update(computer);
                    _context.SaveChanges();

                    return StatusCode(200, new { message = "Frissítve", result = computer });
                }

                return StatusCode(404, new { message = "nincs ilyen Id", result = computer });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message });
            }
        }

        [HttpDelete("deleteComputer")]
        public object DeleteComputer([FromQuery] Guid id)
        {

            try
            {
                var computer = _context.Computers.Find(id);

                if (computer != null)
                {
                    _context.Computers.Remove(computer);
                    _context.SaveChanges();

                    return StatusCode(200, new { message = "Frissítve", result = computer });

                }

                return StatusCode(404, new { message = "nincs ilyen Id", result = computer });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message });
            }
        }
    }
}