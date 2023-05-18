using NetTopologySuite.Geometries;

namespace BookMyHotel.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Polygon Area { get; set; }
    }
}
