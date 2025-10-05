// See https://aka.ms/new-console-template for more information
using EspacioCliente;
using EspacioCadete;

namespace EspacioPedidos;

public enum EstadoPedido
{
    Pendiente,
    Asignado,
    Entregado

}
public class Pedidos
{
    public Cliente cliente { get; set; }
    public int NumeroDePedido { get; set; }
    public string Observaciones { get; set; }


    public EstadoPedido Estado { get; set; }

    public Cadete? CadeteAsignado { get; set; } // con '?' este campo puede ser nullable (estar vacio). En este caso pedido puede existir sin un cadete asignado aun

    public Pedidos() { } //constructor vacio para la serealizacion
    public Pedidos(int numeroDePedido, string observaciones, EstadoPedido estado, Cliente cliente)
    {
        this.NumeroDePedido = numeroDePedido;
        this.Observaciones = observaciones;
        this.cliente = cliente;
        this.Estado = estado;
    }

    public string VerDatosdireccionCliente()
    {
        return cliente.direccion;

    }

}
