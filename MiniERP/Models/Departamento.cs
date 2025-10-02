namespace MiniERP.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        // Relación 1:N
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}