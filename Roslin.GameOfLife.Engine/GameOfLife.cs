namespace Roslin.GameOfLife.Engine;

//Any live cell with fewer than two live neighbors dies, as if by underpopulation.
//Any live cell with two or three live neighbors lives on to the next generation.
//Any live cell with more than three live neighbors dies, as if by overpopulation.
//Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.

public class GameOfLife
{
    private readonly IPrinter _printer;
    private const int Width = 100;
    private const int Height = 25;

    private Cell[,] _matrix = new Cell[Height, Width];

    public GameOfLife(IPrinter printer)
    {
        _printer = printer;
        var height = _matrix.GetLength(0);
        var width = _matrix.GetLength(1);

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var spawnChance = new List<bool> { true, false, false, false, false };
                var isAlive = spawnChance.MinBy(_ => Guid.NewGuid());

                var cell = new Cell(i, j)
                {
                    IsAlive = isAlive
                };
                _matrix[i, j] = cell;
            }
        }

        //_matrix[3, 2].WakeUp();
        //_matrix[3, 3].WakeUp();
        //_matrix[3, 4].WakeUp();
        //_matrix[3, 5].WakeUp();
        //_matrix[3, 6].WakeUp();
        //_matrix[3, 7].WakeUp();
        //_matrix[3, 8].WakeUp();
        //_matrix[3, 9].WakeUp();

        //_matrix[3, 11].WakeUp();
        //_matrix[3, 12].WakeUp();
        //_matrix[3, 13].WakeUp();
        //_matrix[3, 15].WakeUp();

        //_matrix[3, 19].WakeUp();
        //_matrix[3, 20].WakeUp();
        //_matrix[3, 21].WakeUp();

        //_matrix[3, 28].WakeUp();
        //_matrix[3, 29].WakeUp();
        //_matrix[3, 30].WakeUp();
        //_matrix[3, 31].WakeUp();
        //_matrix[3, 32].WakeUp();

        //_matrix[3, 34].WakeUp();
        //_matrix[3, 35].WakeUp();
        //_matrix[3, 36].WakeUp();
        //_matrix[3, 37].WakeUp();
        //_matrix[3, 38].WakeUp();


    }

    public async Task Run(CancellationToken token)
    {
        await Task.Factory.StartNew(() =>
        {
            while (!token.IsCancellationRequested)
            {
                Console.SetCursorPosition(0, 0);
                _matrix = GetNextGen();
                _printer.Print(_matrix);
                //Thread.Sleep(TimeSpan.FromMilliseconds(16.67));
            }
            Console.Clear();
            Console.WriteLine("DONE");
        }, token);
        
    }
    
    private Cell[,] GetNextGen()
    {
        var height = _matrix.GetLength(0);
        var width = _matrix.GetLength(1);
        var nextGen = new Cell[height, width];
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var cell = _matrix[i, j];

                var neighbors = GetNeighbors(j, i, width, height);

                var count = neighbors.Count(x => x.IsAlive);

                var newCell = new Cell(i, j) { IsAlive = cell.IsAlive };

                if (cell.IsAlive)
                {
                    if (count is < 2 or > 3)
                        newCell.Kill();
                    else
                        newCell.Keep();
                }
                else if (count == 3)
                {
                    newCell.WakeUp();
                }

                nextGen[i, j] = newCell;

            }


        }

        return nextGen;
    }

    private IEnumerable<Cell> GetNeighbors(int j, int i, int width, int height)
    {
        var neighbors = new List<Cell>();

        //Över
        if (j != 0 && i != 0)
        {
            var cell1 = _matrix[i - 1, j - 1];
            neighbors.Add(cell1);
        }

        if (i != 0)
        {
            var cell2 = _matrix[i - 1, j];
            neighbors.Add(cell2);
        }

        if (j != width - 1 && i != 0)
        {
            var cell3 = _matrix[i - 1, j + 1];
            neighbors.Add(cell3);
        }

        //Samma
        if (j != 0)
        {
            var cell4 = _matrix[i, j - 1];
            neighbors.Add(cell4);
        }

        if (j != width - 1)
        {
            var cell5 = _matrix[i, j + 1];
            neighbors.Add(cell5);
        }


        //Under
        if (i != height - 1 && j != 0)
        {
            var cell6 = _matrix[i + 1, j - 1];
            neighbors.Add(cell6);
        }

        if (i != height - 1)
        {
            var cell7 = _matrix[i + 1, j];
            neighbors.Add(cell7);
        }

        if (j != width - 1 && i != height - 1)
        {
            var cell8 = _matrix[i + 1, j + 1];
            neighbors.Add(cell8);
        }

        return neighbors;
    }
}