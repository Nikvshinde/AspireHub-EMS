namespace AspireHub_EMS.Model
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public string SalaryMonth { get; set; } // e.g., "Feb-2026"
    }
}
