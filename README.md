# To-Do CLI

Gestor de tareas por consola hecho en C#. Permite agregar, listar, completar y eliminar tareas, y las guarda en un archivo JSON para no perderlas al cerrar el programa.

## Características
- Menú interactivo en la terminal
- Validación de entradas (no se rompe si escribes letras donde va un número)
- Persistencia de datos en JSON
- Código separado en capas: modelo, servicio y almacenamiento

## Tecnologías
C# · .NET · System.Text.Json

## Cómo ejecutarlo
```bash
git clone https://github.com/tamiexe/todo-cli.git
cd todo-cli
dotnet run
```

## Estructura
```
Models/     # TodoItem: los datos de una tarea
Services/   # TodoService: reglas de negocio
Storage/    # JsonTodoStorage: leer y guardar el JSON
Program.cs  # menú de consola
```

## Qué aprendí
- Records, listas genéricas y LINQ básico
- Validación con int.TryParse
- Separar la lógica de la interfaz de consola
- Serialización y deserialización de JSON