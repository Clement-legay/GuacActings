using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "visitor, admin")]
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
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var result = await _serviceService.GetServiceById(id);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpGet("files/{serviceName}/{fileName}", Name = "GetServicePicture")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String; size: 112MB")]
    public async Task<IActionResult> GetDocumentFile(string serviceName, string fileName)
    {
        var serviceByLink = await _serviceService.GetServiceByLink(serviceName, fileName);
        if (serviceByLink is null)
        {
            return BadRequest("Image not found");
        }
        
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), serviceByLink.PictureUrl!);
        var contentDisposition = new ContentDisposition
        {
            Inline = true,
            FileName = serviceName + "." + serviceByLink.PictureUrl!.Split('.').Last()
        };
        Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
        HttpContext.Response.Headers["Title"] = serviceName;
        return PhysicalFile(filePath, $"image/{serviceByLink.PictureUrl!.Split('.').Last()}");
    }
    
    [HttpPost(Name = "AddService")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddService([FromForm] ServiceRegistryDto service)
    {
        var result = await _serviceService.AddService(service);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpPut("{id:int}/update", Name = "UpdateService")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateService(int id, [FromForm] ServiceUpdateDto service)
    {
        var result = await _serviceService.UpdateService(service, id);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }
    
    [HttpDelete("{id:int}/delete", Name = "DeleteService")]
    [Authorize(Roles = "admin")]
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