using Homework2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Book> GetByQueryString(int? index)
        {
            try
            {
                if (index == null)
                {
                    return BadRequest("Index is a required parameter!");
                }

                if (index < 0)
                {
                    return BadRequest("The index cannot be negative!");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }
                return Ok(StaticDb.Books[index.Value]);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("multipleQuery")]
        public ActionResult<List<Book>> FilterBookByAuthorTitle(string? author, string? title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return BadRequest("You need to send at least one filter parameter!");
                }
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> filteredBooksByTitle = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return Ok(filteredBooksByTitle);
                }
                if (string.IsNullOrEmpty(title))
                {
                    List<Book> filteredBooksByAuthor = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return Ok(filteredBooksByAuthor);
                }

                List<Book> filteredBooks =
                    StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())
                      && x.Author.ToLower().Contains(author.ToLower())).ToList();
                return Ok(filteredBooks);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddNewBook([FromBody] Book book)
        {
            try
            {
                if (book == null || string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Both Title and Author are required.");
                }

                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book Added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("titles")]
        public IActionResult AcceptListOfBooks([FromBody] List<Book> books)
        {
            try
            {
                if (books == null || books.Count == 0)
                {
                    return BadRequest("At least one book is required.");
                }

                List<string> titles = books.Select(book => book.Title).ToList();
                return Ok(titles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
