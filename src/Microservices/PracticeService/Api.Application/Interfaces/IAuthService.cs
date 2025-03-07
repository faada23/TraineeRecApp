using Api.Core.AdditionalClasses;
using API.Application.DTOs;

namespace Api.Application.interfaces;

public interface IAuthService 
{   
    public Task<Result<string?>> Login(LoginRequest userRequest);
}