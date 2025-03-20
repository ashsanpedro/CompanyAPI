using Microsoft.AspNetCore.Mvc;
using dotnet_crud_api.DTOs;
using dotnet_crud_api.Models;
using dotnet_crud_api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_crud_api.Controllers;

[Route("Employee/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeFetch>> GetEmployeeById(Guid id)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        if (employee == null) return NotFound(new { message = "Employee not found" });
        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeCreateDTO dto)
    {
        var result = await _employeeService.CreateEmployee(dto);
        if (result == null)
        {
            return BadRequest(new {message = "Employee already exists"});
        }
        
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(Guid id, EmployeeCreateDTO dto)
    {
       var updatedEmployee = await _employeeService.UpdateEmployee(id, dto);

       if (updatedEmployee == null)
       {
           return NotFound(new {message = "Employee not found"});
       }
       return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployee(Guid id)
    {
        var employee = await _employeeService.DeleteEmployee(id);
        if (!employee)
        {
            return NotFound(new {message = "Employee not found"});
        }
        return Ok(new {message = "Deleted Successfully"});
    }
}