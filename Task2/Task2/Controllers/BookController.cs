using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Task2.Database.Entities;
using Task2.Database.Repository;
using Task2.Mappers.DTOs;

namespace Task2.Controllers;

public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public BookController(IGenericRepository<Book> bookRepository, ILogger<BookController> logger, IMapper mapper, IConfiguration configuration)
    {
        _bookRepository = bookRepository;
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("books")]
    public ActionResult<IEnumerable<BookWReviewNumDTO>> GetAll([FromQuery(Name = "order")] string? orderBy)
    {
        var filter = new[] {"author", "title"};
        
        if (orderBy is null || !filter.Contains(orderBy))
            return BadRequest("Invalid order");
        
        var books = _bookRepository.GetAll().Select(_mapper.Map<BookWReviewNumDTO>).ToList();
        
        var result = orderBy == filter[0] 
            ? books.OrderBy(book => book.Author)
            : books.OrderBy(book => book.Title);
        return Ok(result);
    }
    
    [HttpGet("recommended")]
    public ActionResult<IEnumerable<BookWReviewNumDTO>> GetBooksRecommended([FromQuery(Name = "genre")] string? genre)
    {
        var booksCol = _bookRepository.GetAll().ToList();

        if(booksCol.All(book => book.Genre == genre))
            return NotFound("No books with this genre");

        var result = booksCol.Where(book => book.Genre == genre)
            .Select(_mapper.Map<BookWReviewNumDTO>)
            .Where(dto => dto.ReviewsNumber > 10)
            .OrderByDescending(dto => dto.AvgRating)
            .Take(10);
        return Ok(result);
    }

    [HttpPost("books/{id:int}")]
    public async Task<ActionResult<BookWReviewNumDTO>> GetBookDetails(int id)
    {
        var book = await _bookRepository.GetById(id);
        if (book is null)
            return NotFound("Book was not found");

        var result = _mapper.Map<BookDetailReviewDTO>(book);
        return Ok(result);
    }

    [HttpDelete("books/{id:int}")]
    public async Task<ActionResult> DeleteBook([FromQuery(Name = "secret")] string secret, int id)
    {
        var secretKey = _configuration.GetValue<string>("secretKey");

        if (secretKey != secret)
            return BadRequest("Invalid secret key");
        
        var book = await _bookRepository.GetById(id);
        if (book is null)
            return NotFound("Book was not found");

        await _bookRepository.Delete(book);
        return Ok();
    }

    [HttpPost("books/save")]
    public async Task<ActionResult<IdDTO>> SaveBook(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] BookDTO bookDTO)
    {
        var book = _mapper.Map<Book>(bookDTO);

        if (bookDTO.BookId is not null)
            await _bookRepository.Update(book);
        else
            await _bookRepository.Insert(book);

        return Ok(_mapper.Map<IdDTO>(book));
    }
    
    [HttpPut("books/{id:int}/review")]
    public async Task<ActionResult<IdDTO>> SaveReview(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] ReviewDTO reviewDTO, int id)
    {
        var bookSource = await _bookRepository.GetById(id);
        var review = _mapper.Map<Review>(reviewDTO);

        if (bookSource is null)
            return BadRequest("Invalid book id");
        
        bookSource.Reviews.Add(review);
        await _bookRepository.Update(bookSource);

        return Ok(_mapper.Map<IdDTO>(review));
    }

    [HttpPut("books/{id:int}/rate")]
    public async Task<ActionResult<IdDTO>> RateBook(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] RatingDTO ratingDTO, int id)
    {
        var bookSource = await _bookRepository.GetById(id);
        var rating = _mapper.Map<Rating>(ratingDTO);
        
        if(bookSource is null)
            return BadRequest("Invalid book id");
        
        bookSource.Ratings.Add(rating);
        await _bookRepository.Update(bookSource);
        
        return Ok(_mapper.Map<IdDTO>(rating));
    }
}