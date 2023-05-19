using BookMyHotel.DTO;
using BookMyHotel.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace BookMyHotel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly LiteDbContext liteDb;
        GeometryFactory gf;

        public CitiesController(AppDbContext appDbContext, LiteDbContext liteDb)
        {
            this.appDb = appDbContext;
            this.liteDb = liteDb;
            gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        }

        [HttpPost]
        public City AddCity(AddCityDto dto)
        {
            var coOrds = dto.CoOrdinates.Select(c => new Coordinate(c.First(), c.Last())).ToArray();
            var area = gf.CreatePolygon(coOrds);
            area.Normalize();
            var city = new City
            {
                Name = dto.Name,
                Area = area.Reverse()
            };
            appDb.Add(city);
            appDb.SaveChanges();
            return city;
        }

        [HttpGet]
        public List<City> GetCities()
        {
            return liteDb.Cities.AsNoTracking().ToList();
        }
    }
}
