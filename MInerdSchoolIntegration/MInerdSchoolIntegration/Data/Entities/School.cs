using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinerdSchoolIntegration.Data.Entities
{
    public class School
    {
        [Key]
        [MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(8)]
        public string CampusCode { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
