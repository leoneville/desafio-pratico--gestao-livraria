using GestaoLivraria.Communication.Requests;
using GestaoLivraria.Communication.Responses;
using GestaoLivraria.Data;
using GestaoLivraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GestaoLivraria.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : GestaoLivrariaBaseController
{
    [HttpPost]
    [SwaggerOperation(Summary = "Efetua o registro de um livro no sistema")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostBook(RequestRegisterBookJson request, AppDbContext context, CancellationToken ct)
    {
        var book = new Book()
        {
            Titulo = request.Titulo,
            Autor = request.Autor,
            Genero = request.Genero,
            Preco = request.Preco,
            QtdEstoque = request.QtdEstoque,
        };

        await context.Books.AddAsync(book, ct);
        await context.SaveChangesAsync(ct);

        return Created(string.Empty, book);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retorna uma lista com todos os livros cadastrados no sistema")]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBooks(AppDbContext context, CancellationToken ct)
    {
        var books = await context.Books.ToListAsync(ct);

        return Ok(books);
    } 

    [HttpGet]
    [SwaggerOperation(Summary = "Retorna as informações de um livro cadastrado no sistema pelo ID")]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOneBook(Guid id, AppDbContext context, CancellationToken ct)
    {
        var book = await context.Books.FirstOrDefaultAsync(book => book.Id == id, ct);
        if (book == null)
            return NotFound(new DefaultResponse("Livro não encontrado!"));

        return Ok(book);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Efetua a atualização das informações de um livro pelo ID")]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutBook(Guid id, [FromBody] RequestUpdateBookJson request, AppDbContext context, CancellationToken ct)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (book == null)
            return NotFound(new DefaultResponse("Livro não encontrado!"));

        book.Titulo = request.Titulo;
        book.Autor = request.Autor;
        book.Genero = request.Genero;
        book.Preco = request.Preco;
        book.QtdEstoque = request.QtdEstoque;

        await context.SaveChangesAsync(ct);

        return Ok(book);
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Efetua a exclusão de um livro no sistema pelo ID")]
    [Route("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(Guid id, AppDbContext context, CancellationToken ct)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (book == null)
            return NotFound(new DefaultResponse("Livro não encontrado!"));

        context.Books.Remove(book);

        await context.SaveChangesAsync(ct);

        return NoContent();
    }
}
