namespace MiniERP.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public class CreateUsuarioViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un rol")]
        public string Rol { get; set; }

        // ⚡ No debe validarse como input del formulario
        [ValidateNever]   // <-- 👈 importante
        public IEnumerable<SelectListItem> RolesDisponibles { get; set; }
    }
}
