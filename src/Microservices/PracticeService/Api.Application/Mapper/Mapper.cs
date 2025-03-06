using System.Runtime.InteropServices;
using Api.Core.Entities;
using API.Application.DTOs;

public static class Mapper{
    public static User ToUser(this CreateUserRequest request)
    {
        return new User
        {
            Fullname = request.FullName,
            Email = request.Email,
            Passwordhash = request.Password, 
            RoleId = request.RoleId,
            Student = request.Student?.ToStudent(),
            Traineeshipsupervisor = request.TraineeshipSupervisor?.ToTraineeshipSupervisor()
        };
    }

    public static Student ToStudent(this StudentRequest request)
    {
        return new Student
        {
            Birthday = request.Birthday,
            Phonenumber = request.PhoneNumber,
            Course = request.Course,
            Fullname = request.FullName,
            GroupnameId = request.GroupId,
            SpecialityId = request.SpecialityId
        };
    }

    public static Traineeshipsupervisor ToTraineeshipSupervisor(this TraineeshipSupervisorRequest request)
    {       
        return new Traineeshipsupervisor
        {
            Fullname = request.FullName,
            Phonenumber = request.PhoneNumber,
            SupervisortypeId = request.SupervisorTypeId,
            OrganizationId = request.OrganizationId
        };
    }
}