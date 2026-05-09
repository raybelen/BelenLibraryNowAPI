using BelenLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BelenLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Don Quixote", Author = "Miguel de Cervantes", Genre= "Novel", Available = true , PublishedYear = 1605 },
            new Book { Id = 2, Title = "To Kill a Mockingbird ", Author = "Harper Lee", Genre= "Southern Gothic", Available = true , PublishedYear = 1960}
        };
        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrieve." });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });
            return Ok(new { status = "success", data = book, message = "Book retrieved." });

        }
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id },
                new { status = "success", data = newBook, message = "Book created" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book updated" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            books.Remove(book);
            return Ok(new { status = "success", data = (object?)null, message = "Book deleted" });
        }
    }
}
