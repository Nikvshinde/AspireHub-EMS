namespace AspireHub_EMS.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public string ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
        // ⚡ Add these navigation properties
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<Salary> Salaries { get; set; }
    }
}
