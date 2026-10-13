using TodoCli.Models;
using TodoCli.Storage;

namespace TodoCli.Services;

public class TodoService
{
    private readonly JsonTodoStorage _storage;
    private readonly List<TodoItem> _tareas;

    public TodoService(JsonTodoStorage storage)
    {
        _storage = storage;
        _tareas = _storage.Cargar();
    }

    public IReadOnlyList<TodoItem> ObtenerTodas() => _tareas;

    public bool Agregar(string? titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return false;

        int nuevoId = _tareas.Count == 0 ? 1 : _tareas.Max(t => t.Id) + 1;
        _tareas.Add(new TodoItem(nuevoId, titulo.Trim(), false));
        _storage.Guardar(_tareas);
        return true;
    }

    public bool Completar(int id)
    {
        int pos = _tareas.FindIndex(t => t.Id == id);
        if (pos == -1)
            return false;

        _tareas[pos] = _tareas[pos] with { Completada = true };
        _storage.Guardar(_tareas);
        return true;
    }

    public bool Eliminar(int id)
    {
        int pos = _tareas.FindIndex(t => t.Id == id);
        if (pos == -1)
            return false;

        _tareas.RemoveAt(pos);
        _storage.Guardar(_tareas);
        return true;
    }
}