using EmployeeApi.Models;
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet("GetAllEmployees")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        try
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            if (!employees.Any()) return NotFound("No employee data available.");
            return Ok(employees);
        }
        catch (Exception ex)
        {
            Log.Error("Error fetching employees: {Message}", ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("GetEmployeeById")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        return employee is null ? NotFound("Employee not found") : Ok(employee);
    }

    [HttpPost("AddEmployee")]
    public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
    {
        var addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = addedEmployee.EmpID }, addedEmployee);
    }

    [HttpPut("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.EmpID) return BadRequest("Mismatched Employee ID");

        bool updated = await _employeeRepository.UpdateEmployeeAsync(employee);
        return updated ? NoContent() : NotFound("Employee not found");
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        bool deleted = await _employeeRepository.DeleteEmployeeAsync(id);
        return deleted ? NoContent() : NotFound("Employee not found");
    }
}
