using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RelacionesEFCoreApp.Data;
using RelacionesEFCoreApp.Models;

public class InscripcionesController : Controller
{
    private readonly ApplicationDbContext _context;

    public InscripcionesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Inscripciones/Create
    public IActionResult Create()
    {
        ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");
        ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Nombre");
        return View();
    }

    // POST: Inscripciones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EstudianteId,CursoId,FechaInscripcion,Estado")] Inscripcion inscripcion)
    {
        // Revisar si ya está inscrito
        var existe = await _context.Inscripciones
            .AnyAsync(i => i.EstudianteId == inscripcion.EstudianteId && i.CursoId == inscripcion.CursoId);

        if (existe)
        {
            ModelState.AddModelError("", "El estudiante ya está inscrito en este curso.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(inscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Estudiantes", new { id = inscripcion.EstudianteId });
        }
        ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", inscripcion.CursoId);
        ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Nombre", inscripcion.EstudianteId);
        return View(inscripcion);
    }
}