using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationWeb.Models
{
    public class Reservacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaReservacion { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public decimal PrecioTotal { get; set; }
        public int HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; }
        [NotMapped]
        public int CategoriaId => Habitacion.CategoriaId;

        [NotMapped]
        public int PisoId => Habitacion.PisoId;
    }
}
