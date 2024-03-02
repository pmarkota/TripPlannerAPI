namespace API.Requests.PackingList
{
    public class PackingListPutRequest
    {
        public long? Id { get; set; }
        public long? TripId { get; set; }
        public string? Item { get; set; }
        public int? Quantity { get; set; }
        public string? Category { get; set; }

    }
}
