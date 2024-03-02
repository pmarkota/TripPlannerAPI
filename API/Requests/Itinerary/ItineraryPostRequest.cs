namespace API.Requests.Itinerary
{
    public class ItineraryPostRequest
    {
        public long TripId { get; set; }
        public DateTime Date { get; set; }
        public long ActivityId { get; set; }
        public string Location { get; set; }
    }
}
