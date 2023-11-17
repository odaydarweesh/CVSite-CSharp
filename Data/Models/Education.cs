using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    public class Education
    {
        [Key]
        public int EduID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Please add an education with at least {2} characters.", MinimumLength = 3)]
        [Display(Name = "Titel:")]
        public string Title { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }

    }
}
