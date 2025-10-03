using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniERP.ViewModels
{
    public class EditUsuarioViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Rol Actual")]
        public string RolActual { get; set; }

        public List<string> RolesDisponibles { get; set; }
    }
}
