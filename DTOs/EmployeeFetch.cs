namespace dotnet_crud_api.DTOs;

public class EmployeeFetch
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
    public string DepartmentName { get; set; }
    public string PositionTitle { get; set; }
}