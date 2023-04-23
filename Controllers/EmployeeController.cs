using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/employee")]
[ApiVersion("1")]
public class EmployeeController : ControllerBase
{
    #region Fields

    private readonly IEmployeeService _employeeService;

    #endregion
    
    #region Constructor
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    #endregion
    
    #region Methods
    
    /// <summary>
    /// Returns all employees
    /// </summary>
    /// <returns></returns>    
    [HttpGet(Name = "GetEmployees")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetEmployees(int page = 1, int rows = 10)
    {
        var result = await _employeeService.GetEmployees(page, rows);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetEmployeeById")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var result = await _employeeService.GetEmployeeById(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("site/{siteId:int}", Name = "GetEmployeesBySiteId")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetEmployeesBySiteId(int siteId, string search = "", int page = 1, int rows = 10)
    {
        var result = await _employeeService.GetEmployeesBySiteId(siteId, search, page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("service/{serviceId:int}", Name = "GetEmployeesByServiceId")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetEmployeesByServiceId(int serviceId, string search = "", int page = 1, int rows = 10)
    {
        var result = await _employeeService.GetEmployeesByServiceId(serviceId, search, page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpGet("search", Name = "GetEmployeesByName")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetEmployeesByName(int page = 1, int rows = 10, string name = "")
    {
        var result = await _employeeService.GetEmployeesByName(page, rows, name);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost(Name = "AddEmployee")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddEmployee([FromForm] EmployeeRegistryDto employee)
    {
        var result = await _employeeService.AddEmployee(employee);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpPut("{id:int}/update", Name = "UpdateEmployee")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeUpdateDto employee, int id)
    {
        var result = await _employeeService.UpdateEmployee(employee, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);

    }

    [HttpDelete("{id:int}/delete", Name = "DeleteEmployee")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _employeeService.DeleteEmployee(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    #endregion
}