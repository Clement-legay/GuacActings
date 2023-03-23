using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("[controller]")]
public class SiteController : ControllerBase
{
    #region Fields

    private readonly ISiteService _siteService;

    #endregion

    #region Constructor

    public SiteController(ISiteService siteService)
    {
        _siteService = siteService;
    }
    
    #endregion

    #region Methods

    [HttpGet(Name = "GetAllSites")]
    public async Task<IActionResult> GetSites(int page = 1, int rows = 10)
    {
        var result = await _siteService.GetSites(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    #endregion
}