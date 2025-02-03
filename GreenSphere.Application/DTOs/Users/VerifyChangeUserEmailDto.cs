namespace GreenSphere.Application.DTOs.Users
{
    public class VerifyChangeUserEmailDto
    {
        public string Code { get; set; } = null!;
        public string NewEmail { get; set; } = null!;
    }
}
