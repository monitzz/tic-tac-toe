﻿namespace Coding.Exercise
{
    public static class TicTacToe
    {
        static char[,] slots =
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
        };

        static int rows = slots.GetLength(0);
        static int cols = slots.GetLength(1);
        static char winner = ' ';
        static bool gameWon = false;
        static bool isDraw = false;
        static string endGameResponse = "no";

        static void Main(string[] args)
        {
            endGameResponse = "no";

            while (true)
            {
                PrintBoard();
                CheckResult(slots);

                if (gameWon)
                {
                    PrintBoard();
                    Console.WriteLine("The winner is... " + winner + "!");
                }
                else if (isDraw)
                {
                    PrintBoard();
                    Console.WriteLine("This game ended in a draw.");
                }

                Console.Write("Would you like to play again? (yes, no) ");

                if (gameWon || isDraw)
                {
                    RepeatGame();
                }

                if ((gameWon || isDraw) && endGameResponse == "no")
                {
                    break;
                }
            }
        }

        public static void PrintBoard()
        {
            Console.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (j == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(slots[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (slots[i, j] == 'X' || slots[i, j] == 'x')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.Write(slots[i, j]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" | ");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine();

                if (i < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("--+---+--");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }

        public static void CheckResult(char[,] field)
        {
            char newChar;
            int rowPosition, columnPosition;

            while (true)
            {
                Console.Write("Insert X or O: ");
                while (true)
                {
                    char.TryParse(Console.ReadLine().ToUpper(), out newChar);

                    if (newChar == 'O' || newChar == 'o' || newChar == 'X' || newChar == 'x')
                    {
                        break;
                    }
                    Console.Write("Invalid input. Insert X or O: ");
                }

                Console.Write("Insert the row: ");
                while (true)
                {
                    string? rowResponse = Console.ReadLine();

                    if (int.TryParse(rowResponse, out rowPosition) && rowPosition >= 1 && rowPosition <= 3)
                    {
                        rowPosition--;
                        break;
                    }
                    Console.Write("Invalid input. Insert the row (1, 2, or 3): ");
                }

                Console.Write("Insert the column: ");
                while (true)
                {
                    string? columnResponse = Console.ReadLine();

                    if (int.TryParse(columnResponse, out columnPosition) && columnPosition >= 1 && columnPosition <= 3)
                    {
                        columnPosition--;
                        break;
                    }
                    Console.Write("Invalid input. Insert the column (1, 2, or 3): ");
                }

                if (field[rowPosition, columnPosition] == ' ')
                {
                    field[rowPosition, columnPosition] = newChar;
                    break;
                }
                else
                {
                    Console.WriteLine("That slot is already filled! Try again.");
                }
            }

            gameWon = IsRowIdentical(field, rowPosition) || IsColumnIdentical(field, columnPosition) || IsDiagonalIdentical(field);
            isDraw = !gameWon && IsBoardFull(field);
        }

        public static bool IsRowIdentical(char[,] array, int rowIndex)
        {
            int columnCount = array.GetLength(1);
            char firstValue = array[rowIndex, 0];
            if (firstValue == ' ')
                return false;

            for (int column = 1; column < columnCount; column++)
            {
                if (array[rowIndex, column] != firstValue)
                {
                    return false;
                }
            }

            winner = firstValue;
            return true;
        }

        public static bool IsColumnIdentical(char[,] array, int columnIndex)
        {
            int rowCount = array.GetLength(0);
            char firstValue = array[0, columnIndex];
            if (firstValue == ' ')
                return false;

            for (int row = 1; row < rowCount; row++)
            {
                if (array[row, columnIndex] != firstValue)
                {
                    return false;
                }
            }

            winner = firstValue;
            return true;
        }

        public static bool IsDiagonalIdentical(char[,] array)
        {
            int size = array.GetLength(0);
            char firstValueAtBackwardsDiagonal = array[0, 0];
            char firstValueAtForwardDiagonal = array[0, size - 1];

            bool isBackwardsDiagonalIdentical = firstValueAtBackwardsDiagonal != ' ';
            bool isForwardDiagonalIdentical = firstValueAtForwardDiagonal != ' ';

            for (int i = 1; i < size; i++)
            {
                if (array[i, i] != firstValueAtBackwardsDiagonal)
                {
                    isBackwardsDiagonalIdentical = false;
                }
                if (array[i, size - 1 - i] != firstValueAtForwardDiagonal)
                {
                    isForwardDiagonalIdentical = false;
                }
            }

            if (isBackwardsDiagonalIdentical)
            {
                winner = firstValueAtBackwardsDiagonal;
            }
            else if (isForwardDiagonalIdentical)
            {
                winner = firstValueAtForwardDiagonal;
            }

            return isBackwardsDiagonalIdentical || isForwardDiagonalIdentical;
        }

        public static bool IsBoardFull(char[,] array)
        {
            foreach (char slot in array)
            {
                if (slot == ' ')
                {
                    return false;
                }
            }

            return true;
        }
        
        public static void RepeatGame()
        {
            while (true)
            {
                endGameResponse = Console.ReadLine();

                if (endGameResponse == "yes" || endGameResponse == "no")
                {
                    break;
                }
            }

            if (endGameResponse == "yes")
            {
                for (int index = 0; index < rows * cols; index++)
                {
                    int i = index / cols;
                    int j = index % cols;

                    slots[i, j] = ' ';
                }
            }
        }
    }
}
