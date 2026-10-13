
using System.Text.Json;
using TodoCli.Models;

namespace TodoCli.Storage;

public class JsonTodoStorage
{
    private static readonly JsonSerializerOptions Opciones = new() { WriteIndented = true };
    private readonly string _ruta;

    public JsonTodoStorage(string ruta)
    {
        _ruta = ruta;
    }

    public List<TodoItem> Cargar()
    {
        if (!File.Exists(_ruta))
            return new List<TodoItem>();

        try
        {
            string json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }
        catch (JsonException)
        {
            return new List<TodoItem>();
        }
    }

    public void Guardar(IEnumerable<TodoItem> tareas)
    {
        string json = JsonSerializer.Serialize(tareas, Opciones);
        File.WriteAllText(_ruta, json);
    }
}