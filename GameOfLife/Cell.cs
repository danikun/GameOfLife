namespace GameOfLife;

public readonly struct Cell
{
    public Cell(long x, long y)
    {
        X = x;
        Y = y;
    }

    private long X { get; }
    private long Y { get; }

    public bool IsNeighbour(Cell cell)
    {
        var xDistance = Math.Abs(X - cell.X);
        var yDistance = Math.Abs(Y - cell.Y);

        if (xDistance == 0 && yDistance == 0)
        {
            return false;
        }

        if (xDistance < 2 && yDistance < 2)
        {
            return true;
        }

        return false;
    }

    public IEnumerable<Cell> Neighbours()
    {
        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;
                yield return new Cell(X + i, Y + j);
            }
        }
    }
}