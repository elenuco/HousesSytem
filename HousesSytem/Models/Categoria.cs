using System.Collections.Generic;

namespace HousesSytem.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public ICollection<Habitacion> Habitaciones { get; set; }
    }
}
