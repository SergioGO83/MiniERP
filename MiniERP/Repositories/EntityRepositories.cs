using MiniERP.Models;
using MiniERP.Data;

namespace MiniERP.Repositories
{
    public interface IEmpleadoRepository : IRepository<Empleado> { }
    public class EmpleadoRepository(AppDbContext context) : Repository<Empleado>(context), IEmpleadoRepository { }

    public interface IProductoRepository : IRepository<Producto> { }
    public class ProductoRepository(AppDbContext context) : Repository<Producto>(context), IProductoRepository { }

    public interface ICategoriaRepository : IRepository<Categoria> { }
    public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository { }

    public interface IDepartamentoRepository : IRepository<Departamento> { }
    public class DepartamentoRepository(AppDbContext context) : Repository<Departamento>(context), IDepartamentoRepository { }

    public interface IUsuarioRepository : IRepository<Usuario> { }
    public class UsuarioRepository(AppDbContext context) : Repository<Usuario>(context), IUsuarioRepository { }
}
