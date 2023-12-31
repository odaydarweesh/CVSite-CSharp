﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public string Content { get; set; }
        public virtual ICollection<UserMessage> UserMessages { get; set; }
    }
}
