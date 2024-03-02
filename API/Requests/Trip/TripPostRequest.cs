namespace API.Requests.Trip
{
    public class TripPostRequest
    {
        public string Purpose { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
    }
}
