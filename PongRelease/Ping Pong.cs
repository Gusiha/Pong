using Menu;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;



//TODO Общая задача - при нажатии "Exit" все закрывалось сразу(вместе с консолью)
namespace KeyboardMenu
{
    public class Session
    {
        public int SessionId { get; set; }

        public Ball ball { get; set; }
        public Player LeftPlayer { get; set; }
        public Player RightPlayer { get; set; }
        public Field field { get; set; }
        public Scoreboard ScoreBoard { get; set; }
        public DateTime StartTime { get; set; }
        public Stopwatch GameDuration { get; set; }

        public bool FinishFlag { get; set; }
        public Session()
        {

        }
        public Session(string LeftPlayerName, string RightPlayerName)
        {
            ball = new Ball();
            LeftPlayer = new Player(LeftPlayerName);
            RightPlayer = new Player(RightPlayerName);
            field = new Field();
            ScoreBoard = new Scoreboard();
            StartTime = DateTime.Now;
            GameDuration = new Stopwatch();
            FinishFlag = false;
        }

        public void UpdateRound()
        {
            UI.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y);
            UI.PrintScores(LeftPlayer.Points, RightPlayer.Points);

            UI.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y + 1);
            UI.PrintPlayerNames(LeftPlayer.Name, RightPlayer.Name);

            //TODO перенести это в данные: DateTime.Now.ToShortTimeString();

            UI.SetCursorPosition(ball.X, ball.Y);
            UI.Print(Ball.BallTile);
            Thread.Sleep((int)DataBase.GameSettings.speed); //Adds a timer so that the players have time to react


            UI.SetCursorPosition(ball.X, ball.Y);
            UI.Print(" "); //Clears the previous position of the ball

