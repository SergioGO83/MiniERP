namespace MiniERP.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public int Stock { get; set; }
        public decimal Precio { get; set; }

        // Relación con Categoría
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}