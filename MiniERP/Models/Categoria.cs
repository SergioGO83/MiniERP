namespace MiniERP.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";

        // Relación 1:N con productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}