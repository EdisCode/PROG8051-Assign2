# Gem Hunters Game

Gem Hunters is a simple console-based 2D game where players compete to collect the most gems within a set number of turns.

## Objective

The objective of the game is to collect as many gems as possible within 30 moves (15 turns for each player). Players can move their character around the board and collect gems while avoiding obstacles.

## Features

- **Board Size**: A 6x6 square board.
- **Players**: 2 player game. Player 1 starts in the top-left corner and Player 2 starts in the bottom-right corner.
- **Gems**: Randomly placed on the board at the start of the game. Gems do not move once placed.
- **Obstacles**: Random positions on the board that players cannot pass through.
- **Turns**: Players alternate turns, with each player getting 15 turns.
- **Winning**: The player with the most gems collected after all turns are exhausted wins. If both players have the same number of gems, it's a tie.

## How to Play

1. **Clone the Repository:**
    ```bash
    git clone https://github.com/EdisCode/PROG8051-Assign2.git
    ```

2. **Navigate to the Project Directory:**
    ```bash
    cd PROG8051-Assign2
    ```

3. **Run the Application:**
    ```bash
    dotnet run
    ```

4. **Follow On-screen Instructions:**
    - Input your moves (U for up, D for down, L for left, R for right).
    - Collect gems while avoiding obstacles.
    - The game ends after 30 moves or when all gems are collected, and the player with the most gems wins.

## Implementation Details

The game is implemented in C# and consists of the following classes:

- `Position`: Represents a position on the game board.
- `Player`: Represents a player in the game, with methods for moving and collecting gems.
- `Cell`: Represents a cell on the game board, which can contain a player, gem, obstacle, or be empty.
- `Board`: Represents the game board, with methods for initializing the board, displaying the current state, and validating moves.
- `Game`: Manages the game state, including player turns, total turns, and win conditions.

## Video Link

Follow this [Link](https://www.loom.com/share/3e9f5575c3eb47cdb090e490cb049502?sid=ad2fedd5-50cb-44d3-a177-a371b67bdb7b) to watch the implementation video.

Enjoy the hunt! 🎊
And remember finders keepers. 😉