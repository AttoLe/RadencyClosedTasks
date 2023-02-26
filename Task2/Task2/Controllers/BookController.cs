using Task2.DTOs;
using Task2.Entities;
using Task2.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task2.Controllers;

public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IGenericRepository<Book> _repository;
    private readonly IMapper _mapper;
    
    public BookController(IGenericRepository<Book> repository, ILogger<BookController> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("books")]
    public async Task<ActionResult<IEnumerable<BookWReviewNumDTO>>> GetAll([FromQuery(Name = "order")] string? orderBy)
    {
        var booksCol = _repository.GetAll();
        var filter = new[] {"author", "title"};
        
        if (orderBy is null || !filter.Contains(orderBy))
            return BadRequest("Provider order is invalid");

        var books = (await booksCol).Select(_mapper.Map<BookWReviewNumDTO>).ToList();
        
        var result = orderBy == filter[0] 
            ? books.OrderBy(book => book.Author)
            : books.OrderBy(book => book.Title);
        return Ok(result);
    }
    
    [HttpGet("recommended")]
    public async Task<ActionResult<IEnumerable<BookWReviewNumDTO>>> GetBooksRecommended([FromQuery(Name = "genre")] string? genre)
    {
        var booksCol = (await _repository.GetAll()).ToList();

        if(booksCol.All(book => book.Genre == genre))
            return NotFound("No genre");

        var result = booksCol.Where(book => book.Genre == genre)
            .Select(_mapper.Map<BookWReviewNumDTO>)
            .Where(dto => dto.ReviewsNumber > 10)
            .OrderByDescending(dto => dto.AvgRating).Take(10);
        return Ok(result);
    }

    [HttpPost("books/{id:int}")]
    public async Task<ActionResult<BookWReviewNumDTO>> GetBookDetails(int id)
    {
        var book = await _repository.GetById(id);
        if (book is null)
            return NotFound("Book was not found");

        var result = _mapper.Map<BookDetailReviewDTO>(book);
        return Ok(result);
    }
    
    /*
     * delete with secret key
     */

    [HttpPost("/books/save")]
    public async Task<ActionResult> SaveBook([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] BookDTO bookDTO)
    {
        var book = _mapper.Map<Book>(bookDTO);

        if (bookDTO.BookId is not null)
            await _repository.Update(book);
        else
            await _repository.Insert(book);

        return Ok(_mapper.Map<IdDTO>(book));
    }
    
    [HttpPost("/books/{id:int}/review")]
    public async Task<ActionResult> SaveReview([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] ReviewDTO reviewDto, int id)
    {
        var bookSource = await _repository.GetById(id);
        var review = _mapper.Map<Review>(reviewDto);
        
        if (bookSource is null)
            return BadRequest("Provided book id is invalid");
        
        bookSource.Reviews.Add(review);
        await _repository.Update(bookSource);

        return Ok(_mapper.Map<IdDTO>(review));
    }
}