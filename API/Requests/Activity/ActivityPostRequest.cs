namespace API.Requests.Activity
{
    public class ActivityPostRequest
    {
        public long? TripId { get; set; }
        
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
