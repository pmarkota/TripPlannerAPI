namespace API.Requests.Itinerary
{
    public class ItineraryPutRequest
    {
        public long? Id { get; set; }
        public long? TripId { get; set; }
        public DateTime? Date { get; set; }
        public long? ActivityId { get; set; }
        public string? Location { get; set; }
    }
}
