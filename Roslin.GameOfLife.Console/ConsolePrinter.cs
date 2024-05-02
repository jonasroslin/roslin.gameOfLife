using Roslin.GameOfLife.Engine;

namespace Roslin.GameOfLife.Console;

public class ConsolePrinter : IPrinter
{
    public void Print(Cell[,] matrix)
    {
        var height = matrix.GetLength(0);
        var width = matrix.GetLength(1);

        for (var i = 0; i < height; i++)
        {
            System.Console.Write("    ");
            for (var j = 0; j < width; j++)
            {
                System.Console.Write(matrix[i, j]);
            }

            System.Console.WriteLine();
        }
    }
}