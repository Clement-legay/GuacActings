using guacactings.Models;

namespace guacactings.Services;

public interface IEmployeeService
{
    // Get all employees
    Task<IEnumerable<Employee>> GetEmployees(int page, int rows);
    // Create a new employee
    Task<Employee?> AddEmployee(EmployeeRegistryDto employee);
    // Update an employee
    Task<Employee?> UpdateEmployee(EmployeeUpdateDto employee, int id);
}