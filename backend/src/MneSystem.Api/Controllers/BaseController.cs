using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger Logger;

    protected BaseController(ILogger logger)
    {
        Logger = logger;
    }

    protected ObjectResult Ok<T>(T data, string message = "Success")
    {
        return Ok(new ApiResponse<T>(true, message, data));
    }

    protected ObjectResult BadRequest(string message = "Bad request")
    {
        return base.BadRequest(new ApiResponse<object>(false, message));
    }

    protected ObjectResult NotFound(string message = "Resource not found")
    {
        return base.NotFound(new ApiResponse<object>(false, message));
    }

    protected ObjectResult ServerError(string message = "An error occurred")
    {
        return StatusCode(500, new ApiResponse<object>(false, message));
    }
}