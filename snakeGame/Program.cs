using System;
using System.Text;
using System.Threading;


namespace snakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            drawFrame();
            Console.CursorVisible = false;

            int[] x = new int[100];
            x[0] = 45;

            int[] y = new int[100];
            y[0] = 10;

            int yDirection = 0;
            int xDirection = 1;

            int xPos = 0;
            int yPos = 0;

            int eatenApples = 0;
            double speed = 150;

            string gameOver = null;

            Random random = new Random();
            int xApple = random.Next(32, 89);
            int yApple = random.Next(7, 24);
            Console.SetCursorPosition(xApple, yApple);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("●");
            Console.ForegroundColor = ConsoleColor.White;

            while (gameOver != "GAME OVER")
            {
                Console.SetCursorPosition(81, 4);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Score: {0}", eatenApples);
                Console.ForegroundColor = ConsoleColor.White;

                reenterScreeen(ref x, ref y, eatenApples);

                Console.SetCursorPosition(xPos, yPos); //delete the back piece
                Console.Write(" ");

                for (int i = 0; i <= eatenApples; i++)
                {
                    Console.SetCursorPosition(x[i], y[i]); //print piece

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("♦");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                xPos = x[0];
                yPos = y[0];

                //collision of snake parts
                for (int i = 0; i < eatenApples; i++)
                {
                    if (x[eatenApples] == x[i] && y[eatenApples] == y[i])
                    {
                        gameOver = "GAME OVER";

                        for (int a = 0; a < 3; a++)  //animation effect at the end                     
                        {
                            for (int j = 0; j <= eatenApples; j++)
                            {
                                Console.SetCursorPosition(x[j], y[j]); 

                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("♦");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            Thread.Sleep(500);

                            for (int j = 0; j <= eatenApples; j++)
                            {
                                Console.SetCursorPosition(x[j], y[j]);

                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("♦");
                                Console.ForegroundColor = ConsoleColor.White;

                            }

                            Thread.Sleep(500);
                        }

                        break;
                    }
                }

                if (Console.KeyAvailable) // find direction snake is going
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (xDirection != 0 && (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow))
                    {
                        xDirection = 0;

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                yDirection = yDirection - 1;
                                break;

                            case ConsoleKey.DownArrow:
                                yDirection = yDirection + 1;
                                break;
                        }
                    }

                    if (yDirection != 0 && (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.LeftArrow))
                    {
                        yDirection = 0;

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                xDirection = xDirection - 1;
                                break;

                            case ConsoleKey.RightArrow:
                                xDirection = xDirection + 1;
                                break;
                        }
                    }
                }
                // move snake
                if (yDirection > 0)
                {
                    //y++;   
                    y[eatenApples + 1] = y[eatenApples] + 1;
                    x[eatenApples + 1] = x[eatenApples];
                    for (int i = 0; i <= eatenApples + 1; i++)
                    {
                        y[i] = y[i + 1];
                        x[i] = x[i + 1];
                    }
                }
                else if (yDirection < 0)
                {
                    //y--;
                    y[eatenApples + 1] = y[eatenApples] - 1;
                    x[eatenApples + 1] = x[eatenApples];
                    for (int i = 0; i <= eatenApples + 1; i++)
                    {
                        y[i] = y[i + 1];
                        x[i] = x[i + 1];
                    }
                }
                else if (xDirection > 0)
                {
                    //x++;
                    x[eatenApples + 1] = x[eatenApples] + 1;
                    y[eatenApples + 1] = y[eatenApples];
                    for (int i = 0; i <= eatenApples + 1; i++)
                    {
                        y[i] = y[i + 1];
                        x[i] = x[i + 1];
                    }
                }
                else if (xDirection < 0)
                {
                    //x--;
                    x[eatenApples + 1] = x[eatenApples] - 1;
                    y[eatenApples + 1] = y[eatenApples];
                    for (int i = 0; i <= eatenApples + 1; i++)
                    {
                        y[i] = y[i + 1];
                        x[i] = x[i + 1];
                    }
                }
                else
                {
                    //x = x + 1;  
                    x[eatenApples + 1] = x[eatenApples] + 1;
                    y[eatenApples + 1] = y[eatenApples];
                    for (int i = 0; i <= eatenApples + 1; i++)
                    {
                        y[i] = y[i + 1];
                        x[i] = x[i + 1];
                    }                    
                }
                //snake and apple collition 
                if (xApple == x[eatenApples] && yApple == y[eatenApples])
                {
                    eatenApples++;

                    if (xDirection > 0)
                    {
                        x[eatenApples] = xApple + 1;
                        y[eatenApples] = yApple;
                    }
                    else if (xDirection < 0)
                    {
                        x[eatenApples] = xApple - 1;
                        y[eatenApples] = yApple;
                    }
                    else if (yDirection > 0)
                    {
                        x[eatenApples] = xApple;
                        y[eatenApples] = yApple + 1;
                    }
                    else
                    {
                        x[eatenApples] = xApple;
                        y[eatenApples] = yApple - 1;
                    }

                    speed = speed * 0.925;

                    //checking similarity of apple and snake coordinates
                    xApple = random.Next(31, 90);
                    yApple = random.Next(6, 25);

                    for (int i = 0; i <= eatenApples; i++)
                    {
                        while (x[i] == xApple && y[i] == yApple)
                        {
                            xApple = random.Next(31, 90);
                            yApple = random.Next(6, 25);
                        }
                    }

                    Console.SetCursorPosition(xApple, yApple);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (xDirection != 0) // change speed because of different sizes of the cursor
                {
                    Thread.Sleep(Convert.ToInt32(speed));
                }
                else
                {
                    Thread.Sleep(Convert.ToInt32(speed * 1.5)); 
                }
            }

            Console.Clear();
            drawFrame();
            seeYourLastScore(eatenApples);

            Console.ReadLine();
        }
        public static void drawFrame()
        {
            for (int y = 5; y < 26; y++)
            {
                for (int x = 29; x < 91; x++)
                {
                    if (x == 29 || x == 90)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("|");
                    }
                    else if (y == 5 || y == 25)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("-");
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(" ");
                    }
                }
            }
        }
        public static void reenterScreeen(ref int[] x, ref int[] y, int eatenApples)
        {           
            if (x[eatenApples] == 90)
            {
                x[eatenApples] = 30;
            }
            if (x[eatenApples] == 29)
            {
                x[eatenApples] = 89;
            }
            if (y[eatenApples] == 5)
            {
                y[eatenApples] = 24;
            }
            if (y[eatenApples] == 25)
            {
                y[eatenApples] = 6;
            }
        }
        public static void seeYourLastScore(int score)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(55, 13);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(55, 15);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Score: {0}", score);
        }       
    }
}
