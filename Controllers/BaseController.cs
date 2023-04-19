using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/basics")]
[ApiVersion("1")]
public class BaseController : ControllerBase
{
    #region Fields

    private readonly IConfiguration _configuration;

    #endregion

    #region Constructor
    
    public BaseController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion
    
    [HttpGet("apikey", Name = "GetApiKey")]
    public IActionResult GetApiKey()
    {
        var result = _configuration.GetValue("ApiKey", "No value found");
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}