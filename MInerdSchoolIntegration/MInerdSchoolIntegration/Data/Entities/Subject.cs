using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinerdSchoolIntegration.Data.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        [Display(Name = "Materia")]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}