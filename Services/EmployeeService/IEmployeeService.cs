using guacactings.Models;

namespace guacactings.Services;

public interface IEmployeeService
{
    // Get all employees
    Task<IEnumerable<Employee>> GetEmployees(int page, int rows);
    Task<Employee> AddEmployee(Employee employee);
}