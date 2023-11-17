using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVSite.Models
{
    public class MessageModels
    {

            public int IdMeddelande { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Meddelandet ska vara minst tre bokstav.",      MinimumLength = 3)]
            [Display(Name = "Meddelandet:")]


        public string Innehåll { get; set; }
        public int Mottagare { get; set; }
        public bool Läst { get; set; }


        [Required]
            [StringLength(100, ErrorMessage = "Namnet är minst två bokstav.", MinimumLength = 2  )]
            [Display(Name = "Skickas av:")]
            public string SkickasAv { get; set; }


            [Display(Name = "Mottagre:")]
            public string MottagareNamn { get; set; }

        



    }
}