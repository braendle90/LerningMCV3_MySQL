using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.ViewModel
{
    public class EditRoleViewModel
    {

        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        
        public string Id { get; set; }
        [Required(ErrorMessage = "Rollenname ist ein Pflichtfeld")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; } 


    }
}
