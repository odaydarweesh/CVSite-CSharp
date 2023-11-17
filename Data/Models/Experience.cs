using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Experience must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Title:")]
        public string Titel { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }

    }
}
