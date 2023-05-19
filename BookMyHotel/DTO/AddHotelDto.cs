namespace BookMyHotel.DTO
{
    public class AddHotelDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int DistanceCovered { get; set; }
        public int DeliveryChargePerKm { get; set; }
    }
}
