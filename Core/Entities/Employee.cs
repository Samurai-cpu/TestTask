namespace Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ManagerId { get; set; }
        public bool Enable { get; set; }
    }
}