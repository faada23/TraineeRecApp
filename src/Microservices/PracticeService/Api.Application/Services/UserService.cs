using Api.Application.Enums;
using Api.Application.interfaces;
using Api.Core.AdditionalClasses;
using Api.Core.Entities;
using API.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Api.Application.Services;

public class UserService : IUserService
{	
	public IRepository<User> UserRepository {get;set;}
	public IRepository<Student> StudentRepository {get;set;}
	public IRepository<Traineeshipsupervisor> TraineeshipSupervisorRepository {get;set;}
	public IRepository<Role> RoleRepository {get;set;}
	public IJwtProvider JwtProvider {get;set;}
    public UserService(IJwtProvider jwtProvider)
	{
		JwtProvider = jwtProvider;
	}
  
	public async Task<Result<int>> CreateUser(CreateUserRequest userRequest)
	{   
		var roleResult = await RoleRepository.FindBy(x => x.Id == userRequest.RoleId);
		if (!roleResult.IsSuccess) return Result<int>.Failure($"Role with id {userRequest.RoleId} not found");

		var role = roleResult.Data;

        var user = userRequest.ToUser();

        user.Passwordhash = new PasswordHasher<User>().HashPassword(user,user.Passwordhash);
        var userResult = await UserRepository.Insert(user);
        
        if (!userResult.IsSuccess)
        {
            return userResult;
        }
        
        int userId = userResult.Data;
        
        Result<int> profileResult;
        
        switch (role.Name)
        {
            case var name when name == RolesEnum.Student.ToString():
                profileResult = await CreateStudentProfile(userRequest.Student);
                break;
                
            case var name when name == RolesEnum.UniversitySupervisor.ToString():
                profileResult = await CreateSupervisorProfile(userRequest.TraineeshipSupervisor);
                break;
                
            default:
                return Result<int>.Failure($"Role type '{role.Name}' is not supported for profile creation");
        }
        
        if (!profileResult.IsSuccess)
        {
            await UserRepository.Delete(userId);
            return Result<int>.Failure($"Failed to create profile");
        }
        
        return Result<int>.Success(userId);
	}

	private async Task<Result<int>> CreateStudentProfile(StudentRequest? studentRequest)
	{
		if (studentRequest == null)
		{
			return Result<int>.Failure("Student profile data is required for Student role");
		}
		
		var student = studentRequest.ToStudent();
		
		return await StudentRepository.Insert(student);
	}

	private async Task<Result<int>> CreateSupervisorProfile(TraineeshipSupervisorRequest? supervisorRequest)
	{
		if (supervisorRequest == null)
		{
			return Result<int>.Failure("Supervisor profile data is required for UniversitySupervisor role");
		}
		
		var supervisor = supervisorRequest.ToTraineeshipSupervisor();
		
		return await TraineeshipSupervisorRepository.Insert(supervisor);
	}
}
