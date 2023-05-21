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
        private readonly AppDbContext appDb;
        private readonly LiteDbContext liteDb;
        GeometryFactory gf;

        public HotelsController(AppDbContext appDbContext, LiteDbContext liteDb)
        {
            this.appDb = appDbContext;
            this.liteDb = liteDb;
            gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<Hotel> AddHotel(AddHotelDto dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                DistanceCovered = dto.DistanceCovered,
                Location = gf.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude)),
                DeliveryChargePerKM = dto.DeliveryChargePerKm
            };

            if (!appDb.Cities.Any(c => c.Area.Contains(hotel.Location)))
                return BadRequest("No City registered with given hotel location");

            appDb.Add(hotel);
            appDb.SaveChanges();
            return Ok(hotel);
        }

        [HttpGet]
        public List<Hotel> GetHotels()
        {
            return liteDb.Hotels.AsNoTracking().ToList();
        }

        [HttpGet]
        public List<Hotel> GetHotelsInCity(Guid cityId)
        {
            var query = from h in appDb.Hotels
                        from c in appDb.Cities
                        where c.Id == cityId && c.Area.Contains(h.Location)
                        select h;

            return query.AsNoTracking().ToList();
        }

        [HttpGet]
        public List<NearestHotelDto> FindNearestHotels(double latitude, double longitude, int preferredDistance = 0)
        {
            var userLocation = gf.CreatePoint(new Coordinate(longitude, latitude));
            IQueryable<NearestHotelDto> query;
            if (preferredDistance == 0)
            {
                query = from h in appDb.Hotels
                        let distance = h.Location.Distance(userLocation)
                        where distance < h.DistanceCovered
                        orderby distance
                        select new NearestHotelDto
                        {
                            Id = h.Id,
                            Name = h.Name,
                            DeliveryAvailable = true,
                            _distance = distance,
                            _deliveryCharge = (distance / 1000) * h.DeliveryChargePerKM
                        };
            }
            else
            {
                query = from h in appDb.Hotels
                        let distance = h.Location.Distance(userLocation)
                        let deliveryAvailable = distance < h.DistanceCovered
                        where distance < preferredDistance
                        orderby distance
                        select new NearestHotelDto
                        {
                            Id = h.Id,
                            Name = h.Name,
                            DeliveryAvailable = deliveryAvailable,
                            _distance = distance,
                            _deliveryCharge = deliveryAvailable ? (distance / 1000) * h.DeliveryChargePerKM : 0
                        };
            }

            return query.Take(10).ToList();
        }
    }
}
