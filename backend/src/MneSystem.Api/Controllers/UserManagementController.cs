using MediatR;
using MneSystem.Application.DTOs;
using MneSystem.Application.Queries;

namespace MneSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserManagementController> _logger;

    public UserManagementController(IMediator mediator, ILogger<UserManagementController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        try
        {
            var command = new CreateUserCommand(userDto);
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = result.Message,
                Data = result.User
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while creating user"
            });
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
    {
        try
        {
            var command = new UpdateUserCommand(userDto);
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = result.Message,
                Data = result.User
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while updating user"
            });
        }
    }

    [HttpPost("activate/{userId}")]
    public async Task<IActionResult> ActivateUser(string userId)
    {
        try
        {
            var command = new ActivateUserCommand(userId);
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = result.Message,
                Data = result.User
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error activating user");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while activating user"
            });
        }
    }

    [HttpPost("deactivate/{userId}")]
    public async Task<IActionResult> DeactivateUser(string userId)
    {
        try
        {
            var command = new DeactivateUserCommand(userId);
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = result.Message,
                Data = result.User
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deactivating user");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while deactivating user"
            });
        }
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto roleDto)
    {
        try
        {
            var command = new AssignRoleCommand(roleDto);
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Message,
                    Errors = result.Errors
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = result.Message,
                Data = result.User
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning role");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while assigning role"
            });
        }
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetUserList()
    {
        try
        {
            var query = new GetUserListQuery();
            var result = await _mediator.Send(query);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Users retrieved successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user list");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while retrieving users"
            });
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserDetails(string userId)
    {
        try
        {
            var query = new GetUserDetailQuery(userId);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user details");
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while retrieving user details"
            });
        }
    }
}