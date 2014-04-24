using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteCKC.Models
{
    public class MatchViewModels
    {
        [Required(ErrorMessage="Geen thuisteam geselecteerd")]
        [Display(Name="Thuis-team")]
        public int HomeTeamID { get; set; }

        [Required(ErrorMessage = "Geen goals toegevoegd")]
        [Display(Name = "Goals")]
        public int HomeTeamScored { get; set; }

        [Required(ErrorMessage = "Geen uit-team geselecteerd")]
        [Display(Name = "Uit-team")]
        public int AwayTeamID { get; set; }

        [Required(ErrorMessage = "Geen goals toegevoegd")]
        [Display(Name = "Goals")]
        public int AwayTeamScored { get; set; }

        [Required(ErrorMessage = "Geen datum toegevoegd")]
        [Display(Name="Wedstrijd-datum")]
        public string MatchDate { get; set; }
    }
}