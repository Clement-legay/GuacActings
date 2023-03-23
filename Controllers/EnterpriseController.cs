using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;


[ApiController]
[Route("[controller]")]
public class EnterpriseController : ControllerBase
{
    #region Fields

    private readonly IEnterpriseService _enterpriseService;

    #endregion

    #region Constructor

    public EnterpriseController(IEnterpriseService enterpriseService)
    {
        _enterpriseService = enterpriseService;
    }
    
    #endregion

    #region Methods

    [HttpGet(Name = "GetAllEnterprises")]
    public async Task<IActionResult> GetEnterprises(int page = 1, int rows = 10)
    {
        var result = await _enterpriseService.GetEnterprises(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetEnterpriseById")]
    public async Task<IActionResult> GetEnterpriseById(int id)
    {
        var result = await _enterpriseService.GetEnterpriseById(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost(Name = "AddEnterprise")]
    public async Task<IActionResult> AddEnterprise([FromForm] EnterpriseRegistryDto enterprise)
    {
        var result = await _enterpriseService.AddEnterprise(enterprise);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut("{id:int}/update", Name = "UpdateEnterprise")]
    public async Task<IActionResult> UpdateEnterprise([FromForm] EnterpriseUpdateDto enterprise, int id)
    {
        var result = await _enterpriseService.UpdateEnterprise(enterprise, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}/delete", Name = "DeleteEnterprise")]
    public async Task<IActionResult> DeleteEnterprise(int id)
    {
        var result = await _enterpriseService.DeleteEnterprise(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    #endregion
}