namespace GreenSphere.Application.Helpers;

public static class DomainErrors
{
    public static class User
    {
        public const string UnableToCreateAccount =
            "Some errors happened when creating your account, please try again !!";

        public const string UnkownUser = "Unknown User.";
        public const string UnableToUpdateUser = "Unable to Update The User.";
        public const string InvalidAuthCode = "Invalid authentication code.";
        public const string AuthCodeExpired = "Authentication code is expired.";
        public const string AlreadyEmailConfirmed = "Email is cofirmed yet.";
        public const string UserNotFound = "User '{0}' not found.";
        public const string EmailNotFound = "Email '{0}' not found.";
        public const string EmailInUse = "Email '{0}' is already in use";
        public const string CannotCreateFbUser = "Can not create facebook user.";
        public const string FbFailedAuthentication = "Facebook authentication failed!";
        public const string EmailNotConfirmed = "Email is not confirmed.";
        public const string InvalidCredientials = "Invalid email or password.";
        public const string CodeExpired = "Code has expired. Please request a new reset code.";
        public const string UserHasPrivacy = "User has a privacy setting.";
        public const string UserNotHasPrivacySetting = "User not have privacy settings";
        public const string Invalid2FaCode = "Invalid 2FA Code.";
        public const string InvalidTokenProvider = "Invalid 2FA Token Provider.";
        public const string InvalidCurrentPassword = "Current Password is not valid";
        public const string FailedToChangeEmail = "Change email is not updated";
        public const string TwoFactorRequired =
            "Two Factor Authentication Required To Complete Login, check your inbox and verify your 2fa code.";

        public const string TwoFactorAlreadyDisabled = "Two-factor authentication is already disabled for this user.";
        public const string Disable2FaFailed = "Failed to disable two-factor authentication.";
    }


    public static class Roles
    {
        public const string ErrorCreatingRole = "Error creating role '{0}'.";
        public const string RoleNotFound = "Role not found: '{0}'.";
        public const string ErrorUpdatingRole = "Error updating role '{0}'.";
        public const string ErrorDeletingRole = "Error deleting role '{0}'.";
        public const string ErrorAssigningRole = "Error assigning role '{0}' to user '{1}'.";
        public const string ErrorAddingClaim = "Error adding claim to {0}'.";
        public const string ErrorAddingClaimToRole = "Error adding claim to '{0}' role.";
    }
}