using NetTopologySuite.Geometries;
using System.Text.Json.Serialization;

namespace BookMyHotel.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Geometry Area { get; set; }
    }
}
