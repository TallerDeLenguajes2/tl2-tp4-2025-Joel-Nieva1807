
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
        if (!File.Exists("cadeteria.json")) return null;
        string JSONCadeteria = File.ReadAllText("cadeteria.json");
        cadeteria = JsonSerializer.Deserialize<Cadeteria>(JSONCadeteria, _opts);

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
        if (!File.Exists("cadetes.json")) return null;
        string JSONCadetes = File.ReadAllText("cadetes.json");
        cadetes = JsonSerializer.Deserialize<List<Cadete>>(JSONCadetes, _opts);

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
        if (!File.Exists("pedidos.json")) return null;
        string JSONPedidos = File.ReadAllText("pedidos.json");
        pedidos = JsonSerializer.Deserialize<List<Pedidos>>(JSONPedidos, _opts);

        if (pedidos == null)
        {
            Console.WriteLine("Error al deserializar el json");
        }

        return pedidos;

    }

    public void Guardar(List<Pedidos> Pedidos)
    {
        string JsonGuardar = JsonSerializer.Serialize(Pedidos, _opts);
        File.WriteAllText("pedidos.json", JsonGuardar);
        
    }

    
    

}