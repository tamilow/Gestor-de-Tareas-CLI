using TodoCli.Services;
using TodoCli.Storage;

var storage = new JsonTodoStorage("tareas.json");
var servicio = new TodoService(storage);

void MostrarTareas()
{
    var tareas = servicio.ObtenerTodas();

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
    Console.WriteLine("=== Gestor de tareas ===");
    Console.WriteLine("1. Listar tareas");
    Console.WriteLine("2. Agregar tarea");
    Console.WriteLine("3. Completar tarea");
    Console.WriteLine("4. Eliminar tarea");
    Console.WriteLine("5. Salir");
    Console.Write("Elige una opción: ");
    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            MostrarTareas();
            break;

        case "2":
        {
            Console.Write("Título de la tarea: ");
            string? titulo = Console.ReadLine();
            Console.WriteLine(servicio.Agregar(titulo)
                ? "Tarea agregada."
                : "El título no puede estar vacío.");
            break;
        }

        case "3":
        {
            MostrarTareas();
            Console.Write("Id de la tarea a completar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Debes escribir un número.");
                break;
            }

            Console.WriteLine(servicio.Completar(id)
                ? "Tarea completada."
                : "No existe una tarea con ese Id.");
            break;
        }

        case "4":
        {
            MostrarTareas();
            Console.Write("Id de la tarea a eliminar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Debes escribir un número.");
                break;
            }

            Console.WriteLine(servicio.Eliminar(id)
                ? "Tarea eliminada."
                : "No existe una tarea con ese Id.");
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