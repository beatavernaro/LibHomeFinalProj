using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibHomeFinalProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class BooksController : ControllerBase
    {

        private readonly DataContext _context;
        public BooksController(DataContext context)
        {
            _context = context;
        }

        #region "Get All"
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            return Ok(await _context.Books.ToListAsync());
        }
        #endregion

        #region "Get By Year"
        [HttpGet("year/{year}")]
        public async Task<ActionResult<Book>> GetByYear(string year) //se tem parametro tem que adicionar a rota tbm
        {
            var oneBook =  _context.Books.Where(x => x.PublishedDate == year).OrderBy(x => x.Title);
            if (oneBook == null)
                return BadRequest("Book not found");
            return Ok(oneBook);
        }
        #endregion

        #region "Get By Author"
        [HttpGet("author/{name}")]
        public async Task<ActionResult<Book>> GetByAuthor(string name) //se tem parametro tem que adicionar a rota tbm
        {
            var oneBook = _context.Books.Where(x => x.Authors.Contains(name)).OrderBy(x => x.PublishedDate).OrderBy(x => x.Authors);
            if (oneBook == null)
                return BadRequest("Book not found");
            return Ok(oneBook);
        }
        #endregion

        #region "Get By Title"
        [HttpGet("title/{title}")]
        public async Task<ActionResult<Book>> GetByTitle(string title) //se tem parametro tem que adicionar a rota tbm
        {
            var oneBook = _context.Books.Where(x => x.Title.Contains(title)).OrderBy(x => x.Id).OrderBy(x => x.Title);
            if (oneBook == null)
                return BadRequest("Book not found");
            return Ok(oneBook);
        }
        #endregion

        #region "Get By ID"
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Book>> GetById(int id) //se tem parametro tem que adicionar a rota tbm
        {
            var oneBook = _context.Books.Where(x => x.Id == id);
            if (oneBook == null)
                return BadRequest("Book not found");
            return Ok(oneBook);
        }
        #endregion

        #region "Add Book"
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return Ok(await _context.Books.ToListAsync());
        }
        #endregion

        #region "Uptade Book"
        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook([FromBody] Book request)
        {
            var dbBook = await _context.Books.FindAsync(request.Id);
            if (dbBook == null)
                return BadRequest("Book not found");

            dbBook.Title = request.Title;
            dbBook.Authors = request.Authors;
            dbBook.PublishedDate = request.PublishedDate;
            dbBook.Categorie = request.Categorie;
            

            await _context.SaveChangesAsync();

            return Ok(await _context.Books.ToListAsync());
        }
        #endregion

        #region "Delete Book"
        [HttpDelete("del/{id}")]
        public async Task<ActionResult<List<Book>>> DeleteOne(int id) //se tem parametro tem que adicionar a rota tbm
        {
            var dbBook = await _context.Books.FindAsync(id);
            if (dbBook == null)
                return BadRequest("Book not found");

            _context.Books.Remove(dbBook);
            await _context.SaveChangesAsync();
            return Ok(await _context.Books.ToListAsync());

        }
        #endregion

    }
}
