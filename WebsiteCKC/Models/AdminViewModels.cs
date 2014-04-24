using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class AdminViewModels
    {
    }

    public class CreateCompetitionView
    {
        [Required(ErrorMessage="Geen geldige klasse ingevoerd")]
        [Display(Name="Klasse")]
        public int Class { get; set; }

        [Required(ErrorMessage = "Geen geldige groep ingevoerd")]
        [Display(Name = "Groep")]
        public int Group { get; set; }


    }
}