using MneSystem.Application.DTOs;
using MneSystem.Domain.Enums;

namespace MneSystem.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserManagementResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<UserManagementResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = request.UserDto;

        var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
        if (existingUser != null)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User with this email already exists",
                Errors = new List<string> { "Email already registered" }
            };
        }

        var user = new ApplicationUser
        {
            Email = userDto.Email,
            UserName = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Phone = userDto.Phone,
            Designation = userDto.Designation,
            Organization = userDto.Organization,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "Failed to create user",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var roles = userDto.Roles ?? new List<string> { AppRoles.FieldOfficer };
        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        var assignedRoles = await _userManager.GetRolesAsync(user);

        _logger.LogInformation("User created successfully: {Email}", user.Email);

        return new UserManagementResponseDto
        {
            Success = true,
            Message = "User created successfully",
            User = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Phone = user.Phone,
                Designation = user.Designation,
                Organization = user.Organization,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = assignedRoles.ToList(),
                EmailConfirmed = user.EmailConfirmed
            }
        };
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserManagementResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UserManagementResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = request.UserDto;

        var user = await _userManager.FindByIdAsync(userDto.Id);
        if (user == null)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User not found",
                Errors = new List<string> { "User ID not found" }
            };
        }

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Phone = userDto.Phone;
        user.Designation = userDto.Designation;
        user.Organization = userDto.Organization;
        user.UpdatedAt = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed to update user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "Failed to update user",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        _logger.LogInformation("User updated successfully: {Email}", user.Email);

        return new UserManagementResponseDto
        {
            Success = true,
            Message = "User updated successfully",
            User = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Phone = user.Phone,
                Designation = user.Designation,
                Organization = user.Organization,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles.ToList(),
                EmailConfirmed = user.EmailConfirmed
            }
        };
    }
}

public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, UserManagementResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ActivateUserCommandHandler> _logger;

    public ActivateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<ActivateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UserManagementResponseDto> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User not found",
                Errors = new List<string> { "User ID not found" }
            };
        }

        if (user.IsActive)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User is already active"
            };
        }

        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "Failed to activate user",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        _logger.LogInformation("User activated: {Email}", user.Email);

        return new UserManagementResponseDto
        {
            Success = true,
            Message = "User activated successfully",
            User = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Phone = user.Phone,
                Designation = user.Designation,
                Organization = user.Organization,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles.ToList(),
                EmailConfirmed = user.EmailConfirmed
            }
        };
    }
}

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, UserManagementResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DeactivateUserCommandHandler> _logger;

    public DeactivateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<DeactivateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UserManagementResponseDto> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User not found",
                Errors = new List<string> { "User ID not found" }
            };
        }

        if (!user.IsActive)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User is already inactive"
            };
        }

        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "Failed to deactivate user",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        _logger.LogInformation("User deactivated: {Email}", user.Email);

        return new UserManagementResponseDto
        {
            Success = true,
            Message = "User deactivated successfully",
            User = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Phone = user.Phone,
                Designation = user.Designation,
                Organization = user.Organization,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles.ToList(),
                EmailConfirmed = user.EmailConfirmed
            }
        };
    }
}

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, UserManagementResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AssignRoleCommandHandler> _logger;

    public AssignRoleCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AssignRoleCommandHandler> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<UserManagementResponseDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var roleDto = request.RoleDto;

        var user = await _userManager.FindByIdAsync(roleDto.UserId);
        if (user == null)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User not found",
                Errors = new List<string> { "User ID not found" }
            };
        }

        if (!await _roleManager.RoleExistsAsync(roleDto.Role))
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "Role does not exist",
                Errors = new List<string> { $"Role '{roleDto.Role}' not found" }
            };
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Contains(roleDto.Role))
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "User already has this role"
            };
        }

        var result = await _userManager.AddToRoleAsync(user, roleDto.Role);
        if (!result.Succeeded)
        {
            return new UserManagementResponseDto
            {
                Success = false,
                Message = "Failed to assign role",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var updatedRoles = await _userManager.GetRolesAsync(user);

        _logger.LogInformation("Role assigned: {Email} -> {Role}", user.Email, roleDto.Role);

        return new UserManagementResponseDto
        {
            Success = true,
            Message = $"Role '{roleDto.Role}' assigned successfully",
            User = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Phone = user.Phone,
                Designation = user.Designation,
                Organization = user.Organization,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = updatedRoles.ToList(),
                EmailConfirmed = user.EmailConfirmed
            }
        };
    }
}