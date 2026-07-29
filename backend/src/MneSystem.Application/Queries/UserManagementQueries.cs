using MneSystem.Application.DTOs;
using MneSystem.Application.Queries;

namespace MneSystem.Application.Queries;

public record GetUserListQuery : IRequest<List<UserListDto>>;
public record GetUserDetailQuery(string UserId) : IRequest<UserDetailDto?>;