using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Core.Interfaces;
using GroceryShopSystem.Core.Security;
using MediatR;
using GroceryShopSystem.Application.Security;

namespace GroceryShopSystem.Application.Features.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;
    private readonly JwtTokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher hasher, JwtTokenService tokenService)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        return _tokenService.GenerateToken(user);
    }
}