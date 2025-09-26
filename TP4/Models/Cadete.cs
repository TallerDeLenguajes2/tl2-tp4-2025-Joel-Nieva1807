// See https://aka.ms/new-console-template for more information
namespace EspacioCadete;


public class Cadete
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public int Telefono { get; set; }


    public Cadete() { } //constructor vacio para la serealizacion        
    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;

    }



}
