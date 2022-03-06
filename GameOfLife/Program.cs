using GameOfLife;

Console.Clear();
Console.WriteLine("Game of life");

var running = true;
var cells = new HashSet<Cell>();

while (running)
{
    var commandArgs = ReadCommand();

    if (commandArgs[0] == "exit")
    {
        Exit();
    }

    if (commandArgs[0] == "add")
    {
        Add(commandArgs);
    }

    if (commandArgs[0] == "start")
    {
        Start();
    }
}

string[] ReadCommand()
{
    Console.Write(">");
    var strings = Console.ReadLine()!.Split(' ');
    return strings;
}

void Exit()
{
    running = false;
}

void Add(string[] commandArgs1)
{
    if (commandArgs1.Length != 3 || !long.TryParse(commandArgs1[1], out var x) ||
        !long.TryParse(commandArgs1[2], out var y))
    {
        Console.WriteLine("usage: add <x_coordinate> <y_coordinate>");
        return;
    }

    cells.Add(new Cell(x, y));
    Console.WriteLine($"Live cell added at ({x},{y})");
}

void Start()
{
    var universe = new Universe();
    universe.Reset(cells);

    Console.Clear();
    Console.WriteLine("Starting Game...");
    var left = Console.CursorLeft;
    var top = Console.CursorTop;

    var gameRunning = true;

    Task.Run(() =>
    {
        while (gameRunning)
        {
            var consoleKey = Console.ReadKey(true).Key;
            gameRunning = consoleKey != ConsoleKey.Escape;
        }
    });
    
    while (gameRunning)
    {
        Console.SetCursorPosition(left, top);
        Console.WriteLine(universe.Print(-10, 10, -10, 10));
        Console.WriteLine("Press scape to finish execution");
        
        Thread.Sleep(1000);
        universe.Tick();
    }
}