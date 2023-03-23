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

    #endregion
}