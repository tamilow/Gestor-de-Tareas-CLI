using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

const string archivo = "tareas.json";
var tareas = CargarTareas();

List<todoItem> CargarTareas()
{
    if (!File.Exists(archivo))
        return new List<todoItem>();
    
    try
    {
        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<todoItem>>(json) ?? new List<todoItem>();
    }
    catch (JsonException)
    {
        Console.WriteLine("El archivo de tareas estaba dañado. Se empieza con una lista vacía.");
        return new List<todoItem>();
    }
}

void GuardarTareas()
{
    var opciones = new JsonSerializerOptions {WriteIndented = true};
    string json = JsonSerializer.Serialize(tareas, opciones);
    File.WriteAllText(archivo, json);
}

void MostrarTareas()
{
    if (tareas.Count == 0)
    {
        Console.WriteLine("No hay tareas todavía.");
        return;
    }


    foreach (var t in tareas)
    {
        string marca = t.Completada ? "x" : " ";
        Console.WriteLine($"[{marca}] {t.Id}. {t.Titulo}");
    }
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("==== Gestor de Tareas ===");
    Console.WriteLine("1. Listar tareas");
    Console.WriteLine("2. Agregar tarea");
    Console.WriteLine("3. Completar tarea");
    Console.WriteLine("4. Eliminar tarea");
    Console.WriteLine("5. Salir");
    Console.WriteLine("Elige una opción: ");
    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            MostrarTareas();
            break;

        case "2":
            Console.WriteLine("Título de la tarea: ");
            string? titulo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titulo))
            {
                Console.WriteLine("El título no puede estar vacío.");
                break;
            }
            int nuevoId = tareas.Count == 0 ? 1 : tareas.Max(x => x.Id) + 1;
            tareas.Add(new todoItem(nuevoId, titulo.Trim(), false));
            GuardarTareas();
            Console.WriteLine("Tarea agregada.");
            break;

        case "3":
            {
                MostrarTareas();
                Console.Write("Id de la tarea a completar: ");

                if (!int.TryParse(Console.ReadLine(), out int idCompletar))
                {
                    Console.WriteLine("Debes escribir un número. ");
                    break;
                }

                int pos = tareas.FindIndex(x => x.Id == idCompletar);
                if (pos == -1)
                {
                    Console.WriteLine("No existe una tarea con ese Id.");
                    break;
                }

                tareas[pos] = tareas[pos] with { Completada = true};
                GuardarTareas();
                Console.WriteLine("Tarea completada.");
                break;
            }
        case "4":
            {
                MostrarTareas();
                Console.Write("Id de la tarea a eliminar: ");

                if (!int.TryParse(Console.ReadLine(), out int idEliminar))
                {
                    Console.WriteLine("Debes escribir un número. ");
                    break;
                }

                int pos = tareas.FindIndex(x => x.Id == idEliminar);
                if (pos == -1)
                {
                    Console.WriteLine("No existe una tarea con ese Id.");
                    break;
                }

                tareas.RemoveAt(pos);
                GuardarTareas();
                Console.WriteLine("Tarea eliminada. ");
                break;
            }

        case "5":
            Console.WriteLine("¡Hasta luego!");
            return;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }

}

public record todoItem(int Id, string Titulo, bool Completada);

