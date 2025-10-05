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


    [HttpGet("pedidos")]
    public IActionResult GetPedidos()
    {
        if (cadeteria.ListaPedidos == null) return NoContent(); // 204, no hay pedidos cargados, se devuelve cuando la petición fue exitosa pero no hay nada que devolver (lista vacia)

        return Ok(cadeteria.ListaPedidos);//200, pedidos encontrados
    }

    [HttpGet("cadeteria")]
    public IActionResult GetCadeteria()
    {
        if (cadeteria == null) return NotFound("No se encontro la cadeteria"); //404, se usa cuando el recurso solicitado (en este caso la cadetería) no existe en el servidor
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

        if (nuevo == null) return BadRequest("El pedido no puede ser nulo"); //404, si el body es inválido o nulo — evita procesar datos inválidos
        cadeteria.cargarPedido(nuevo);
        
        return Created($"/cadeteria/pedidos/{nuevo.NumeroDePedido}", nuevo);
    }

    [HttpPut("asignarPedido")]
    public IActionResult asignarPedido(int idPedido, int idCadete)
    {
        string respuesta = cadeteria.asignarCadeteAlPedido(idCadete, idPedido);

        if (respuesta.Contains("no encontrado", StringComparison.OrdinalIgnoreCase)) return NotFound(respuesta); // 404
        //StringComparison.OrdinalIgnoreCase evita problemas por mayúsculas/minúsculas cuando evaluás el mensaje.
        if (respuesta.Contains("invalido", StringComparison.OrdinalIgnoreCase)) return BadRequest(respuesta); // 400

        return Ok(respuesta); //200

        /* NotFound(404) si no existe pedido o cadete.

        BadRequest(400) si los parámetros están mal(por ejemplo id negativo).

        Ok(200) si se asignó correctamente. */
    }

    [HttpPut("cambiarEstado")]
    public IActionResult cambiarEstadoPedido(int pedido, int nuevoEstado)
    {
        string resultado = cadeteria.cambiarEstadoPedido(pedido, nuevoEstado);
        if (resultado.Contains("no encontrado", StringComparison.OrdinalIgnoreCase)) return NotFound(resultado); // 404
        if (resultado.Contains("estado invalido", StringComparison.OrdinalIgnoreCase)) return BadRequest(resultado); // 400


        return Ok(resultado);
    }

    [HttpPut("asignarNuevoCadete")]
    public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        string resultado = cadeteria.cambiarCadeteDePedido(idPedido, idNuevoCadete);

        if (resultado.Contains("no encontrado", StringComparison.OrdinalIgnoreCase)) return NotFound(resultado); // 404
        if (resultado.Contains("estado invalido", StringComparison.OrdinalIgnoreCase)) return BadRequest(resultado); // 400
        if (resultado.Contains("ya asignado", StringComparison.OrdinalIgnoreCase)) return BadRequest(resultado); // 400


        return Ok(resultado); //200
    }
}
