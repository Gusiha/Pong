namespace KeyboardMenu
{
    static class UI
    {
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
    }
}
