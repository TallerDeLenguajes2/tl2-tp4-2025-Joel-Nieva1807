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
        if (cadeteria.ListaPedidos == null) return NoContent(); // 204, no hay pedidos cargados

        return Ok(cadeteria.ListaPedidos);//200, pedidos encontrados
    }

    [HttpGet("cadeteria")]
    public IActionResult GetCadeteria()
    {
        if (cadeteria == null) return NotFound("No se encontro la cadeteria"); //404
        return Ok(cadeteria); //200
    }

    [HttpGet("informe")]
    public IActionResult GetInforme()
    {
        if (informe == null) return NotFound("No hay informe generado"); //404
        return Ok(informe); //200

    }

    [HttpPost("crearPedido")]
    public IActionResult crearPedido([FromBody] Pedidos nuevo)
    {

        if (nuevo == null) return BadRequest("El pedido no puede ser nulo"); //404
        cadeteria.cargarPedido(nuevo);
        datosPedidos.Guardar(cadeteria.ListaPedidos);
        return CreatedAtAction(nameof(GetPedidos), new { id = nuevo.NumeroDePedido }, nuevo);
    }

    [HttpPut("asignarPedido")]
    public IActionResult asignarPedido(int idPedido, int idCadete)
    {
        string respuesta = cadeteria.asignarCadeteAlPedido(idCadete, idPedido);

        if (respuesta.Contains("no encontrado", StringComparison.OrdinalIgnoreCase)) return NotFound(respuesta); // 404
        if (respuesta.Contains("invalido", StringComparison.OrdinalIgnoreCase)) return BadRequest(respuesta); // 400

        datosPedidos.Guardar(cadeteria.ListaPedidos);
        return Ok(respuesta); //200
    }

    [HttpPut("cambiarEstado")]
    public IActionResult cambiarEstadoPedido(int pedido, int nuevoEstado)
    {
        string resultado = cadeteria.cambiarEstadoPedido(pedido, nuevoEstado);
        if (resultado.Contains("no encontrado", StringComparison.OrdinalIgnoreCase)) return NotFound(resultado); // 404
        if (resultado.Contains("estado invalido", StringComparison.OrdinalIgnoreCase)) return BadRequest(resultado); // 400

        datosPedidos.Guardar(cadeteria.ListaPedidos);
        return Ok(resultado);
    }

    [HttpPut("asignarNuevoCadete")]
    public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        string resultado = cadeteria.cambiarCadeteDePedido(idPedido, idNuevoCadete);

        if(resultado.Contains("no encontrado", StringComparison.OrdinalIgnoreCase)) return NotFound(resultado); // 404
        if (resultado.Contains("estado invalido", StringComparison.OrdinalIgnoreCase)) return BadRequest(resultado); // 400
        if (resultado.Contains("ya asignado", StringComparison.OrdinalIgnoreCase)) return BadRequest(resultado); // 400

        datosPedidos.Guardar(cadeteria.ListaPedidos);

        return Ok(resultado); //200
    }
}
