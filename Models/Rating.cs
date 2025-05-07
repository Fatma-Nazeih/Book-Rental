namespace BookOrder.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }

}
