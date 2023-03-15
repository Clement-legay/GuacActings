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
    public async Task<IEnumerable<Employee>> GetEmployees(int page = 0, int rows = 10)
    {
        return await _employeeService.GetEmployees(page, rows);
    }

    [HttpPost(Name = "AddEmployee")]
    public async Task<Employee> AddEmployee(Employee employee)
    {
        return await _employeeService.AddEmployee(employee);
    }

    #endregion
}