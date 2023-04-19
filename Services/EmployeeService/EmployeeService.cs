using System.Security.Claims;
using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class EmployeeService : IEmployeeService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion
    
    #region Constructor
    
    public EmployeeService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    #endregion
    
    #region Methods
    
    // Get all employees
    public async Task<IEnumerable<Employee>> GetEmployees(int page = 1, int rows = 10)
    {
        var employees = await _context.Employees.ToListAsync();
        var employeesPaged = employees.Skip((page - 1) * rows).Take(rows);
        return employeesPaged;
    }
    
    // Get an employee by id
    public async Task<Employee?> GetEmployeeById(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Service)
            .Include(e => e.Site)
            .FirstOrDefaultAsync(e => e.Id == id);
        return employee ?? null;
    }
    
    // Get employees by name
    public async Task<ICollection<Employee>?> GetEmployeesByName(string name)
    {
        var employees = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Service)
            .Include(e => e.Site)
            .Where(e => e.Firstname!.Contains(name) || e.Lastname!.Contains(name))
            .ToListAsync();
        return employees;
    }

    public async Task<Employee?> AddEmployee(EmployeeRegistryDto employee)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        // create username
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
            CreatedBy = adminId
        };

        var saveEmployee = _context.Employees.Add(newEmployee).Entity;
        await _context.SaveChangesAsync();
        return saveEmployee;
    }

    public async Task<Employee?> UpdateEmployee(EmployeeUpdateDto employee, int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
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
        updateEmployee.UpdatedBy = adminId;

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