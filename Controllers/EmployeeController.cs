using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<IActionResult> GetEmployees(int page = 1, int rows = 10)
    {
        var result = await _employeeService.GetEmployees(page, rows);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpPost(Name = "AddEmployee")]
    public async Task<IActionResult> AddEmployee(EmployeeRegistryDto employee)
    {
        var result = await _employeeService.AddEmployee(employee);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpPut("{id:int}/update", Name = "UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(EmployeeUpdateDto employee, int id)
    {
        var result = await _employeeService.UpdateEmployee(employee, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);

    }
    
    #endregion
}