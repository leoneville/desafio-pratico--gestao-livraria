using GestaoLivraria.Communication.Requests;
using GestaoLivraria.Communication.Responses;
using GestaoLivraria.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoLivraria.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private static List<Book> books = new();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status400BadRequest)]
    public IActionResult PostBook(RequestRegisterBookJson request)
    {            
        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Titulo = request.Titulo,
            Autor = request.Autor,
            Genero = request.Genero,
            Preco = request.Preco,
            QtdEstoque = request.QtdEstoque,
        };

        books.Add(book);

        return Created(string.Empty, book);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult GetAllBooks() => Ok(books);

    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status404NotFound)]
    public IActionResult GetOneBook(Guid id)
    {
        var book = books.Find(x => x.Id == id);
        if (book == null)
            return NotFound(new DefaultResponse("Livro não encontrado!"));

        return Ok(book);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status404NotFound)]
    public IActionResult PutBook(Guid id, [FromBody] RequestRegisterBookJson request)
    {
        var book = books.Find(x => x.Id == id);
        if (book == null)
            return NotFound(new DefaultResponse("Livro não encontrado!"));

        book.Titulo = request.Titulo;
        book.Autor = request.Autor;
        book.Genero = request.Genero;
        book.Preco = request.Preco;
        book.QtdEstoque = request.QtdEstoque;

        return Ok(new DefaultResponse("Livro atualizado com sucesso!"));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status404NotFound)]
    public IActionResult DeleteBook(Guid id)
    {
        var book = books.Find(x => x.Id == id);
        if (book == null)
            return NotFound(new DefaultResponse("Livro não encontrado!"));

        books.Remove(book);

        return NoContent();
    }
}
