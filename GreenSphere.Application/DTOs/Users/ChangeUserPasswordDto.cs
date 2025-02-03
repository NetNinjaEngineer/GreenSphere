namespace GreenSphere.Application.DTOs.Users
{
    public class ChangeUserPasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
