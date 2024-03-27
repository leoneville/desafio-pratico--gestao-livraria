namespace GestaoLivraria.Communication.Responses;

public class DefaultResponse
{
    public string Msg { get; set; } = string.Empty;

    public DefaultResponse(string msg)
    {
        Msg = msg;
    }
}
