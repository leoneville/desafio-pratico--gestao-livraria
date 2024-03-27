namespace GestaoLivraria.Communication.Requests;

public class RequestRegisterBookJson
{
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public double Preco { get; set; }
    public int QtdEstoque { get; set; }
}
