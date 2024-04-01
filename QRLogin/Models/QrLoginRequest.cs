namespace QRLogin.Models;
public class QrLoginRequest
{
    public required string Payload { get; set; }
    public required string UserId { get; set; }
}

