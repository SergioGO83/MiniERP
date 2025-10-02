using Microsoft.AspNetCore.Identity;

namespace MiniERP.Models
{
    public class Usuario : IdentityUser
    {
        public string Rol { get; set; } = "Operario";
        public string? NombreCompleto { get; set; }
    }
}