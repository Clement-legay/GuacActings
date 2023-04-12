using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;


[ApiController]
[Route("api/v{version:apiVersion}/administrators")]
[ApiVersion("1")]
public class AdministratorController : ControllerBase
{
    #region Fields

    private readonly IAdministratorService _administratorService;

    #endregion

    #region Constructor

    public AdministratorController(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    #endregion

    #region Methods

    [HttpGet(Name = "GetAdministrators")]
    public async Task<IActionResult> GetAdministrators(int page = 1, int rows = 10)
    {
        var administrators = await _administratorService.GetAdministrators(page, rows);
        if (administrators == null)
        {
            return BadRequest();
        }
        
        return Ok(administrators);
    }
    
    [HttpGet("{id:int}", Name = "GetAdministratorById")]
    public async Task<IActionResult> GetAdministratorById(int id)
    {
        var administrator = await _administratorService.GetAdministratorById(id);
        if (administrator == null)
        {
            return BadRequest();
        }
        
        return Ok(administrator);
    }
    
    [HttpPost(Name = "AddAdministrator")]
    public async Task<IActionResult> AddAdministrator([FromForm] AdministratorRegistryDto administrator)
    {
        var newAdministrator = await _administratorService.AddAdministrator(administrator);
        if (newAdministrator == null)
        {
            return BadRequest();
        }
        
        return Ok(newAdministrator);
    }
    
    [HttpPost("login", Name = "LoginAdministrator")]
    public async Task<IActionResult> LoginAdministrator([FromForm] AdministratorLoginDto administrator)
    {
        var loggedAdministrator = await _administratorService.LoginAdministrator(administrator);
        if (loggedAdministrator == null)
        {
            return BadRequest();
        }
        
        return Ok(loggedAdministrator);
    }

    [HttpPost("token", Name = "PersistConnection")]
    public async Task<IActionResult> PersistConnection([FromForm] AdministratorPersistDto administrator)
    {
        var persistedAdministrator = await _administratorService.PersistConnection(administrator);
        if (persistedAdministrator == null)
        {
            return BadRequest();
        }

        return Ok(persistedAdministrator);
    }

    [HttpPut("{id:int}/update/password", Name = "UpdatePasswordAdministrator")]
    public async Task<IActionResult> UpdatePasswordAdministrator(int id, [FromForm] AdministratorUpdatePasswordDto administrator)
    {
        var updatedAdministrator = await _administratorService.UpdatePasswordAdministrator(id, administrator);
        if (updatedAdministrator == null)
        {
            return BadRequest();
        }
        
        return Ok(updatedAdministrator);
    }
    
    [HttpPut("{id:int}/update/email", Name = "UpdateAdministrator")]
    public async Task<IActionResult> UpdateAdministrator(int id, [FromForm] AdministratorUpdateEmailDto administrator)
    {
        var updatedAdministrator = await _administratorService.UpdateEmailAdministrator(id, administrator);
        if (updatedAdministrator == null)
        {
            return BadRequest();
        }
        
        return Ok(updatedAdministrator);
    }
    
    [HttpDelete("{id:int}/delete", Name = "DeleteAdministrator")]
    public async Task<IActionResult> DeleteAdministrator(int id)
    {
        var deletedAdministrator = await _administratorService.DeleteAdministrator(id);
        if (deletedAdministrator == null)
        {
            return BadRequest();
        }
        
        return Ok(deletedAdministrator);
    }

    #endregion
}