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