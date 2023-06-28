using System.Collections.Generic;

namespace ReservationWeb.Models
{
    public class Piso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Habitacion> Habitaciones { get; set; }
    }
}
