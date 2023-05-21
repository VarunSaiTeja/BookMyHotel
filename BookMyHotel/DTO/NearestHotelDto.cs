using System.Text.Json.Serialization;

namespace BookMyHotel.DTO
{
    public class NearestHotelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool DeliveryAvailable { get; set; }

        public double Distance => Math.Round(_distance / 1000, 2);
        public double DeliveryCharge => Math.Round(_deliveryCharge, 2);

        [JsonIgnore]
        public double _distance { get; set; }
        [JsonIgnore]
        public double _deliveryCharge { get; set; }
    }
}
