using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class EmployeeService : IEmployeeService
{
    #region Fields

    private readonly DataContext _context;

    #endregion
    
    #region Constructor
    
    public EmployeeService(DataContext context)
    {
        _context = context;
    }
    
    #endregion
    
    #region Methods
    
    // Get all employees
    public async Task<IEnumerable<Employee>> GetEmployees(int page, int rows)
    {
        var employees = await _context.Employees.ToListAsync();
        var employeesPaged = employees.Skip((page - 1) * rows).Take(rows);
        return employeesPaged;
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        if (employee is null)
        {
            return null;
        }
        
        var newEmployee = new Employee {
            Firstname = employee.Firstname,
            Lastname = employee.Lastname,
            Phone = employee.Phone,
            Email = employee.Email,
            AddressId = employee.AddressId
        };

        var saveEmployee = _context.Employees.Add(newEmployee).Entity;
        await _context.SaveChangesAsync();
        return saveEmployee;
    }

    #endregion
}