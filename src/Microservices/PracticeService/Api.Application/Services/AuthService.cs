using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Api.Application.Enums;
using Api.Application.interfaces;
using Api.Core.AdditionalClasses;
using Api.Core.Entities;
using API.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Api.Application.Services;

public class AuthService : IAuthService
{	
	public IRepository<User> UserRepository {get;set;}
	public IJwtProvider JwtProvider {get;set;}
    public AuthService(IJwtProvider jwtProvider)
	{
		JwtProvider = jwtProvider;
	}
  
	public async Task<Result<string?>> Login(LoginRequest userRequest)
	{   
		var user = (await UserRepository.FindBy(x => x.Email == userRequest.Email)).Data;

		if(user != null)
		{
			var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.Passwordhash, userRequest.Password);
			if(result == PasswordVerificationResult.Success)
			{
				var token = JwtProvider.GenerateToken(user);
				return Result<string?>.Success(token,"Token generated successfuly");

			}
			return Result<string?>.Failure("Wrong Password");
		}
		return Result<string?>.Failure("User was not Found");
	}

}
