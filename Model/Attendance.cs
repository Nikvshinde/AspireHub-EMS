namespace AspireHub_EMS.Model
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public string Status { get; set; } // Present / Absent / Half Day
    }
}
