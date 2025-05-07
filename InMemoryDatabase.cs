using BookOrder.Models;

namespace BookOrder
{
    public static class InMemoryDatabase
    {
        public static List<Order> Orders { get; } = new();
        public static List<Rating> Ratings { get; } = new();
    }

}
