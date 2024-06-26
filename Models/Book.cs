﻿namespace GestaoLivraria.Models;

public class Book
{
    public Guid Id { get; init; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public double Preco { get; set; }
    public int QtdEstoque { get; set; }

    public Book()
    {
        Id = Guid.NewGuid();
    }
}
