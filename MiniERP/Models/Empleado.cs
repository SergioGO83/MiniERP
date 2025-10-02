namespace MiniERP.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public int Edad { get; set; }

        // Relación con Departamento
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }
    }
}