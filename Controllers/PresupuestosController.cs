using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.RateLimiting;
using tl2_tp6_2024_nachoNota.Models;

namespace tl2_tp6_2024_nachoNota.Controllers;

public class PresupuestosController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private PresupuestosRepository presRep;

    public PresupuestosController(ILogger<HomeController> logger)
    {
        _logger = logger;
        presRep = new PresupuestosRepository();
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return View(presRep.GetPresupuestos());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new Presupuesto());
    }

    [HttpPost]
    public IActionResult Crear(Presupuesto presupuesto)
    {
        presupuesto.FechaCreacion = DateTime.Now;
        presRep.create(presupuesto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult AsignarProducto(int id)
    {
        var presupuesto = presRep.GetPresupuesto(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult AsignarProducto(int id, int idProducto, int cantidad)
    {   
        presRep.agregarDetalle(id, idProducto, cantidad);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Modificar(int id)
    {
        var presupuesto = presRep.GetPresupuesto(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult Modificar(Presupuesto presupuestoVista)
    {
        var presupuesto = presRep.GetPresupuesto(presupuestoVista.Id);

        presupuesto.Cliente.AsignarId(presupuestoVista.Cliente.IdCliente);
        presupuesto.FechaCreacion = presupuestoVista.FechaCreacion;
        presRep.modificar(presupuesto);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        var presupuesto = presRep.GetPresupuesto(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult EliminarConfirm(int id)
    {
        presRep.delete(id);
        return RedirectToAction("Listar");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}