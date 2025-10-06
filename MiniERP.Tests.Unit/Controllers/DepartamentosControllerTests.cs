using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Controllers;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Tests.Unit.Controllers;

public class DepartamentosControllerTests
{
    private readonly AppDbContext _context;
    private readonly DepartamentosController _controller;

    public DepartamentosControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase(databaseName: $"DepartamentosTestDB_{Guid.NewGuid()}")
    .Options;

        _context = new AppDbContext(options);

        _context.Departamentos.AddRange(
            new Departamento { Id = 1, Nombre = "RRHH" },
            new Departamento { Id = 2, Nombre = "Almacén" }
        );
        _context.SaveChanges();

        _controller = new DepartamentosController(_context);
    }

    [Fact(DisplayName = "Index devuelve vista con departamentos")]
    public async Task Index_ReturnsViewWithItems()
    {
        var result = await _controller.Index();

        var view = result.Should().BeOfType<ViewResult>().Subject;
        var model = view.Model.Should().BeAssignableTo<IEnumerable<Departamento>>().Subject;

        model.Should().HaveCount(2);
        model.First().Nombre.Should().Be("RRHH");
    }

    [Fact(DisplayName = "Create válido redirige a Index y persiste")]
    public async Task Create_Valid_RedirectsAndSaves()
    {
        var dep = new Departamento { Nombre = "Innovación" };

        var result = await _controller.Create(dep);

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");

        _context.Departamentos.Should().Contain(x => x.Nombre == "Innovación");
    }

    [Fact(DisplayName = "Create inválido devuelve la vista")]
    public async Task Create_Invalid_ReturnsView()
    {
        var dep = new Departamento { Nombre = "" };
        _controller.ModelState.AddModelError("Nombre", "Requerido");

        var result = await _controller.Create(dep);

        var view = result.Should().BeOfType<ViewResult>().Subject;
        view.Model.Should().BeOfType<Departamento>();
        _context.Departamentos.Should().HaveCount(2); // nada nuevo
    }
}
