using Application.Dto.User;

namespace Application.Contracts.Application;

public interface IUserService
{
    Task Register(RegisterDto dto);
    Task<string> Login(LoginDto dto);
}