using MneSystem.Application.DTOs;
using MneSystem.Application.Queries;

namespace MneSystem.Application.Handlers;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserListDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserListQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<UserListDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users
            .OrderByDescending(u => u.CreatedAt)
            .ToList();

        var userListDtos = new List<UserListDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            
            userListDtos.Add(new UserListDto
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
                Roles = roles.ToList(),
                EmailConfirmed = user.EmailConfirmed
            });
        }

        return userListDtos;
    }
}

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailDto?>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserDetailQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserDetailDto?> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDetailDto
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
            EmailConfirmed = user.EmailConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEnabled = user.LockoutEnabled,
            AccessFailedCount = user.AccessFailedCount
        };
    }
}