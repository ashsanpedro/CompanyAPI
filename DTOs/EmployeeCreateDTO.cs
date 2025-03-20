using System;

namespace dotnet_crud_api.DTOs;

public class EmployeeCreateDTO
{
    public string Name { get; set; }
    public int Salary { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid PositionId { get; set; }
}