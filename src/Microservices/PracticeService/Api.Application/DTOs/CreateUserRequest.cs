namespace API.Application.DTOs;

public record CreateUserRequest(
    string FullName, 
    string Email,
    string Password,
    int RoleId,
    StudentRequest? Student,
    TraineeshipSupervisorRequest? TraineeshipSupervisor
);