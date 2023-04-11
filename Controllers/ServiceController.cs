using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/service")]
[ApiVersion("1")]
public class ServiceController : ControllerBase
{
    #region Fields

    private readonly IServiceService _serviceService;

    #endregion
    
    #region Constructor
    
    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    
    #endregion
    
    #region Methods
    
    /// <summary>
    /// Returns all services
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetServices")]
    public async Task<IActionResult> GetServices(int page = 1, int rows = 10)
    {
        var result = await _serviceService.GetServices(page, rows);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpGet("{id:int}", Name = "GetServiceById")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var result = await _serviceService.GetServiceById(id);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpPost(Name = "AddService")]
    public async Task<IActionResult> AddService([FromForm] ServiceRegistryDto service)
    {
        var result = await _serviceService.AddService(service);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpPut("{id:int}", Name = "UpdateService")]
    public async Task<IActionResult> UpdateService(int id, [FromForm] ServiceUpdateDto service)
    {
        var result = await _serviceService.UpdateService(service, id);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteService")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var result = await _serviceService.DeleteService(id);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    #endregion
}