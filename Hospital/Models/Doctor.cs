namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Img { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
