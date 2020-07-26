using System;
using System.Threading;

namespace gra_snake_projekt
{
    class Program
    {
        static void Main(string[] args)
        {
        game:
            {

                Console.CursorVisible = (false);
                Console.Title = "Snake!";

                Console.SetWindowSize(56, 38);

                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                ConsoleColor bgColor = Console.BackgroundColor;
                ConsoleColor fgColor = Console.ForegroundColor;
                int retardation = 100;
                string course = "right";

                int lengthSnake = 8;

                Random rand = new Random();

                int score = 0;
                int x = 20;
                int y = 20;
                int colourTog = 1;
                bool alive = true;
                bool shotOn = false;
                int shotX = 0;
                int shotY = 0;

                int[] xPoints;
                xPoints = new int[8] { 20, 19, 18, 17, 16, 15, 14, 13 };
                int[] yPoints;
                yPoints = new int[8] { 20, 20, 20, 20, 20, 20, 20, 20 };

                while (alive)
                {
                    if (shotOn == false)
                    {
                        bool collide = false;
                        shotOn = true;
                        shotX = rand.Next(4, Console.WindowWidth - 4);
                        shotY = rand.Next(4, Console.WindowHeight - 4);

                        for (int l = (xPoints.Length - 1); l > 1; l--)
                        {
                            if (xPoints[l] == shotX & yPoints[l] == shotY)
                            {
                                collide = true;
                            }
                        }
                        if (collide == true)
                        {
                            shotOn = false;
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(shotX, shotY);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.BackgroundColor = bgColor;
                            Console.Write("#");
                            shotOn = true;
                        }

                    }
                    Array.Resize<int>(ref xPoints, lengthSnake);
                    Array.Resize<int>(ref yPoints, lengthSnake);

                    System.Threading.Thread.Sleep(retardation);
                    colourTog++;
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.RightArrow:
                                if (course != "left")
                                {
                                    course = "right";
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                if (course != "right")
                                {
                                    course = "left";
                                }
                                break;
                            case ConsoleKey.UpArrow:

                                if (course != "down")
                                {
                                    course = "up";
                                }
                                break;
                            case ConsoleKey.DownArrow:

                                if (course != "up")
                                {
                                    course = "down";
                                }
                                break;
                            default:
                                break;
                        }
                    } 

                    if (course == "right")
                    {
                        x += 1;
                    }
                    else if (course == "left")
                    {
                        x -= 1;
                    }
                    else if (course == "down")
                    {
                        y += 1;
                    }
                    else if (course == "up")
                    {
                        y -= 1;
                    }

                    xPoints[0] = x;
                    yPoints[0] = y;

                    for (int l = (xPoints.Length - 1); l > 0; l--)
                    {
                        xPoints[l] = xPoints[l - 1];
                        yPoints[l] = yPoints[l - 1];
                    }

                    try
                    {
                        Console.SetCursorPosition(xPoints[0], yPoints[0]);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        alive = false;
                    }
                    if (colourTog == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        colourTog = 1;
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.ForegroundColor = fgColor;
                    Console.Write("*");

                    try
                    {
                        Console.SetCursorPosition(xPoints[xPoints.Length - 1], yPoints[yPoints.Length - 1]);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        alive = false;
                    }
                    Console.BackgroundColor = bgColor;
                    Console.Write(" ");

                    if (x == shotX & y == shotY)
                    {
                        shotOn = false;
                        lengthSnake += 1;
                        retardation -= retardation / 16;
                        new Thread(() => Console.Beep(320, 250)).Start();
                    }

                    for (int l = (xPoints.Length - 1); l > 1; l--)
                    {
                        if (xPoints[l] == xPoints[0] & yPoints[l] == yPoints[0])
                        {
                            alive = false;
                        }

                    }
                    score = ((lengthSnake) - 8);
                    Console.SetCursorPosition(2, 2);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Score: {0} ", score);

                }
                new Thread(() => Console.Beep(37, 1)).Start();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
 
                Console.Beep(831, 250);

                Console.Beep(785, 250);

                ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

                for (int i = 0; i < 1; i++)
                {
                    foreach (var color in colors)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = color;
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n");
                        Console.WriteLine("\n                       Game over! :(");
                        Console.WriteLine("\n\n                   Your score was: {0} !", score);
                        System.Threading.Thread.Sleep(100);
                    }
                }
                Thread.Sleep(1000);
                Console.WriteLine("\n\n\n\n\n\n             -- Press Any Key To Continue: --");
                Thread.Sleep(500);
                Console.ReadKey(true);
                Console.ReadKey(true);
                goto game;
            }
        }
    }
}