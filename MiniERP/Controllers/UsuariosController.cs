using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniERP.Models;
using MiniERP.ViewModels;

namespace MiniERP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, string>();

            foreach (var user in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.FirstOrDefault() ?? "Sin rol";
            }

            ViewBag.UserRoles = userRoles;
            return View(usuarios);
        }


        // GET: Usuarios/Create
        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .ToList();

            var model = new CreateUsuarioViewModel
            {
                RolesDisponibles = roles
            };

            return View(model);
        }
        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NombreCompleto = model.NombreCompleto
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.Rol))
                    {
                        await _userManager.AddToRoleAsync(user, model.Rol);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description); // <-- log en consola
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // 🔹 REPONER ROLES ANTES DE VOLVER A LA VISTA
            model.RolesDisponibles = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .ToList();

            return View(model);
        }

        // GET: Usuarios/Delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // POST: Usuarios/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "El usuario no existe.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Usuario eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "Error al eliminar el usuario.";
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Usuarios/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.Roles = roles;
            ViewBag.UserRole = userRoles.FirstOrDefault();

            return View(user);
        }

        // POST: Usuarios/Edit/id

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Usuario user, string selectedRole)
        {
            if (id != user.Id) return NotFound();

            var usuarioDb = await _userManager.FindByIdAsync(id);
            if (usuarioDb == null) return NotFound();

            usuarioDb.Email = user.Email;
            usuarioDb.UserName = user.Email;
            usuarioDb.NombreCompleto = user.NombreCompleto;

            var result = await _userManager.UpdateAsync(usuarioDb);

            if (result.Succeeded)
            {
                // 🔹 Cambiar roles
                var rolesActuales = await _userManager.GetRolesAsync(usuarioDb);
                await _userManager.RemoveFromRolesAsync(usuarioDb, rolesActuales);

                if (!string.IsNullOrEmpty(selectedRole))
                {
                    await _userManager.AddToRoleAsync(usuarioDb, selectedRole);
                }

                TempData["Success"] = "Usuario actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "No se pudo actualizar el usuario.";
            return View(user);
        }
        // GET: Usuarios/ResetPassword/5
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new ResetPasswordViewModel { UserId = id };
            return View(model);
        }

        // POST: Usuarios/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                TempData["Error"] = "Usuario no encontrado.";
                return RedirectToAction("Index");
            }

            // Generar token y resetear
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["Success"] = "Contraseña reseteada correctamente.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }


    }
}
