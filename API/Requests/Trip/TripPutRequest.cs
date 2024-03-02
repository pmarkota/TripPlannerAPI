namespace API.Requests.Trip
{
    public class TripPutRequest
    {
        public long Id { get; set; }
        public string Purpose { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
