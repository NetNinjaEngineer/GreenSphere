namespace GreenSphere.Application.Interfaces.Services;
public interface ICurrentUser
{
    string Id { get; }
    string Email { get; }
}
