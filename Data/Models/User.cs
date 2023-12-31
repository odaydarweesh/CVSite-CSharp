﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Data.Models
{
    public class User 
    {
        [Key]
        public int UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public bool PrivateUser { get; set; } 
        public virtual ICollection<ProjectsUsers> Projects { get; set; }



    }
}
