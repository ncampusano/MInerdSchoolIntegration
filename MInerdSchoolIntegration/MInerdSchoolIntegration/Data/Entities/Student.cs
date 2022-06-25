using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinerdSchoolIntegration.Data.Entities
{
    public class Student
    {
        public int Id  { get; set; }

        [MaxLength(13)]
        public string Rne  { get; set; }

        [MaxLength(50)]
        public string Town  { get; set; }

        [MaxLength(1)]
        public string AcademicLevel  { get; set; }

        [MaxLength(5)]
        public int AcademicGrade  { get; set; }

        [MaxLength(3)]
        public string AcademicPeriod { get; set; }

        [MaxLength(3)]
        public string BloodType { get; set; }

        [MaxLength(50)]
        public string Disabilities { get; set; }

        public School School { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
