using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.ViewModel
{
    public class PersonViewModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nachname")]
        public string LastName { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Straßennamen")]
        public string Street { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Stadt")]
        public string City { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Tierart")]
        public string Type { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Tiername")]
        public string Name { get; set; }
    }
}
