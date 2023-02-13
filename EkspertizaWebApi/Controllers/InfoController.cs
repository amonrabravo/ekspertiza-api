using EkspertizaWebApiData;
using EkspertizaWebApiData.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EkspertizaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly AppDbContext context;

        public InfoController(
            AppDbContext context
            )
        {
            this.context = context;
        }

        [HttpGet("provinces")]
        public async Task<ActionResult> GetProvinces() => Ok(await context.Provinces.OrderBy(p => p.Name).Select(p => new { p.Id, p.Name }).ToListAsync());


        [HttpGet("cities/{id}")]
        public async Task<ActionResult> GetCities(int id) => Ok(await context.Cities.Where(p=>p.ProvinceId == id).OrderBy(p => p.Name).Select(p => new { p.Id, p.Name }).ToListAsync());

        [HttpGet("services/{id}")]
        public async Task<ActionResult> GetServices(int id) => Ok(new[]
        {
            new { Id = "1", Name = "Loli Ekspertiz", Address = "Lorem ipsum dolor sit amet. 1", Raiting = 1, latitude = 41.0099049, longitude = 29.1325156 },
            new { Id = "2", Name = "Elek Ekspertiz", Address = "Lorem ipsum dolor sit amet. 2", Raiting = 4, latitude = 41.00999049, longitude = 29.1323156 },
            new { Id = "3", Name = "Kibolo Ekspertiz", Address = "Lorem ipsum dolor sit amet. 3", Raiting = 5, latitude = 41.0098049, longitude = 29.13275156 },
            new { Id = "4", Name = "Tulloc Ekspertiz", Address = "Lorem ipsum dolor sit amet. 4", Raiting = 5, latitude = 41.0099449, longitude = 29.1345156 },
            new { Id = "5", Name = "Nimsinz Ekspertiz", Address = "Lorem ipsum dolor sit amet. 5", Raiting = 3, latitude = 41.0093049, longitude = 29.1365156 },
            new { Id = "6", Name = "Ruftur Ekspertiz", Address = "Lorem ipsum dolor sit amet. 6", Raiting = 2, latitude = 41.0099949, longitude = 29.13295156 },
            new { Id = "7", Name = "Komo Ekspertiz", Address = "Lorem ipsum dolor sit amet. 1", Raiting = 1, latitude = 41.0099049, longitude = 29.1325156 },
            new { Id = "8", Name = "Amsterdam Ekspertiz", Address = "Lorem ipsum dolor sit amet. 2", Raiting = 4, latitude = 41.00999049, longitude = 29.1323156 },
            new { Id = "9", Name = "Londra Ekspertiz", Address = "Lorem ipsum dolor sit amet. 3", Raiting = 5, latitude = 41.0098049, longitude = 29.13275156 },
            new { Id = "10", Name = "Kabil Ekspertiz", Address = "Lorem ipsum dolor sit amet. 4", Raiting = 5, latitude = 41.0099449, longitude = 29.1345156 },
            new { Id = "11", Name = "Cape Town Ekspertiz", Address = "Lorem ipsum dolor sit amet. 5", Raiting = 3, latitude = 41.0093049, longitude = 29.1365156 },
            new { Id = "12", Name = "Tokyo Ekspertiz", Address = "Lorem ipsum dolor sit amet. 6", Raiting = 2, latitude = 41.0099949, longitude = 29.13295156 },

        }.OrderByDescending(p=>p.Raiting).ToList());
    }
}
