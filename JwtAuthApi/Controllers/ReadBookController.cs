using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JwtAuthApi.Models;
using JwtAuthApi.Data;
using JwtAuthApi.DTOs;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
    
[ApiController]
[Route("api/[controller]")]
public class ReadBooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReadBooksController(AppDbContext context) => _context = context;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReadBookDto>>> GetReadBooks()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var readBooks = await _context.ReadBooks
            .Include(rb => rb.Book)
            .Where(rb => rb.UserId == userId)
            .Select(rb => new ReadBookDto
            {
                Id = rb.Id,
                BookId = rb.BookId,
                Rating = rb.Rating,
                Comment = rb.Comment,
                UserId = rb.UserId,
                BookTitle = rb.Book.Title
            })
            .ToListAsync();

        return Ok(readBooks);
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReadBook(ReadBookDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var readBook = new ReadBook
        {
            
            UserId = userId,
            BookId = dto.BookId,
            Rating = dto.Rating,
            Comment = dto.Comment
        };

        _context.ReadBooks.Add(readBook);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteReadBook(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var userIsAdmin = User.IsInRole("Admin");

        var readBook = await _context.ReadBooks.FirstOrDefaultAsync(rb => rb.Id == id);

        if (readBook == null)
            return NotFound("Livro lido não encontrado.");

        if (!userIsAdmin && readBook.UserId != userId)
            return Forbid("Você não tem permissão para remover esta review.");

        _context.ReadBooks.Remove(readBook);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("book/{bookId}")]
    [Authorize]
    public async Task<IActionResult> GetUserReviewsForBook(int bookId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var readBooks = await _context.ReadBooks
            .Where(rb => rb.UserId == userId && rb.BookId == bookId)
            .Include(rb => rb.Book)
            .Include(rb => rb.User)
            .Select(rb => new ReadBookDto
            {
                Id = rb.Id,
                BookId = rb.BookId,
                Rating = rb.Rating,
                Comment = rb.Comment,
                UserId = rb.UserId,
                BookTitle = rb.Book.Title
            })
            .ToListAsync();

        if (!readBooks.Any())
            return NotFound();

        return Ok(readBooks);
    }



}
