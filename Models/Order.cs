namespace BookOrder.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<int> BookIds { get; set; } = new();
    }

}
