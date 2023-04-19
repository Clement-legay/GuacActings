using guacactings.Models;

namespace guacactings.Services;

public interface IEmployeeService
{
    // Get all employees
    Task<IEnumerable<Employee>> GetEmployees(int page, int rows);
    // Get an employee by id
    Task<Employee?> GetEmployeeById(int id);
    Task<ICollection<Employee>?> GetEmployeesByName(string name);
    // Create a new employee
    Task<Employee?> AddEmployee(EmployeeRegistryDto employee);
    // Update an employee
    Task<Employee?> UpdateEmployee(EmployeeUpdateDto employee, int id);
    // Delete an employee
    Task<Employee?> DeleteEmployee(int id);
}