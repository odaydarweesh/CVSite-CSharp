using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Data.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Skill must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Title:")]
        public string Title { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}