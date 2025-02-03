namespace GreenSphere.Application.Interfaces.Identity;
public interface ICurrentUser
{
    string Id { get; }
    string Email { get; }
}
