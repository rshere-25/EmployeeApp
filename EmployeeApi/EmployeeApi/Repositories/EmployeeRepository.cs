using EmployeeApi.Context;
using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace EmployeeApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _filePath;

        public EmployeeRepository(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "EmployeeData.json");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            if (_configuration.GetValue<bool>("IsDbConnected"))
            {
                return await _context.Employees.ToListAsync();
            }
            else
            {
                if (!File.Exists(_filePath))
                {
                    Log.Error("Employee data file not found.");
                    return Enumerable.Empty<Employee>();
                }

                string jsonStr = await File.ReadAllTextAsync(_filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<Employee>>(jsonStr, options) ?? new List<Employee>();
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            if (_configuration.GetValue<bool>("IsDbConnected"))
            {
                return await _context.Employees.FindAsync(id);
            }
            else
            {
                var employees = await GetAllEmployeesAsync();
                return employees.FirstOrDefault(e => e.EmpID == id);
            }
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (_configuration.GetValue<bool>("IsDbConnected"))
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                var employees = (await GetAllEmployeesAsync()).ToList();
                employees.Add(employee);
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(employees));
            }
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            if (_configuration.GetValue<bool>("IsDbConnected"))
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var employees = (await GetAllEmployeesAsync()).ToList();
                var index = employees.FindIndex(e => e.EmpID == employee.EmpID);
                if (index == -1) return false;
                employees[index] = employee;
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(employees));
                return true;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            if (_configuration.GetValue<bool>("IsDbConnected"))
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) return false;
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var employees = (await GetAllEmployeesAsync()).ToList();
                var newEmployees = employees.Where(e => e.EmpID != id).ToList();
                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(newEmployees));
                return employees.Count != newEmployees.Count;
            }
        }
    }
}
