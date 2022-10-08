using System.Diagnostics;

namespace KeyboardMenu
{
    static class UI
    {
        // Методы типа
        public static void PrintMatchHistory(Player item)
        {
            Console.WriteLine($"Name : {item.Name}, Points : {item.Points}");
        }

        public static void DisplayInfo(string str)
        {
            Console.Clear();
            Console.WriteLine(str);
            Console.ReadKey(true);
        }

        public static void Pause()
        {
            Console.ReadKey();
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void Print(string str)
        {
            Console.WriteLine(str);
        }

        public static void Print(char ball)
        {
            Console.WriteLine(ball);
        }

        public static void Print(TimeSpan GameDuration)
        {
            Console.WriteLine(GameDuration);
        }


        public static void ResetColor()
        {
            Console.ResetColor();
        }

        public static void SetColorToHighlight()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }

        public static void SetDefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Метод с параметром ref
        public static void PrintMenuElement(ref string currentOption)
        {
            Console.WriteLine($" << {currentOption} >> \n");
        }

        public static void DisableCursor()
        {
            Console.CursorVisible = false;
        }

        public static void MessageToInputName(int number)
        {
            Console.WriteLine($"Введите имя игрока N{number}");
        }

        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public static void PrintScores(int LeftPlayerPoints, int RightPlayerPoints)
        {
            Console.WriteLine($"{LeftPlayerPoints} | {RightPlayerPoints}");
        }


        public static void PrintPlayerNames(string LeftPlayerName, string RightPlayerName)
        {
            Console.WriteLine($"{LeftPlayerName}   |   {RightPlayerName}");
        }
    }
}
