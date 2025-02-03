namespace GreenSphere.Application.DTOs.Users
{
    public class ChangeUserEmailDto
    {
        public string NewEmail { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
        public string Code { get; set; }

    }
}
