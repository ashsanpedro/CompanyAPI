using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_crud_api.Models;

public class Employee
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid PositionId { get; set; }
}