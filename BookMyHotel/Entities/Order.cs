using NetTopologySuite.Geometries;

namespace BookMyHotel.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Point DeliveryAddress { get; set; }
        public long Distance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
