using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/site")]
[ApiVersion("1")]
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
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetSites(int page = 1, int rows = 10)
    {
        var result = await _siteService.GetSites(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("{id:int}", Name = "GetSiteById")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetSiteById(int id)
    {
        var result = await _siteService.GetSiteById(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpPost(Name = "AddSite")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddSite([FromForm] SiteRegistryDto site)
    {
        var result = await _siteService.AddSite(site);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpPut("{id:int}/update", Name = "UpdateSite")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateSite([FromForm] SiteUpdateDto site, int id)
    {
        var result = await _siteService.UpdateSite(site, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}/delete", Name = "DeleteSite")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteSite(int id)
    {
        var result = await _siteService.DeleteSite(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    #endregion
}