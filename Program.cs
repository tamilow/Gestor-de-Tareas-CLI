    var tareas = new List<todoItem>();
    tareas.Add(new todoItem(1, "Aprender C#", false));
    tareas.Add(new todoItem(2, "Hacer el To-Do Cli", false));

    foreach (var t in tareas)
    {
        string marca = this.Completada ? "x" : " ";
        Console.WriteLine($"[{marca}] {t.Id}. {t.Titulo}");
    }

public record todoItem(int Id, string Titulo, bool Completada);
