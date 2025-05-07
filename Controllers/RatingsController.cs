using BookOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private static int _ratingCounter = 1;

        [HttpPost]
        public IActionResult AddRating([FromBody] Rating rating)
        {
            if (rating.Stars < 1 || rating.Stars > 5)
                return BadRequest("Stars must be between 1 and 5.");

            bool hasRented = InMemoryDatabase.Orders.Any(o =>
                o.UserId == rating.UserId && o.BookIds.Contains(rating.BookId));

            if (!hasRented)
                return BadRequest("User has not rented this book.");

            rating.RatingId = _ratingCounter++;
            InMemoryDatabase.Ratings.Add(rating);
            return Ok(rating);
        }

        [HttpGet("/api/books/{bookId}/ratings")]
        public IActionResult GetRatingsForBook(int bookId)
        {
            var ratings = InMemoryDatabase.Ratings.Where(r => r.BookId == bookId).ToList();
            return Ok(ratings);
        }

        [HttpGet("/api/books/{bookId}/ratingSummary")]
        public IActionResult GetRatingSummary(int bookId)
        {
            var bookRatings = InMemoryDatabase.Ratings.Where(r => r.BookId == bookId).ToList();
            if (bookRatings.Count == 0)
                return Ok(new { Average = 0, Count = 0 });

            double average = bookRatings.Average(r => r.Stars);
            return Ok(new { Average = Math.Round(average, 2), Count = bookRatings.Count });
        }
    }


}
