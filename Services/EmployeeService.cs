using dotnet_crud_api.Data;
using dotnet_crud_api.Models;
using dotnet_crud_api.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_crud_api.Services;

public class EmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeFetch>> GetAllEmployees() =>
        await _context.Employees
            .Join(_context.Departments, 
                e => e.DepartmentId, 
                d => d.Id, 
                (e, d) => new { e, d })
            .Join(_context.Positions, 
                ed => ed.e.PositionId, 
                p => p.Id, 
                (ed, p) => new EmployeeFetch
                {
                    Id = ed.e.Id,
                    Name = ed.e.Name,
                    Salary = ed.e.Salary,
                    DepartmentName = ed.d.Name,  
                    PositionTitle = p.Title      
                })
            .ToListAsync();

    public async Task<EmployeeFetch> GetEmployeeById(Guid id)
    {
        var employee = await _context.Employees
            .Where(e => e.Id == id)
            .Join(_context.Departments, 
                e => e.DepartmentId, 
                d => d.Id, 
                (e, d) => new { e, d })
            .Join(_context.Positions, 
                ed => ed.e.PositionId, 
                p => p.Id, 
                (ed, p) => new EmployeeFetch
                {
                    Id = ed.e.Id,              
                    Name = ed.e.Name,
                    Salary = ed.e.Salary,
                    DepartmentName = ed.d.Name,  
                    PositionTitle = p.Title      
                })
            .FirstOrDefaultAsync();

        return employee;
    }

    public async Task<Employee> CreateEmployee(EmployeeCreateDTO dto)
    {
       bool exists = await _context.Employees
           .AnyAsync(e => e.Name == dto.Name && e.DepartmentId == dto.DepartmentId);
       if (exists)
       {
           return null;
       }

       var employee = new Employee
       {
           Id = Guid.NewGuid(),
           Name = dto.Name,
           Salary = dto.Salary,
           DepartmentId = dto.DepartmentId,
           PositionId = dto.PositionId
       };
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        
        return employee;
    }

    public async Task<Employee> UpdateEmployee(Guid id, EmployeeCreateDTO dto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return null;
        
        employee.Name = dto.Name;
        employee.Salary = dto.Salary;
        
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        
        return employee;
    }

    public async Task<bool> DeleteEmployee(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if ( employee == null) return false;
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
    
}