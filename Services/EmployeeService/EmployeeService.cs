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
    public async Task<IEnumerable<Employee>> GetEmployees(int page = 1, int rows = 10)
    {
        var employees = await _context.Employees.Include(e => e.Address).ToListAsync();
        var employeesPaged = employees.Skip((page - 1) * rows).Take(rows);
        return employeesPaged;
    }
    
    // Get an employee by id
    public async Task<Employee?> GetEmployeeById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        return employee ?? null;
    }

    public async Task<Employee?> AddEmployee(EmployeeRegistryDto employee)
    {
        var username = $"{employee.Firstname![0]}{employee.Lastname}{new Random().Next(1000, 9999)}";
        
        var newEmployee = new Employee {
            Firstname = employee.Firstname,
            Lastname = employee.Lastname,
            Username = username,
            Phone = employee.Phone,
            HomePhone = employee.HomePhone,
            Email = employee.Email,
            AddressId = employee.AddressId,
            BirthDate = employee.BirthDate,
            ServiceId = employee.ServiceId,
            SiteId = employee.SiteId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        var saveEmployee = _context.Employees.Add(newEmployee).Entity;
        await _context.SaveChangesAsync();
        return saveEmployee;
    }

    public async Task<Employee?> UpdateEmployee(EmployeeUpdateDto employee, int id)
    {
        if (employee is null)
        {
            return null;
        }
        
        var updateEmployee = await _context.Employees.FindAsync(id);
        if (updateEmployee is null)
        {
            return null;
        }

        updateEmployee.AddressId = employee.AddressId ?? updateEmployee.AddressId;
        updateEmployee.ServiceId = employee.ServiceId ?? updateEmployee.ServiceId;
        updateEmployee.SiteId = employee.SiteId ?? updateEmployee.SiteId;
        updateEmployee.Firstname = employee.Firstname ?? updateEmployee.Firstname;
        updateEmployee.Lastname = employee.Lastname ?? updateEmployee.Lastname;
        updateEmployee.Phone = employee.Phone ?? updateEmployee.Phone;
        updateEmployee.HomePhone = employee.HomePhone ?? updateEmployee.HomePhone;
        updateEmployee.Email = employee.Email ?? updateEmployee.Email;
        updateEmployee.BirthDate = employee.BirthDate ?? updateEmployee.BirthDate;
        updateEmployee.UpdatedAt = DateTime.Now;

        var updatedEmployee = _context.Employees.Update(updateEmployee).Entity;
        await _context.SaveChangesAsync();
        return updatedEmployee;
    }

    public async Task<Employee?> DeleteEmployee(int id)
    {
        var deleteEmployee = await _context.Employees.FindAsync(id);
        if (deleteEmployee is null)
        {
            return null;
        }

        var deletedEmployee = _context.Employees.Remove(deleteEmployee).Entity;
        await _context.SaveChangesAsync();
        return deletedEmployee;
    }

    #endregion
}