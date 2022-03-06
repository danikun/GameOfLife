using GameOfLife;

var universe = new Universe();

universe.Reset(new Cell(0, 0), new Cell(-1, 0), new Cell(1, 0), new Cell(0, 1), new Cell(0, -1));

Console.Clear();
Console.WriteLine("Game of life");

var left = Console.CursorLeft;
var top = Console.CursorTop;

Console.WriteLine(universe.Print(-10, 10, -10, 10));

while (true)
{
    Thread.Sleep(1000);
    universe.Tick();
    
    Console.SetCursorPosition(left, top);
    Console.WriteLine(universe.Print(-10, 10, -10, 10));
}