            TimeSpan ts = GameDuration.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            UI.SetCursorPosition(Field.fieldLength / 2 - 2, Field.fieldWidth + 3);
            UI.Print(elapsedTime);
        }
        public void UpdateBall()
        {
            if (ball.isBallGoingDown)
            {
                ball.Y++;
            }
            else
            {
                ball.Y--;
            }

            if (ball.isBallGoingRight)
            {
                ball.X++;
            }
            else
            {
                ball.X--;
            }

            if (ball.Y == 1 || ball.Y == Field.fieldWidth - 1)
            {
                ball.isBallGoingDown = !ball.isBallGoingDown; //Change direction
            }
        }
        public void UpdateRackets()
        {
            for (int i = 1; i < Field.fieldWidth; i++)
            {
                UI.SetCursorPosition(0, i);
                UI.Print(" ");
                UI.SetCursorPosition(Field.fieldLength - 1, i);
                UI.Print(" ");
            }
        }
        public bool IsRightKnockBall()
        {
            if (ball.Y >= LeftPlayer.Racket.Height + 1 && ball.Y <= LeftPlayer.Racket.Height + Rackets.Length) //Left racket hits the ball and it bounces
            {
                //TODO Убрать отсюда смену направления
                return false;
            }
            return true;
        }
        public bool IsLeftKnockBall()
        {

            if (ball.Y >= RightPlayer.Racket.Height + 1 && ball.Y <= RightPlayer.Racket.Height + Rackets.Length) //Right racket hits the ball and it bounces
            {
                //TODO Убрать отсюда смену направления
                return false;
            }
            return true;
        }
        public void PrintTheRacket()
        {
            for (int i = 0; i < Rackets.Length; i++)
            {
                UI.SetCursorPosition(0, i + 1 + LeftPlayer.Racket.Height);
                UI.Print(Rackets.Tile);
                UI.SetCursorPosition(Field.fieldLength - 1, i + 1 + RightPlayer.Racket.Height);
                UI.Print(Rackets.Tile);
            }
        }
        public void Start()
        {
            GameDuration.Start();
            while (!FinishFlag)
            {
                field.CreateField();
                PrintTheRacket();

                while (!Console.KeyAvailable)
                {
                    UpdateRound();
                    UpdateBall();
                    UI.SetCursorPosition(ScoreBoard.X, ScoreBoard.Y);
                    UI.Print($"{LeftPlayer.Points} | {RightPlayer.Points}");

                    //TODO Исправить повторяющийся код
                    if (ball.X == 1)
                        if (!IsRightKnockBall()) { ball.isBallGoingRight = !ball.isBallGoingRight; }
                        else
                        {
                            RightPlayer.Points++;
                            ball.Y = Field.fieldWidth / 2;
                            ball.X = Field.fieldLength / 2;

                            if (RightPlayer.Points == 1)
                            {
                                GameDuration.Stop();
                                RightPlayer.Win(GameDuration);
                                FinishFlag = true;
                                break;
                            }
                        }
                    if (ball.X == Field.fieldLength - 2)
                        if (!IsLeftKnockBall()) { ball.isBallGoingRight = !ball.isBallGoingRight; }
                        else
                        {
                            LeftPlayer.Points++;
                            ball.Y = Field.fieldWidth / 2;
                            ball.X = Field.fieldLength / 2;

                            if (LeftPlayer.Points == 1)
                            {
                                GameDuration.Stop();
                                LeftPlayer.Win(GameDuration);
                                FinishFlag = true;
                                break;
                            }
                        }


                }

                switch (Console.ReadKey().Key)
                {

                    //TODO Добавить в UI ConsoleKey
                    case ConsoleKey.Escape:
                        {

                            UI.SetCursorPosition(Field.fieldLength / 2, Field.fieldWidth / 2);
                            UI.Print("_Paused");
                            Console.ReadKey();
                            UI.Clear();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (RightPlayer.Racket.Height > 0)
                        {
                            RightPlayer.Racket.Height--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (RightPlayer.Racket.Height < Field.fieldWidth - Rackets.Length - 1)
                        {
                            RightPlayer.Racket.Height++;

                        }
                        break;

                    case ConsoleKey.W:
                        if (LeftPlayer.Racket.Height > 0)
                        {
                            LeftPlayer.Racket.Height--;
                        }
                        break;

                    case ConsoleKey.S:
                        if (LeftPlayer.Racket.Height < Field.fieldWidth - Rackets.Length - 1)
                        {
                            LeftPlayer.Racket.Height++;
                        }
                        break;
                }
                UpdateRackets();


            }


        }
    }
    //TODO Возможно добавить все классы в DataBase
    public class Player
    {
        // Свойства (автосвойства)
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        [NotMapped]
        public Rackets Racket { get; set; }

        // Метод экземпляра
        public void Win(Stopwatch GameDuration)
        {
            UI.Clear();
            UI.SetCursorPosition(0, 0);
            UI.Print($"{Name} победил!");
            UI.Print(GameDuration.Elapsed);
        }

        public Player()
        {

        }

        public Player(string name)
        {
            Name = name;
            Points = 0;
            Racket = new Rackets();
        }
    }



    public class Field
    {
        //Field
        public static int fieldLength = (int)DataBase.GameSettings.fieldLength;
        public static int fieldWidth = (int)DataBase.GameSettings.fieldWidth;


        public const char fieldTile = '#';
        public string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));

        public void CreateField()
        {
            //Print the borders
            UI.SetCursorPosition(0, 0);
            UI.Print(line);

            UI.SetCursorPosition(0, fieldWidth);
            UI.Print(line);
        }
    }

    public class Rackets
    {
        public int RacketsId { get; set; }
        public static int Length = Field.fieldWidth / 4;
        public const char Tile = '|';
        public int Height { get; set; } = 0;


    }

    public class Ball
    {
        public int X { get; set; } = Field.fieldLength / 2;
        public int Y { get; set; } = Field.fieldWidth / 2;

        public const char BallTile = 'O';

        public bool isBallGoingDown { get; set; } = true;
        public bool isBallGoingRight { get; set; } = true;

    }

    public class Scoreboard
    {
        //Scoreboard
        public int X { get; private set; } = Field.fieldLength / 2 - 2;
        public int Y { get; private set; } = Field.fieldWidth + 1;
    }

}