namespace Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
