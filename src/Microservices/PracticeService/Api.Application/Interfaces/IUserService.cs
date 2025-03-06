using Api.Core.AdditionalClasses;
using API.Application.DTOs;

namespace Api.Application.interfaces;

public interface IUserService 
{   
    public Task<Result<int>> CreateUser(CreateUserRequest userRequest);
}