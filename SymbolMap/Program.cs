namespace SymbolMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;
            char player = Convert.ToChar(File.ReadAllText("character.txt"));
            char wall = Convert.ToChar(File.ReadAllText("wall.txt"));
            int[] playerPosition = { 5, 5 };
            char[,] map = CreateMapFromFile("map.txt");

            Console.CursorVisible = false;

            while (isWorking)
            {
                DrawLevel(map, player, playerPosition);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey(true);

                    if (userInput.Key == ConsoleKey.Escape)
                    {
                        isWorking = false;
                    }
                    else
                    {
                        ApplyInput(userInput, map, wall, playerPosition);
                    }
                }
            }
        }

        static void DrawLevel(char[,] map, char player, int[] playerPosition)
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (x == playerPosition[0] && y == playerPosition[1])
                    {
                        DrawPlayer(player, playerPosition);
                    }
                    else
                    {
                        Console.Write(map[x, y]);
                    }
                }

                Console.WriteLine();
            }
        }

        static char[,] CreateMapFromFile(string filePath)
        {
            string[] file = File.ReadAllLines(filePath);
            char[,] map = new char[file[0].Length, file.Length];

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = file[y][x];
                }
            }

            return map;
        }

        static void DrawPlayer(char player, int[] position)
        {
            Console.SetCursorPosition(position[0], position[1]);
            Console.Write(player);
        }

        static void MovePlayer(char[,] map, int[] charCordinates, int charDirectionX, int charDirectionY, char wallChar)
        {
            if (map[charCordinates[0] + charDirectionX, charCordinates[1] + charDirectionY] != wallChar)
            {
                charCordinates[0] += charDirectionX;
                charCordinates[1] += charDirectionY;
            }
        }

        static void ApplyInput(ConsoleKeyInfo userInput, char[,] map, char wallChar, int[] charCordinates)
        {
            const ConsoleKey UpArrow = ConsoleKey.UpArrow;
            const ConsoleKey DownArrow = ConsoleKey.DownArrow;
            const ConsoleKey LeftArrow = ConsoleKey.LeftArrow;
            const ConsoleKey RightArrow = ConsoleKey.RightArrow;

            int directionX = 0;
            int directionY = 0;

            switch (userInput.Key)
            {
                case UpArrow:
                    directionY = -1;
                    break;

                case DownArrow:
                    directionY = +1;
                    break;

                case LeftArrow:
                    directionX = -1;
                    break;

                case RightArrow:
                    directionX = +1;
                    break;
            }

            MovePlayer(map, charCordinates, directionX, directionY, wallChar);
        }
    }
}
