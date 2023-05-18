using BookMyHotel.DTO;
using BookMyHotel.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace BookMyHotel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public HotelsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public Hotel AddHotel(AddHotelDto dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                DistanceCovered = dto.DistanceCovered,
                Location = new Point(dto.Longitude, dto.Latitude)
            };

            appDbContext.Add(hotel);
            appDbContext.SaveChanges();
            return hotel;
        }

        [HttpGet]
        public List<Hotel> GetHotels()
        {
            return appDbContext.Hotels.AsNoTracking().ToList();
        }
    }
}
