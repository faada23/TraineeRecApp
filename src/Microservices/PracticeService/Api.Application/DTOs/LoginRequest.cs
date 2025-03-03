namespace API.Application.DTOs;

public record LoginRequest(
    string Email,
    string Password
);