using System.Collections.Generic;

namespace ReservationWeb.Models
{
    public class Habitacion
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public bool Disponible { get; set; }
        public string Detalles { get; set; }
        public decimal Precio { get; set; }
        public int PisoId { get; set; }
        public Piso Piso { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<Reservacion> Reservaciones { get; set; }
    }
}
