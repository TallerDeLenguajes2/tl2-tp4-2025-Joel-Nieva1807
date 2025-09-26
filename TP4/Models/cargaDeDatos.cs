
using System.IO;
using System.Text.Json;
using EspacioCadeteria;
using EspacioCadete;
using Microsoft.VisualBasic;

namespace EspacioCargaDatos;

public interface IAccesoADatos
{
    Cadeteria cargaDatos(string rutaCadeteria, string rutaCadetes); //ruta cadeteria: para Json sera el path del Json, para csv sera el archivo cadeteria.csv
    //rutaCadetes: en csv seria el archivo cadete1.csv, en Json se lo puede ignorar
}

public class AccesoADatos : IAccesoADatos
{
    public Cadeteria cargaDatos(string rutaCadeteria, string rutaCadetes)
    {
        Cadeteria cadeteria = null;
        if (rutaCadeteria == null)
        {
            Console.WriteLine("Error fatal!");
        }
        else
        {
            var lineasCad = File.ReadAllLines(rutaCadeteria);
            var partes = lineasCad[0].Split(',');
            string nombre = partes[0].Trim('"');
            int telefono = int.Parse(partes[1]);

            cadeteria = new Cadeteria(nombre, telefono);

            if (!string.IsNullOrEmpty(rutaCadetes) && File.Exists(rutaCadetes))
            {
                var lineasCadetes = File.ReadAllLines(rutaCadetes);
                foreach (var linea in lineasCadetes)
                {

                    var separador = linea.Split(',');
                    int id = int.Parse(separador[0]);
                    string nombreC = separador[1].Trim('"');
                    string direccion = separador[2].Trim('"');
                    int tel = int.Parse(separador[3]);
                    cadeteria.ListaCadetes.Add(new Cadete(id, nombreC, direccion, tel));
                }
            }
        }
        return cadeteria;
    }

}

public class AccesoADatosJSON : IAccesoADatos
{
    Cadeteria cadeteria = null;
    List<Cadete> cadetes = null;

    private JsonSerializerOptions _opts = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public Cadeteria cargaDatos(string rutaCadeteria, string rutaCadetes)
    {
        string Cadeteriajson = File.ReadAllText(rutaCadeteria);
        cadeteria = JsonSerializer.Deserialize<Cadeteria>(Cadeteriajson, _opts);

        string cadetesJson = File.ReadAllText(rutaCadetes);
        cadetes = JsonSerializer.Deserialize<List<Cadete>>(cadetesJson, _opts);

        if (cadeteria == null)
        {
            Console.WriteLine("Error al deserializar el JSON.");
        }

        cadeteria.ListaCadetes.AddRange(cadetes); //agregar todos los elementos de una colección a otra colección existente de forma rápida.
            
        return cadeteria;
    }
}