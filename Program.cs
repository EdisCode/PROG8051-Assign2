using System;

namespace Assignment2
{
    class GemHunters
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Gem Hunters Game!");
            Console.WriteLine("Try to collect the most gems.");
            Game gemHunters = new Game();
            gemHunters.Start();
        }
    }

    class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Player
    {
        public string Name { get; }
        public Position Position { get; set; }
        public int GemCount { get; set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
            GemCount = 0;
        }

        public Position Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    Position = new Position(Position.X - 1, Position.Y);
                    break;
                case 'D':
                    Position = new Position(Position.X + 1, Position.Y);
                    break;
                case 'L':
                    Position = new Position(Position.X, Position.Y - 1);
                    break;
                case 'R':
                    Position = new Position(Position.X, Position.Y + 1);
                    break;
            }
            return Position;
        }
    }

    class Cell
    {
        public string Occupant { get; set; }

        public Cell(string occupant)
        {
            Occupant = occupant;
        }
    }

    class Board
    {
        public Cell[,] Grid { get; }

        public Board()
        {
            Grid = new Cell[6, 6]; // 6x6 grid
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Initialize the grid with empty cells
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Grid[i, j] = new Cell("-");
                }
            }

            // Place players
            Grid[0, 0].Occupant = "P1";
            Grid[5, 5].Occupant = "P2";

            // Place gems (for simplicity, randomly place 6 gems)
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                int x = random.Next(0, 6);
                int y = random.Next(0, 6);
                if (Grid[x, y].Occupant == "-")
                {
                    Grid[x, y].Occupant = "G";
                }
                else
                {
                    i--; // try again
                }
            }

            // Place obstacles (for simplicity, randomly place 5 obstacles)
            for (int i = 0; i < 5; i++)
            {
                int x = random.Next(0, 6);
                int y = random.Next(0, 6);
                if (Grid[x, y].Occupant == "-")
                {
                    Grid[x, y].Occupant = "O";
                }
                else
                {
                    i--; // try again
                }
            }
        }

        public void Display()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(Grid[i, j].Occupant + " ");
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
                    newX--;
                    break;
                case 'D':
                    newX++;
                    break;
                case 'L':
                    newY--;
                    break;
                case 'R':
                    newY++;
                    break;
            }

            // Check if the new position is within the board boundaries
            if (newX < 0 || newX >= 6 || newY < 0 || newY >= 6)
                return false; // Out of bounds


            //Check if the new position contains an obstacle
            if (Grid[newX, newY].Occupant == "O")
            {

                Console.WriteLine("Obstacle in the way.");
                return false; // Obstacle encountered
            }

            return true;
        }

        public bool CollectGem(Player player)
        {
            if (Grid[player.Position.X, player.Position.Y].Occupant == "G")
            {
                player.GemCount++;
                Grid[player.Position.X, player.Position.Y].Occupant = "-";
                return true;
            }
            return false;
        }
    }

    class Game
    {
        private readonly Board Board;
        private readonly Player Player1;
        private readonly Player Player2;
        private Player CurrentTurn;
        private int TotalTurns;

        public Game()
        {
            Board = new Board();
            Player1 = new Player("P1", new Position(0, 0));
            Player2 = new Player("P2", new Position(5, 5));
            CurrentTurn = Player1;
            TotalTurns = 0;
        }

        public void Start()
        {
            while (!IsGameOver())
            {
                // Display the board before the player makes a move
                Console.WriteLine();
                Board.Display();

                Console.WriteLine($"\n{CurrentTurn.Name}'s turn.");
                Console.Write("Enter move U for Up, D for Down, L for Left & R for right: ");
                char direction = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (Board.IsValidMove(CurrentTurn, direction))
                {
                    Position previousPosition = CurrentTurn.Position;
                    Position currentPosition = CurrentTurn.Move(direction);
                   

                    bool collectedGem = Board.CollectGem(CurrentTurn);

                    string directionString = "";
                    switch (direction)
                    {
                        case 'U':
                            directionString = "up";
                            break;
                        case 'D':
                            directionString = "down";
                            break;
                        case 'L':
                            directionString = "left";
                            break;
                        case 'R':
                            directionString = "right";
                            break;
                    }

                    Console.WriteLine($"{CurrentTurn.Name} moved {directionString}.");

                    // Check if the player collected a gem
                    if (collectedGem)
                    {
                        Console.WriteLine($"{CurrentTurn.Name} collected a gem!");
                    }

                    // Check if there are any remaining gems
                    if (!Board.Grid.Cast<Cell>().Any(cell => cell.Occupant == "G"))
                    {
                        // End the game if there are no more gems
                        break;
                    }

                    Board.Grid[previousPosition.X, previousPosition.Y].Occupant = "-";
                    Board.Grid[currentPosition.X, currentPosition.Y].Occupant = CurrentTurn.Name;

                    // Switch turn after each move
                    SwitchTurn();
                    TotalTurns++;
                }

                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                    continue;  // Skip turn switch if the move is invalid
                }

            }

            AnnounceWinner();
        }

        private void SwitchTurn()
        {
            CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
        }

        private bool IsGameOver()
        {
            return TotalTurns >= 30; // 15 turns per player
        }

        private void AnnounceWinner()
        {
            Console.WriteLine("\nGame over!");
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