namespace Roslin.GameOfLife.Engine;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsAlive { get; set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Kill()
    {
        IsAlive = false;
    }

    public void WakeUp()
    {
        IsAlive = true;
    }

    public void Keep()
    {
    }

    public override string ToString()
    {
        System.Console.ForegroundColor = IsAlive 
            ? ConsoleColor.DarkCyan 
            : ConsoleColor.Red;
        
        return IsAlive ? "█" : " ";
    }
}