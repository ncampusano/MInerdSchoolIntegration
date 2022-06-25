namespace MinerdSchoolIntegration.Data.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int ObtainedGrade { get; set; }

        public Student Student { get; set; }

        public Subject Subject { get; set; }
    }
}
