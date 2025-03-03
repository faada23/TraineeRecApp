using Api.Application.interfaces;
using API.Application.DTOs;

namespace Api.Application.Services;

public class AuthService : IAuthService
{
    public AuthService()
    {

    }
    
    public async Task<string?> Login(LoginRequest userRequest)
    {       
      throw new NotImplementedException();
    }

    public async Task<int> CreateUser(CreateUserRequest userRequest)
    {       
      throw new NotImplementedException();
    }
}
