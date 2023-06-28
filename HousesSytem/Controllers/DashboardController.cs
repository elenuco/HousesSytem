using Microsoft.AspNetCore.Mvc;
using ReservationWeb.App;
using ReservationWeb.Models;
using System;
using System.Linq;

namespace ReservationWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalHabitaciones = _context.Habitaciones.Count();
            var habitacionesDisponibles = _context.Habitaciones.Count(h => h.Disponible);
            var habitacionesOcupadas = _context.Habitaciones.Count(h => !h.Disponible);

            var viewModel = new DashboardViewModel
            {
                TotalHabitaciones = totalHabitaciones,
                HabitacionesDisponibles = habitacionesDisponibles,
                HabitacionesOcupadas = habitacionesOcupadas
            };

            return View(viewModel);
        }
    }



}
