namespace API.Requests.Activity
{
    public class ActivityPutRequest
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
