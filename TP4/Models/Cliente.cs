// See https://aka.ms/new-console-template for more information
namespace EspacioCliente;
public class Cliente
{

    public string nombre { get; set; }
    public string direccion { get; set; }
    public int telefono { get; set; }
    public string datosReferenciaDireccion { get; set; }

    public Cliente() { } //constructor vacio para la serealizacion
    public Cliente(string nombre, string direccion, int telefono, string datosReferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string VerDatosDelCliente()
    {
        return $"La direccion del cliente es: {nombre}" +
        $"El telefono del cliente es: {telefono}" +
        $"Referencia de la direccion del cliente es: {datosReferenciaDireccion}";

    }


}