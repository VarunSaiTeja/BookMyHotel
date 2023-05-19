using NetTopologySuite.Geometries;
using System.Text.Json.Serialization;

namespace BookMyHotel.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Point Location { get; set; }
        public int DistanceCovered { get; set; }
        public int DeliveryChargePerKM { get; set; }
    }
}
