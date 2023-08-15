using Homework2.Models;

namespace Homework2
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>
        {
            new Book()
            {
              Author = "J.R.R. Tolkien",
              Title = "The Lord of the Rings"
            },
            new Book()
            {
              Author = "Harper Lee",
              Title = "To Kill a Mockingbird"
            },
            new Book()
            {
              Author = "F. Scott Fitzgerald",
              Title = "The Great Gatsby"
            },
        };
    }
}
