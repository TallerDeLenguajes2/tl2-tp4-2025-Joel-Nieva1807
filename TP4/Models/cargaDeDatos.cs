
using System.IO;
using System.Text.Json;
using EspacioCadeteria;
using EspacioCadete;
using Microsoft.VisualBasic;
using EspacioPedidos;

namespace EspacioCargaDatos;

public class AccesoADatosCadeteria
{
    Cadeteria cadeteria = null;
    private JsonSerializerOptions _opts = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
    public Cadeteria obtener()
    {
        string rutaCadeteria = File.ReadAllText("cadeteria.json");
        cadeteria = JsonSerializer.Deserialize<Cadeteria>(rutaCadeteria, _opts);

        if (cadeteria == null)
        {
            Console.WriteLine("Error al deserializar el JSON.");
        }

        return cadeteria;

    }

}

public class AccesoADatosCadetes
{
    List<Cadete> cadetes = null;
    private JsonSerializerOptions _opts = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public List<Cadete> obtener()
    {
        string rutaCadetes = File.ReadAllText("cadetes.json");
        cadetes = JsonSerializer.Deserialize<List<Cadete>>(rutaCadetes, _opts);

        if (cadetes == null)
        {
            Console.WriteLine("Error al deserializar el json");
        }

        return cadetes;
    }


}

public class AccesoADatosPedidos
{
    List<Pedidos> pedidos = null;
    private JsonSerializerOptions _opts = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public List<Pedidos> obtener()
    {
        string rutaPedidos = File.ReadAllText("pedidos.json");
        pedidos = JsonSerializer.Deserialize<List<Pedidos>>(rutaPedidos, _opts);

        if (pedidos == null)
        {
            Console.WriteLine("Error al deserializar el json");
        }

        return pedidos;
        
    }

    
    

}