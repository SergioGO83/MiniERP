using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        if (User.IsInRole("Admin"))
            return View("AdminDashboard");

        if (User.IsInRole("RRHH"))
            return View("RRHHDashboard");

        if (User.IsInRole("Operario"))
            return View("OperarioDashboard");

        return RedirectToAction("Login", "Account");
    }
}