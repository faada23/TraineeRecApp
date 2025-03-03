public record StudentRequest
(
    DateTime? Birthday,
    string PhoneNumber, 
    string Course,
    string FullName,
    int? GroupId, 
    int? SpecialityId 
);