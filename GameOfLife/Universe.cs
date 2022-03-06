using System.Text;

namespace GameOfLife;

public class Universe
{
    private readonly HashSet<Cell> _cells;

    public Universe()
    {
        _cells = new HashSet<Cell>();
    }

    public void Reset(IEnumerable<Cell> cells)
    {
        _cells.Clear();

        foreach (var cell in cells)
        {
            _cells.Add(cell);
        }
    }
    
    public void Tick()
    {
        Reset(NextLiveCells.ToHashSet());
    }

    private int CountNeighbours(Cell cell)
    {
        return _cells.Count(c => c.IsNeighbour(cell));
    }

    private IEnumerable<Cell> DeadCells => _cells
        .SelectMany(cell => cell.Neighbours())
        .Where(cell => !_cells.Contains(cell));

    private IEnumerable<Cell> SurvivingCells => _cells
        .Where(cell => CountNeighbours(cell) > 1)
        .Where(cell => CountNeighbours(cell) < 4);

    private IEnumerable<Cell> CellsBeingBorn => DeadCells
        .Where(cell => CountNeighbours(cell) == 3);

    private IEnumerable<Cell> NextLiveCells => SurvivingCells.Union(CellsBeingBorn);

    #region Print
    public string Print(long minY, long maxY, long minX, long maxX)
    {
        var builder = new StringBuilder();

        for (var y = maxY; y >= minY; y--)
        {
            builder.AppendLine(PrintLine(y, minX, maxX));
        }

        return builder.ToString();
    }
    
    private string PrintLine(long lineNumber, long lineStart, long lineEnd)
    {
        var builder = new StringBuilder();
        
        for (var x = lineStart; x <= lineEnd; x++)
        {
            var isLive = _cells.Contains(new Cell(x, lineNumber));
            builder.Append(isLive ? 'X' : ' ');
        }

        return builder.ToString();
    }
    #endregion
    #region Equality

    private bool Equals(Universe other)
    {
        return _cells.SetEquals(other._cells);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Universe)obj);
    }

    public override int GetHashCode()
    {
        return _cells.GetHashCode();
    }

    public static bool operator ==(Universe? left, Universe? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Universe? left, Universe? right)
    {
        return !Equals(left, right);
    }

    #endregion
}