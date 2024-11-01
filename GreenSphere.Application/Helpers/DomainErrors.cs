﻿namespace GreenSphere.Application.Helpers;
public static class DomainErrors
{
    public static class User
    {
        public const string UnableToCreateAccount = "Some errors happened when creating your account, please try again !!";
        public const string UnkownUser = "Unknown User.";
        public const string UnableToUpdateUser = "Unable to Update The User.";
        public const string InvalidAuthCode = "Invalid authentication code.";
        public const string AuthCodeExpired = "Authentication code is expired.";
        public const string AlreadyEmailConfirmed = "Email is cofirmed yet.";
        public const string UserNotFound = "User '{0}' not found.";
        public const string CannotCreateFbUser = "Can not create facebook user.";
        public const string FbFailedAuthentication = "Facebook authentication failed!";
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
