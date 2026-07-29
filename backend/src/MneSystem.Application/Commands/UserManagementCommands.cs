using MediatR;
using MneSystem.Application.DTOs;

namespace MneSystem.Application.Commands;

public record CreateUserCommand(CreateUserDto UserDto) : IRequest<UserManagementResponseDto>;
public record UpdateUserCommand(UpdateUserDto UserDto) : IRequest<UserManagementResponseDto>;
public record ActivateUserCommand(string UserId) : IRequest<UserManagementResponseDto>;
public record DeactivateUserCommand(string UserId) : IRequest<UserManagementResponseDto>;
public record AssignRoleCommand(AssignRoleDto RoleDto) : IRequest<UserManagementResponseDto>;