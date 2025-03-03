public record TraineeshipSupervisorRequest
(
    string FullName,
    string PhoneNumber, 
    int? SupervisorTypeId, 
    int? OrganizationId 
);