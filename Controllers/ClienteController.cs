using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.RateLimiting;
using tl2_tp6_2024_nachoNota.Models;

namespace tl2_tp6_2024_nachoNota.Controllers;

public class ClienteController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ClienteRepository cliRep;

    public ClienteController(ILogger<HomeController> logger)
    {
        _logger = logger;
        cliRep = new ClienteRepository();
    }

    public IActionResult Listar()
    {
        return View(cliRep.listarProductos());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new Cliente());
    }

    [HttpPost]
    public IActionResult Crear(Cliente cliente)
    {
        cliRep.Create(cliente);
        return RedirectToAction("Listar");
    } 

    [HttpGet]
    public IActionResult Modificar(int idCli)
    {
        var cliente = cliRep.GetCliente(idCli);
        return View(cliente);
    }

    [HttpPost]
    public IActionResult Modificar(Cliente clienteView)
    {
        var cliente = cliRep.GetCliente(clienteView.IdCliente);

        cliente.Nombre = clienteView.Nombre;
        cliente.Email = clienteView.Email;
        cliente.Telefono = clienteView.Telefono;

        cliRep.Update(cliente);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Eliminar(int idCli)
    {
        var producto = cliRep.GetCliente(idCli);
        return View(producto);
    }

    [HttpPost]
    public IActionResult EliminarConfirm(int idCli)
    {    
        cliRep.delete(idCli);
        return RedirectToAction("Listar");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}