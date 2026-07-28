using MediatR;

namespace MneSystem.Application.Commands;

public record RegisterUserCommand(RegisterUserDto UserDto) : IRequest<AuthResponseDto>;