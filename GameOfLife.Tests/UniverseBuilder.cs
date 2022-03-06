using System.Collections.Generic;

namespace GameOfLife.Tests;

public class UniverseBuilder
{
    private readonly HashSet<Cell> _cells;

    public UniverseBuilder()
    {
        _cells = new HashSet<Cell>();
    }

    public UniverseBuilder WithLiveCells(params Cell[] cells)
    {
        foreach (var cell in cells)
        {
            _cells.Add(cell);
        }

        return this;
    }

    public Universe Build()
    {
        var universe = new Universe();

        universe.Reset(_cells);

        return universe;
    }
}