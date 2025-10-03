// See https://aka.ms/new-console-template for more information
using EspacioCadete;
using EspacioPedidos;
using EspacioInforme;
namespace EspacioCadeteria;

public class Cadeteria
{
    public string Nombre { get; set; }
    public int Telefono { get; set; }
    public List<Cadete> ListaCadetes { get; set; }

    public List<Pedidos> ListaPedidos { get; set; }

    public Cadeteria() { } //constructor vacio para serealizacion
    public Cadeteria(string nombre, int telefono)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
        this.ListaPedidos = new List<Pedidos>();
        this.ListaCadetes = new List<Cadete>();
    }

    public void cargarPedido(Pedidos nuevoPedido)
    {
        if (nuevoPedido == null)
        {
            Console.WriteLine("No se puede asignar un pedido vacio!");


            ListaPedidos.Add(nuevoPedido);

        }


    }

    public string asignarCadeteAlPedido(int idCadete, int numPedido)
    {

        Pedidos encontrarPedido = ListaPedidos.FirstOrDefault(p => p.NumeroDePedido == numPedido);

        if (encontrarPedido == null) return "no encontrado";



        Cadete cadeteAsignado = ListaCadetes.FirstOrDefault(c => c.Id == idCadete); //(expresion lambda), c es cada cadete de la lista
                                                                                    //se evalua la condicion c.id == idCadete
                                                                                    //es decir, busca al cadete cuyo Id coincida con el que ingreso el usuario
                                                                                    //(FirstOrDefault es un metodo link que devuelve el primer elemento de la lista que cumpla la condicion o null si no encuentra nada)

        if (cadeteAsignado == null) return "invalido";

        encontrarPedido.Estado = EstadoPedido.Asignado;
        encontrarPedido.CadeteAsignado = cadeteAsignado;
        return $"Pedido {numPedido} asignado a {cadeteAsignado.Nombre}";
    }

    public double JornalACobrar(int idCadete)
    {
        Cadete cadeteEncontrado = ListaCadetes.FirstOrDefault(c => c.Id == idCadete);


        int contador = 0;

        if (cadeteEncontrado != null)
        {
            foreach (var pedido in ListaPedidos)
            {
                if (pedido.CadeteAsignado == cadeteEncontrado && pedido.Estado == EstadoPedido.Entregado)
                {

                    contador++;
                }
            }


        }
        else
        {
            Console.WriteLine("No se encontro cadete con ese ID");
        }

        return contador * 500;
    }

    public Informe mostrarInforme()
    {
        Console.WriteLine("Informe final de la jornada:");

        double totalPedidosEntregados = 0;
        double totalGanado = 0;
        double promedioEnvios;

        if (ListaCadetes.Count > 0)
        {
            promedioEnvios = totalPedidosEntregados / ListaCadetes.Count;
        }
        else
        {
            promedioEnvios = 0;
        }

        Informe informe = new Informe();

        informe.pedidosEntregadosTotal = totalPedidosEntregados;
        informe.totalGanado = totalGanado;
        informe.promedioEnvio = promedioEnvios;

        return informe;

    }

    public string cambiarEstadoPedido(int id, int opcionEstado)
    {
        Pedidos encontrarPedido = ListaPedidos.FirstOrDefault(p => p.NumeroDePedido == id);
        if (encontrarPedido == null) return "no encontrado";
        switch (opcionEstado)
        {
            case 1:
                encontrarPedido.Estado = EstadoPedido.Asignado;

                break;

            case 2:
                encontrarPedido.Estado = EstadoPedido.Entregado;

                break;

            case 3:
                encontrarPedido.Estado = EstadoPedido.Pendiente;

                break;

            default:
                return "error!";
        }

        return $"El pedido {encontrarPedido.NumeroDePedido} se realizo un cambio a su estado a: {encontrarPedido.Estado}";
    }

    public string cambiarCadeteDePedido(int pedidonum, int idDelCadete)
    {
        string resultado = "";
        foreach (var pedido in ListaPedidos)
        {
            if (pedido.NumeroDePedido == pedidonum)
            {
                if (pedido.CadeteAsignado.Id != idDelCadete)
                {
                    resultado = asignarCadeteAlPedido(idDelCadete, pedidonum);

                }
                else
                {
                    return "ya asignado";
                }

            }

        }
        
        return resultado;

    }
}