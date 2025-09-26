using Microsoft.AspNetCore.Mvc;
using EspacioPedidos;
using Microsoft.AspNetCore.SignalR;
using EspacioCargaDatos;
using EspacioCadeteria;
using Microsoft.VisualBasic;
using EspacioInforme;
namespace TP4.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    IAccesoADatos datosCarga = null;
    Cadeteria cadeteria = null;
    Informe informe = null;

    public CadeteriaController()
    {
        datosCarga = new AccesoADatosJSON();
        cadeteria = datosCarga.cargaDatos("cadeteria.json", "cadetes.json");
    }


    [HttpGet("getPedidos")]
    public IActionResult GetPedidos()
    {

        return Ok(cadeteria.ListaPedidos);//le pide al acceso a datos los pedidos existentes
    }

    [HttpGet("getCadeteria")]
    public IActionResult GetCadeterias()
    {
        return Ok(cadeteria);
    }

    [HttpGet("getInforme")]
    public IActionResult GetInforme()
    {
        return Ok(informe);

    }

    [HttpPost("postCrearPedido")]
    public IActionResult crearPedido([FromBody] Pedidos nuevo)
    {
        cadeteria.cargarPedido(nuevo);
        return Ok(nuevo);
    }

    [HttpPut("putAsignarPedido")]
    public IActionResult asignarPedido(int idPedido, int idCadete)
    {
        string respuesta = cadeteria.asignarCadeteAlPedido(idCadete, idPedido);
        return Ok(respuesta);
    }

    [HttpPut("putCambiarEstado")]
    public IActionResult cambiarEstadoPedido(int pedido, int nuevoEstado)
    {
        string resultado = cadeteria.cambiarEstadoPedido(pedido, nuevoEstado);
        return Ok(resultado);
    }

    [HttpPut("putAsignarNuevoCadete")]
    public IActionResult CambiarCadetePedido(int idPedido,int idNuevoCadete)
    {
        
        return Ok(cadeteria.cambiarCadeteDePedido(idPedido, idNuevoCadete));
    }
}
