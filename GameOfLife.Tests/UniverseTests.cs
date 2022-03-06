using Xunit;

namespace GameOfLife.Tests;

public class UniverseTests
{
    [Fact]
    public void Given_AUniverseWithALiveCell_When_ATickPasses_Then_TheUniverseHasNoLiveCells()
    {
        var universe = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0))
            .Build();

        universe.Tick();
        
        var expectedUniverse = new UniverseBuilder()
            .Build();
        
        Assert.Equal(expectedUniverse, universe);
    }
    
    [Fact]
    public void Given_AUniverseWithAnActiveCellWithTwoAdjacentActiveCell_When_ATickPasses_Then_TheLiveCellStaysLive()
    {
        var universe = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0), new Cell(-1, 0), new Cell(1, 0))
            .Build();
        
        universe.Tick();

        var expectedUniverse = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0), new Cell(0, 1), new Cell(0, -1))
            .Build();
        
        Assert.Equal(expectedUniverse, universe);
    }
    
    [Fact]
    public void Given_AUniverseWithAnActiveCellWithThreeAdjacentActiveCell_When_ATickPasses_Then_TheLiveCellStaysLive()
    {
        var universe = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0), new Cell(-1, -1), new Cell(1, 1), new Cell(-1, 1))
            .Build();
        
        universe.Tick();

        var expectedUniverse = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0), new Cell(-1, 0), new Cell(0, 1))
            .Build();
        
        Assert.Equal(expectedUniverse, universe);
    }
    
    [Fact]
    public void Given_AUniverseWithAnActiveCellWithFourAdjacentActiveCell_When_ATickPasses_Then_TheLiveCellDies()
    {
        var universe = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0), new Cell(-1, -1), new Cell(1, 1), new Cell(-1, 1), new Cell(1, -1))
            .Build();

        universe.Tick();

        var expectedUniverse = new UniverseBuilder()
            .WithLiveCells(new Cell(-1, 0), new Cell(0, 1), new Cell(1, 0), new Cell(0, -1), new Cell(-1, 0))
            .Build();
        
        Assert.Equal(expectedUniverse, universe);
    }
    
    [Fact]
    public void Given_AUniverseWithADeadCellWithThreeAdjacentActiveCell_When_ATickPasses_Then_TheDeadCellBecomesLive()
    {
        var universe = new UniverseBuilder()
            .WithLiveCells(new Cell(-1, -1), new Cell(1, 1), new Cell(-1, 1))
            .Build();

        universe.Tick();

        var expectedUniverse = new UniverseBuilder()
            .WithLiveCells(new Cell(0, 0))
            .Build();
        
        Assert.Equal(expectedUniverse, universe);
    }
}