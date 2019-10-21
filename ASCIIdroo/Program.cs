using System;

namespace ASCIIhome
{
    class Program
    {
        static int width = 50;
        static int height = 50;
        static int result;
        static int[,] map = new int[width*2, height*2];
        static string[,] hiddenMap = new string[width, height];
        static string[] commands = {
            "help",
            "line",
            "exit",
            "width",
            "height",
            "draw",
            "clear"
        };
        static void Main(string[] args)
        {
            Init();
            Console.WriteLine("To see available commands, do 'help'");
            Command(Console.ReadLine());
        }
        static void Init()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    hiddenMap[x, y] = "_";
                }
            }


        }
        static void Command(string input)
        {
            switch (input.ToLower())
            {
                default: Console.WriteLine("Command not recognized"); Command(Console.ReadLine()); break;
                case "help":
                    Console.WriteLine("The available commands are: ");
                    for (int x = 0; x < commands.Length; x++)
                    {
                        Console.WriteLine("   " + commands[x]);
                    };
                    Command(Console.ReadLine());
                    break;
                case "exit": break;
                case "width":
                    Console.WriteLine("Please enter the desired width of the field");
                    try
                    {
                        //if (int.TryParse(Console.ReadLine(), out int result) == true)
                        //{
                            result = int.Parse(Console.ReadLine());
                        if(result < 0)
                        {
                            Console.WriteLine("This cannot be negative!");
                            result = Math.Abs(result);
                        }
                        //}
                    }
                    catch(SystemException e) { Console.WriteLine(e.Message);Command(Console.ReadLine());break;}
                    width = result;
                    map = new int[width*2, height*2];
                    hiddenMap = new string[width, height];
                    Init();
                    Console.WriteLine($"Width has been succesfully changed to: {result}");
                    Command(Console.ReadLine());
                    break;
                case "height":
                    Console.WriteLine("Please enter the desired height of the field");
                    try
                    {
                        //if (int.TryParse(Console.ReadLine(), out int result) == true)
                        //{
                        result = int.Parse(Console.ReadLine());
                        if (result < 0)
                        {
                            Console.WriteLine("This cannot be negative!");
                            result = Math.Abs(result);
                        }
                        //}
                    }
                    catch (SystemException e) { Console.WriteLine(e.Message);Command(Console.ReadLine());break;}
                    height = result;
                    map = new int[width*2, height*2];
                    hiddenMap = new string[width, height];
                    Init();
                    Console.WriteLine($"Height has been succesfully changed to: {result}");
                    Command(Console.ReadLine());
                    break;
                case "draw":
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Console.Write($" {hiddenMap[x, y]}");
                        }
                        Console.WriteLine(" ");
                    }
                    Console.WriteLine("Would you like to see the hidden canvas? (Y/N)");//s
                    if (Console.ReadLine().ToLower() == "y") {
                        MakeChars();
                        for (int y = 0; y < height*2; y++)
                        {
                            for (int x = 0; x < width*2; x++)
                            {
                                Console.Write($" {map[x, y]}");
                            }
                            Console.WriteLine(" ");
                        }
                    }
                    Command(Console.ReadLine()); 
                    break;
                case "line":
                    Console.WriteLine("Please insert the coordinates of your lines' end (x1 y1 x2 y2)");
                    string[] inputArray = Console.ReadLine().Split(' ');
                    try
                    {
                        int[] parsedInput = {
                        int.Parse(inputArray[0])*2,
                        int.Parse(inputArray[1])*2,
                        int.Parse(inputArray[2])*2,
                        int.Parse(inputArray[3])*2
                    };
                        map[parsedInput[0], parsedInput[1]] = 1;
                        double deltaX = parsedInput[2] - parsedInput[0];
                        double deltaY = parsedInput[3] - parsedInput[1];
                        double steps = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY));
                        double[] currentXY = {
                            parsedInput[0],
                            parsedInput[1]
                        };
                        for (int i = 0; i < steps+2; i++)
                        {
                            map[Convert.ToInt32(Math.Round(currentXY[0])), Convert.ToInt32(Math.Round(currentXY[1]))] = 1;
                            currentXY[0] += deltaX / steps;
                            currentXY[1] += deltaY / steps;
                        }
                    }
                    catch (SystemException e)
                    {
                        Console.WriteLine(e.Message);
                    };
                    Command("draw");
                    Command(Console.ReadLine());
                    break;
                case "clear": Init(); Command("draw"); Command(Console.ReadLine()); break;
            }
        }

        static void MakeChars() {
            string[] chars = new string[4];
            for (int x = 0; x < width*2; x+=2) {

                for (int y = 0; y < height*2; y+=2) {
                    chars[0] = $"{map[x, y]}";
                    chars[1] = $"{map[x + 1, y]}";
                    chars[2] = $"{map[x, y + 1]}";
                    chars[3] = $"{map[x + 1, y + 1]}";
                    Console.WriteLine($"{chars[0]}{chars[1]}{chars[2]}{chars[3]}");
                    switch ($"{chars[0]}{chars[1]}{chars[2]}{chars[3]}") {
                        case "0000": hiddenMap[x / 2, y / 2] = "_" ; break;
                        case "0001": hiddenMap[x / 2, y / 2] = "_"; break;
                        case "0010": hiddenMap[x / 2, y / 2] = "_"; break;
                        case "0100": hiddenMap[x / 2, y / 2] = "_"; break;
                        case "1000": hiddenMap[x / 2, y / 2] = "_"; break;
                        case "0011": hiddenMap[x / 2, y / 2] = "-"; break;
                        case "0101": hiddenMap[x / 2, y / 2] = "|"; break;
                        case "1001": hiddenMap[x / 2, y / 2] = "\\"; break;
                        case "1011": hiddenMap[x / 2, y / 2] = "X"; break;
                        case "0111": hiddenMap[x / 2, y / 2] = "X"; break;
                        case "0110": hiddenMap[x / 2, y / 2] = "/"; break;
                        case "1100": hiddenMap[x / 2, y / 2] = "-";break;
                        case "1101": hiddenMap[x / 2, y / 2] = "x";break;
                        case "1110": hiddenMap[x / 2, y / 2] = "x"; break;
                        case "1010": hiddenMap[x / 2, y / 2] = "|"; break;
                        case "1111":Console.Write("X"); break;
                    }
                }                
            }
        }

    }
}
