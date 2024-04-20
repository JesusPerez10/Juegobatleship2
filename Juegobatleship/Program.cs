using System;

class BattleshipGame
{
    static int boardSize = 10;
    static char[,] playerBoard = new char[boardSize, boardSize];
    static char[,] opponentBoard = new char[boardSize, boardSize];
    static Random random = new Random();
    static int playerShipsRemaining = 5;
    static int opponentShipsRemaining = 5;

    static void Main(string[] args)
    {
        InitializeBoards();
        PlaceShips(playerBoard);

        Console.WriteLine("¡Bienvenido a Battleship!");
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1. Jugar contra la computadora");
        Console.WriteLine("2. Jugar contra otro jugador");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("Opción inválida. Por favor, elija 1 o 2.");
        }

        if (choice == 1)
        {
            PlaceShips(opponentBoard);
            PlayAgainstComputer();
        }
        else
        {
            PlaceShips(playerBoard); // Reiniciamos los tableros para el segundo jugador
            Console.Clear();
            PlaceShips(opponentBoard);
            PlayAgainstPlayer();
        }

        Console.WriteLine("¡Juego terminado!");
    }

    static void InitializeBoards()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                playerBoard[i, j] = '~'; // Caracter que representa agua
                opponentBoard[i, j] = '~';
            }
        }
    }

    static void PlaceShips(char[,] board)
    {
        int shipsToPlace = 5; // Puedes ajustar el número de barcos según tus preferencias
        while (shipsToPlace > 0)
        {
            Console.Clear();
            DisplayBoard(board);
            Console.WriteLine($"Quedan {shipsToPlace} barcos por colocar.");

            Console.Write("Ingrese la fila donde desea colocar el barco (0-9): ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la columna donde desea colocar el barco (0-9): ");
            int col = int.Parse(Console.ReadLine());

            if (row >= 0 && row < boardSize && col >= 0 && col < boardSize && board[row, col] == '~')
            {
                board[row, col] = 'O'; // Caracter que representa un barco
                shipsToPlace--;
            }
            else
            {
                Console.WriteLine("Ubicación inválida. Inténtelo de nuevo.");
            }
        }
    }

    static void DisplayBoard(char[,] board)
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void PlayAgainstComputer()
    {
        Console.Clear();
        Console.WriteLine("Comienza el juego contra la computadora.");

        while (!GameIsOver())
        {
            DisplayBoards();
            PlayerTurn();
            if (!GameIsOver())
                ComputerTurn();
        }
    }

    static void PlayAgainstPlayer()
    {
        Console.Clear();
        Console.WriteLine("Comienza el juego entre dos jugadores.");

        while (!GameIsOver())
        {
            Console.WriteLine("Turno del Jugador 1:");
            PlayerTurn();
            if (!GameIsOver())
            {
                Console.Clear();
                Console.WriteLine("Comienza el turno del Jugador 2:");
                PlayerTurn();
            }
        }
    }

    static void DisplayBoards()
    {
        Console.Clear();
        Console.WriteLine("Tablero del Jugador:");
        DisplayBoard(playerBoard);
        Console.WriteLine("\nTablero del Oponente:");
        DisplayBoard(opponentBoard);
    }

    static void PlayerTurn()
    {
        {
            Console.WriteLine("Ingrese la fila para atacar (0-9): ");
            int row = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la columna para atacar (0-9): ");
            int col = int.Parse(Console.ReadLine());

            if (opponentBoard[row, col] == 'O')
            {
                Console.WriteLine("¡Has golpeado un barco enemigo!");
                opponentBoard[row, col] = 'X'; // Marcador de golpe
                opponentShipsRemaining--;
                Console.Beep(); // Sonido de beep
            }
            else if (opponentBoard[row, col] == '~')
            {
                Console.WriteLine("¡Has disparado al agua!");
                opponentBoard[row, col] = 'A'; // Marcador de agua
            }
            else
            {
                Console.WriteLine("Ya has disparado en esta ubicación.");
            }
            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void ComputerTurn()
    {
        
            int row = random.Next(boardSize);
            int col = random.Next(boardSize);

            if (playerBoard[row, col] == 'O')
            {
                Console.WriteLine("¡La computadora ha golpeado uno de tus barcos!");
                playerBoard[row, col] = 'X'; // Marcador de golpe
                playerShipsRemaining--;
                Console.Beep(); // Sonido de beep
            }
            else if (playerBoard[row, col] == '~')
            {
                Console.WriteLine("¡La computadora ha disparado al agua!");
                playerBoard[row, col] = 'M'; // Marcador de agua
            }
            else
            {
                Console.WriteLine("La computadora ya ha disparado en esta ubicación.");
            }
            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadLine();
        
    }

    static bool GameIsOver()
    {

        return playerShipsRemaining == 0 || opponentShipsRemaining == 0;
    }
}
