using System.Collections.Generic;

namespace HousesSytem.Models
{
    public class Piso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Habitacion> Habitaciones { get; set; }
    }
}
