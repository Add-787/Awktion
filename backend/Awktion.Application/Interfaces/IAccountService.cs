using Awktion.Application.Dtos.Login;
using Awktion.Application.Dtos.Register;

namespace Awktion.Application.Interfaces;

public interface IAccountService
{
    Task<LoginResponse> AuthenticateAsync(LoginRequest request);
    Task<string> RegisterAsync(RegisterRequest request);
}
