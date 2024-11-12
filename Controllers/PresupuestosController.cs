using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
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
    public IActionResult ListarDetalles(int id)
    {
        return View(presRep.GetDetalles(id));
    }

    [HttpGet]
    public IActionResult Crear()
    {
        var cliRep = new ClienteRepository();

        return View(new PresupuestoViewModel(cliRep.getClientes()));
    }

    [HttpPost]
    public IActionResult Crear(int IdCliente)
    {
        var presupuesto = new Presupuesto();
        presupuesto.FechaCreacion = DateTime.Now;
        presupuesto.Cliente.AsignarId(IdCliente);
        
        presRep.create(presupuesto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult AsignarProducto(int id)
    {
        var presupuesto = presRep.GetPresupuesto(id);
        if(presupuesto is null)
        {
            return RedirectToAction("Listar");
        } else 
        {
            var prodRep = new ProductosRepository();
            var presupuestoVM = new ProductoAltaViewModel(id, prodRep.listarProductos());
            return View(presupuestoVM);
        }
    
    }

    [HttpPost]
    public IActionResult AsignarProducto(int idPresupuesto, int idProducto, int cantidad)
    {   
        presRep.agregarDetalle(idPresupuesto, idProducto, cantidad);
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