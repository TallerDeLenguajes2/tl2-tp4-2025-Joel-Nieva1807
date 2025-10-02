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
    AccesoADatosCadeteria datosCadeteria = null;
    AccesoADatosCadetes datosCadetes = null;
    AccesoADatosPedidos datosPedidos = null;
    Cadeteria cadeteria = null;
    Informe informe = null;

    public CadeteriaController()
    {
        datosCadeteria = new AccesoADatosCadeteria();
        datosCadetes = new AccesoADatosCadetes();
        datosPedidos = new AccesoADatosPedidos();

        cadeteria = datosCadeteria.obtener();
        cadeteria.ListaCadetes = datosCadetes.obtener();
        cadeteria.ListaPedidos = datosPedidos.obtener();
    }


    [HttpGet("pedidos")]
    public IActionResult GetPedidos()
    {

        return Ok(cadeteria.ListaPedidos);//le pide al acceso a datos los pedidos existentes
    }

    [HttpGet("cadeteria")]
    public IActionResult GetCadeteria()
    {
        return Ok(cadeteria);
    }

    [HttpGet("Informe")]
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
        return Ok(respuesta); //not found
    }

    [HttpPut("putCambiarEstado")]
    public IActionResult cambiarEstadoPedido(int pedido, int nuevoEstado)
    {
        string resultado = cadeteria.cambiarEstadoPedido(pedido, nuevoEstado);
        return Ok(resultado);//si no existe el pedido notfound("no existe el pedido")
    }

    [HttpPut("putAsignarNuevoCadete")]
    public IActionResult CambiarCadetePedido(int idPedido,int idNuevoCadete)
    {
        
        return Ok(cadeteria.cambiarCadeteDePedido(idPedido, idNuevoCadete)); //corregir
    }
}
