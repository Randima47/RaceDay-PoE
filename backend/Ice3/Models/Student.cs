namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }               // Primary Key
        public string StudentNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Mark { get; set; }
    }
}
