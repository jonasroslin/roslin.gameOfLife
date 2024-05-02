using Roslin.GameOfLife.Console;
using Roslin.GameOfLife.Engine;

Console.CursorVisible = false;
Console.SetWindowSize(500, 500);
//Console.WindowWidth = 500;

var gameOfLife = new GameOfLife(new ConsolePrinter());

var source = new CancellationTokenSource();
var cancellationToken = source.Token;
Console.CancelKeyPress += delegate
{
    source.Cancel();
    //Console.Clear();
    //Console.SetCursorPosition(0, 0);
    Console.WriteLine("Thank you for playing");
};


await gameOfLife.Run(cancellationToken);