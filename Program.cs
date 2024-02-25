using System;

namespace Assignment2
{
    class GemHunters
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Gem Hunters Game!");
            Console.WriteLine("Try to collect the most gems.\n");
            Game gemHunters = new Game();
            gemHunters.Start();
        }
    }

    class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Player
    {
        public string Name;
        public Position Position;
        public int GemCount;

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
            GemCount = 0;
        }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    Console.WriteLine("Player moved up\n");
                    Position.Y--;
                    break;
                case 'D':
                    Console.WriteLine("Player moved down\n");
                    Position.Y++;
                    break;
                case 'L':
                    Console.WriteLine("Player moved left\n");
                    Position.X--;
                    break;
                case 'R':
                    Console.WriteLine("Player moved right\n");
                    Position.X++;
                    break;
            }
        }
    }

    class Cell
    {
        public string Occupant;

        public Cell(string occupant)
        {
            Occupant = occupant;
        }
    }

    class Board
    {
        public Cell[,] Grid;

        public Board()
        {
            Grid = new Cell[6, 6];
            // Initialize the grid with empty cells
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Grid[i, j] = new Cell("-");
                }
            }
            // Place players and gems
            Grid[0, 0].Occupant = "P1";
            Grid[5, 5].Occupant = "P2";
            Random random = new Random();
            // Place gems
            for (int i = 0; i < 4; i++)
            {
                int gemX = random.Next(0, 6);
                int gemY = random.Next(0, 6);
                while (Grid[gemX, gemY].Occupant != "-")
                {
                    gemX = random.Next(0, 6);
                    gemY = random.Next(0, 6);
                }
                Grid[gemX, gemY].Occupant = "G";
            }
            // Place obstacles
            for (int i = 0; i < 6; i++)
            {
                int obstacleX = random.Next(0, 6);
                int obstacleY = random.Next(0, 6);
                if (Grid[obstacleX, obstacleY].Occupant == "-")
                {
                    Grid[obstacleX, obstacleY].Occupant = "O";
                }
                else
                {
                    i--; // try again
                }
            }
        }

        public void Display(Player player1, Player player2)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (player1.Position.X == j && player1.Position.Y == i)
                    {
                        Console.Write("P1 ");
                    }
                    else if (player2.Position.X == j && player2.Position.Y == i)
                    {
                        Console.Write("P2 ");
                    }
                    else
                    {
                        Console.Write(Grid[i, j].Occupant + " ");
                    }
                }
                Console.WriteLine();
            }
        }


        public bool IsValidMove(Player player, char direction)
        {
            int newX = player.Position.X;
            int newY = player.Position.Y;
            switch (direction)
            {
                case 'U':
                    newY--;
                    break;
                case 'D':
                    newY++;
                    break;
                case 'L':
                    newX--;
                    break;
                case 'R':
                    newX++;
                    break;
            }

            // Check if the new position is within the board boundaries
            if (newX < 0 || newX >= 6 || newY < 0 || newY >= 6)
            {
                return false;
            }

            // Check if the new position contains an obstacle
            if (Grid[newY, newX].Occupant == "O")
            {
                return false;
            }

            // Check if the new position is occupied by the other player
            if (Grid[newY, newX].Occupant == (player.Name == "P1" ? "P2" : "P1"))
            {
                return false;
            }

            return true;
        }

        public void CollectGem(Player player)
        {
            if (Grid[player.Position.X, player.Position.Y].Occupant == "G")
            {
                player.GemCount++;
                Grid[player.Position.X, player.Position.Y].Occupant = "-";
            }
        }
    }

    class Game
    {
        public Board Board;
        public Player Player1;
        public Player Player2;
        public Player CurrentTurn;
        public int TotalTurns;

        public Game()
        {
            Board = new Board();
            Player1 = new Player("P1", new Position(0, 0)); // Start from the next position beside (0, 0)
            Player2 = new Player("P2", new Position(5, 5)); // Start from the next position beside (5, 5)
            CurrentTurn = Player1;
            TotalTurns = 0;
        }

        public void Start()
        {
            while (!IsGameOver())
            {
                Board.Display(Player1, Player2);
                Console.WriteLine($"\nCurrent turn: {CurrentTurn.Name}");
                Console.WriteLine("Enter move U for Up, D for Down, L for Left & R for right:");
                char move = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (Board.IsValidMove(CurrentTurn, move))
                {
                    CurrentTurn.Move(move);
                    Board.CollectGem(CurrentTurn);
                    TotalTurns++;
                }
                else
                {
                    Console.WriteLine(Board.IsValidMove(CurrentTurn, move) ? "Invalid move. Try again." : "Obstacle in the way. Try again.");
                    continue; // Skip turn switch if the move is invalid
                }

                // Switch turns only if the game is not over
                if (!IsGameOver())
                {
                    SwitchTurn();
                }
            }
            AnnounceWinner();
        }

        public void SwitchTurn()
        {
            CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
        }

        public bool IsGameOver()
        {
            return TotalTurns >= 30;
        }

        public void AnnounceWinner()
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Player 1 collected {Player1.GemCount} gems.");
            Console.WriteLine($"Player 2 collected {Player2.GemCount} gems.");
            if (Player1.GemCount > Player2.GemCount)
            {
                Console.WriteLine("Player 1 wins!");
            }
            else if (Player1.GemCount < Player2.GemCount)
            {
                Console.WriteLine("Player 2 wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }
}
