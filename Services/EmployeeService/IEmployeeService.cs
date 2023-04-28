using guacactings.Models;

namespace guacactings.Services;

public interface IEmployeeService
{
    // Get all employees
    Task<IEnumerable<Employee>> GetEmployees(int page, int rows);
    // Get an employee by id
    Task<Employee?> GetEmployeeById(int id);
    Task<IEnumerable<Employee>?> GetEmployeesBySiteId(int siteId, string search ="", int? serviceId=null, int page = 1, int rows = 10);
    Task<IEnumerable<Employee>?> GetEmployeesByServiceId(int serviceId, string search ="", int? siteId=null, int page = 1, int rows = 10);
    Task<IEnumerable<Employee>?> GetEmployeesByName(int? serviceId=null, int? siteId=null, int page = 1, int rows = 10, string name = "");
    // Create a new employee
    Task<Employee?> AddEmployee(EmployeeRegistryDto employee);
    // Update an employee
    Task<Employee?> UpdateEmployee(EmployeeUpdateDto employee, int id);
    // Delete an employee
    Task<Employee?> DeleteEmployee(int id);
}