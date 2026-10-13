using System;
using System.Security.Cryptography.X509Certificates;

var tareas = new List<todoItem>();
    tareas.Add(new todoItem(1, "Aprender C#", false));

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
    Console.WriteLine("4. Salir");
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

