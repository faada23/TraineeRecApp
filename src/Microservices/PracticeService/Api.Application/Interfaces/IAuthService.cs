using API.Application.DTOs;

namespace Api.Application.interfaces;

public interface IAuthService 
{   
    public Task<int> CreateUser(CreateUserRequest userRequest);
    public Task<string?> Login(LoginRequest userRequest);
}