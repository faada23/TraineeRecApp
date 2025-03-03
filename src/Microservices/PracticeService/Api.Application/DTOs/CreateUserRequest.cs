namespace API.Application.DTOs;

public record CreateUserRequest(
    string FullName, 
    string Email,
    string NotPassword,
    int RoleId,
    StudentRequest? Student,
    TraineeshipSupervisorRequest? TraineeshipSupervisor
);