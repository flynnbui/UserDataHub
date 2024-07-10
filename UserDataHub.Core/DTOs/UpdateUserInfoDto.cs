using UserDataHub.Core.Entities;

namespace UserDataHub.Core.DTOs
{
    public record UpdateUserInfoDto(
        string EnrollmentFormCode,
        UserData UserData
    );
}
