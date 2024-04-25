using Core.Entities;

namespace Application
{
    public class GetEmployeeDTO : Employee
    {
        public List<Employee>? Subordinates { get; set; }
    }
}