public record StudentRequest
(
    DateOnly Birthday,
    string PhoneNumber, 
    char Course,
    string FullName,
    int GroupId, 
    int SpecialityId 
);