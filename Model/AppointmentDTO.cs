namespace Models
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ProfileId { get; set; }

    }


}