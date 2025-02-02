using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendario.Data;
using Calendario.Models;
using System.Diagnostics.Tracing;

namespace Calendario.Controllers
{
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Evento creado correctamente";
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Evento evento)
        {
            // if (id != evento.EventoID)
            // {
            //     return NotFound();
            // }

            if (ModelState.IsValid)
            {

                var eventoEncontrado = await _context.Eventos.FindAsync(evento.EventoID);

                if (eventoEncontrado == null)
                {
                    return NotFound();
                }
                eventoEncontrado.Titulo = evento.Titulo;
                eventoEncontrado.Fecha = evento.Fecha;
                eventoEncontrado.Descripcion = evento.Descripcion;
                eventoEncontrado.Ubicacion = evento.Ubicacion;

                _context.Update(eventoEncontrado);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Evento actualizado correctamente";
                return RedirectToAction("Index");
                // try
                // {
                //     _context.Update(evento);
                //     await _context.SaveChangesAsync();
                // }
                // catch (DbUpdateConcurrencyException)
                // {
                //     if (!EventoExists(evento.EventoID))
                //     {
                //         return NotFound();
                //     }
                //     else
                //     {
                //         throw;
                //     }
                // }
                // return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Evento eliminado correctamente";
            return RedirectToAction("Index");

            // return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoID == id);
        }
    }
}
