using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

public class ProjectsUsers
{
    [Key, ForeignKey("User"), Column(Order = 0)]
    public int UserID { get; set; }
    [Key, ForeignKey("Project"), Column(Order = 1)]
    public int ProjectID { get; set; }
    public virtual User User { get; set; }
    public virtual Project Project { get; set; }
}

