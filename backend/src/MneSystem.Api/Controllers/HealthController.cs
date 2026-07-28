public class HealthController : BaseController
{
    public HealthController(ILogger<HealthController> logger) : base(logger)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { 
            Status = "Healthy", 
            Timestamp = DateTime.UtcNow,
            Application = "M&E System API",
            Version = "1.0.0"
        }, "System is operational");
    }
}