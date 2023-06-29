using System.Collections.Generic;

namespace HousesSytem.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int PisoId { get; set; }
        public Piso Piso { get; set; }

        public ICollection<Reservacion> Reservaciones { get; set; }
    }
}
