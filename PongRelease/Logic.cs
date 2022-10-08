using Menu;


namespace KeyboardMenu
{

    class Game
    {
        public void Start()
        {
            RunMainMenu();
        }
        public void RunMainMenu()
        {
            string Title = DataBase.Preview;
            string[] options = { "Play", "About", "Match History", "Exit" };
            Menu mainmenu = new Menu(Title, options);
            int selectedindex = mainmenu.Run();


            switch (selectedindex)
            {
                case 0:
                    PlayGame();
                    break;
                case 1:
                    DisplayAboutInfo();
                    break;
                case 2:
                    MatchHistory();
                    break;
                case 3:
                    ExitGame();
                    break;

            }

        }

        private void PlayGame()
        {
            UI.Clear();
            //Использование out-а
            Session session = new Session(PlayerName(out string Input, 1), PlayerName(out string Input2, 2));
            session.Start();
            DataBase.WriteDataInDatabase(session);
        }

        public string PlayerName(out string Input, int number)
        {
            UI.Clear();
            UI.MessageToInputName(number);
            Input = Console.ReadLine();
            if (Input == null)
            {
                Input = "DefaultPlayer1";
                return ($"{Input}");
            }
            UI.Clear();
            return Input;

        }

        public void DisplayAboutInfo()
        {
            string About = DataBase.About;
            UI.DisplayInfo(About);
            RunMainMenu();
        }

        private void MatchHistory()
        {
            UI.Clear();
            List<Player> PlayersList = DataBase.GetPlayersData();
            foreach (var item in PlayersList)
            {
                UI.PrintMatchHistory(item);
            }
            UI.Pause();
            RunMainMenu();
        }

        private void ExitGame()
        {
            Environment.Exit(0);
        }
    }

    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string WriteText;

        // Конструктор экземпляров
        public Menu(string Title, string[] options)
        {
            WriteText = Title;
            Options = options;
            SelectedIndex = 0;
        }


        public void DisplayOptions()
        {
            UI.Print(WriteText);
            
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];


                if (i == SelectedIndex)
                {
                    UI.SetColorToHighlight();
                }
                else
                {
                    UI.SetDefaultColor();
                }

                UI.PrintMenuElement(ref currentOption);
                UI.ResetColor();
            }

        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                UI.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }

}




