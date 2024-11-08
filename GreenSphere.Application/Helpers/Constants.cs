namespace GreenSphere.Application.Helpers;
public static class Constants
{
    public const string ConfirmEmailCodeSentSuccessfully = "The Confirm email code sent to your email successfully, check your inbox.";
    public const string EmailConfirmed = "User email is confirmed.";
    public const string AuthCodeExpireKey = "AuthCodeExpirationInMinutes";
    public const string EmailSent = "The email was sent successfully!";
    public const string EmailNotSent = "Unfortunately, the email could not be sent.";
    public const string TwoFactorCodeSent = "2FA code is sent, please validate 2FA code to complete the setup.";
    public const string TwoFactorEnabled = "Your 2FA authentication is successfully enabled.";
    public const string Disable2FASuccess = "Two-factor authentication has been disabled successfully.";
    public static class User
    {
        public const string CanViewProfilePolicy = "CanViewProfile";
    }

    public static class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";
        public const string RoleCreatedSuccessfully = "Role '{0}' created successfully.";
        public const string RoleUpdatedSuccessfully = "Role '{0}' updated successfully.";
        public const string RoleDeletedSuccessfully = "Role '{0}' deleted successfully.";
        public const string RoleAssignedSuccessfully = "Role '{0}' assigned successfully to user '{1}'.";
        public const string ClaimAddedSuccessfully = "Claim '{0}:{1}' added successfully to user '{2}'.";
        public const string ClaimAddedToRoleSuccessfully = "Claim '{0}:{1}' added successfully to role '{2}'.";
    }
}