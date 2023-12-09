namespace CamplyMarket.Application.Abstraction.Token;

public interface ITokenHandler  
{
    DTOs.Token CreateAccessToken(int second);
}
