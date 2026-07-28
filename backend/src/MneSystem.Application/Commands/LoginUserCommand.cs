using MediatR;

namespace MneSystem.Application.Commands;

public record LoginUserCommand(LoginUserDto LoginDto) : IRequest<AuthResponseDto>;