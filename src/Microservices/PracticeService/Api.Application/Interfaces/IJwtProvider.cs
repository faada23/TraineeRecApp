using Api.Core.Entities;

public interface IJwtProvider 
{
    public string? GenerateToken(User user);
}