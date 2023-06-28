using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationWeb.App;
using ReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationWeb.Controllers
{
    public class GestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Reservar()
        {
            var clientes = _context.Clientes.ToList();
            var categorias = _context.Categorias.ToList();
            var pisos = _context.Pisos.ToList();
            var habitaciones = _context.Habitaciones
                .Include(h => h.Categoria)
                .Include(h => h.Piso)
                .ToList();

            ViewBag.Clientes = clientes;
            ViewBag.Categorias = categorias;
            ViewBag.Pisos = pisos;
            ViewBag.Habitaciones = habitaciones;
            if (habitaciones != null)
            {
                // Asignar las habitaciones a ViewBag.Habitaciones
                ViewBag.Habitaciones = habitaciones;
            }
            else
            {
                // Manejar el caso cuando no hay habitaciones disponibles
                ViewBag.Habitaciones = new List<Habitacion>();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Reservar(int clienteId, int habitacionId, DateTime fechaCheckIn, DateTime fechaCheckOut)
        {
            var cliente = _context.Clientes.Find(clienteId);
            var habitacion = _context.Habitaciones.Find(habitacionId);

            if (cliente != null && habitacion != null && habitacion.Clientes.Count == 0)
            {
                var precioTotal = CalcularPrecioTotal(habitacion.Categoria, fechaCheckIn, fechaCheckOut);

                var reservacion = new Reservacion
                {
                    ClienteId = cliente.Id,
                    Cliente = cliente,
                    HabitacionId = habitacion.Id,
                    Habitacion = habitacion,
                    FechaReservacion = DateTime.Now,
                    FechaCheckIn = fechaCheckIn,
                    FechaCheckOut = fechaCheckOut,
                    PrecioTotal = precioTotal
                };

                habitacion.Clientes.Add(cliente);
                _context.Reservaciones.Add(reservacion);
                _context.SaveChanges();

                // Actualizar el dashboard
                var dashboard = _context.Dashboard.FirstOrDefault();
                if (dashboard != null)
                {
                    dashboard.TotalHabitaciones = _context.Habitaciones.Count();
                    dashboard.HabitacionesDisponibles = _context.Habitaciones.Count(h => h.Disponible);
                    dashboard.HabitacionesOcupadas = _context.Habitaciones.Count(h => !h.Disponible);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "Dashboard");
            }

            // Si ocurre algún error, redirecciona a la página de reservar con un mensaje de error
            ViewBag.ErrorMessage = "No se pudo realizar la reservación.";
            var clientes = _context.Clientes.ToList();
            var categorias = _context.Categorias.ToList();
            var pisos = _context.Pisos.ToList();
            var habitaciones = _context.Habitaciones
                .Include(h => h.Categoria)
                .Include(h => h.Piso)
                .ToList();

            ViewBag.Clientes = clientes;
            ViewBag.Categorias = categorias;
            ViewBag.Pisos = pisos;
            ViewBag.Habitaciones = habitaciones;
            return View();
        }

        private decimal CalcularPrecioTotal(Categoria categoria, DateTime fechaCheckIn, DateTime fechaCheckOut)
        {
            // Lógica para calcular el precio total de la reservación basado en la categoría y las fechas

            decimal precio = categoria.Precio; // Obtener el precio por noche de la categoría
            int totalNoches = (int)(fechaCheckOut - fechaCheckIn).TotalDays; // Calcular el número total de noches

            decimal precioTotal = precio * totalNoches; // Calcular el precio total multiplicando el precio por noche por el número de noches

            return precioTotal;
        }
    }


}
