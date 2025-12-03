using System;
using System.Collections.Generic;

public static class SudokuValidator
{
    public static bool IsValid(int[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            var row = new HashSet<int>();
            var col = new HashSet<int>();
            for (int j = 0; j < 9; j++)
            {
                int r = board[i, j];
                int c = board[j, i];
                if (r != 0 && !row.Add(r)) return false;
                if (c != 0 && !col.Add(c)) return false;
            }
        }
        for (int box = 0; box < 9; box++)
        {
            var square = new HashSet<int>();
            int startRow = (box / 3) * 3;
            int startCol = (box % 3) * 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    int val = board[startRow + i, startCol + j];
                    if (val != 0 && !square.Add(val)) return false;
                }
        }
        return true;
    }

    public static bool Solve(int[,] board)
    {
        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                if (board[r, c] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        board[r, c] = num;
                        if (IsValid(board) && Solve(board)) return true;
                        board[r, c] = 0;
                    }
                    return false;
                }
            }
        }
        return true;
    }
}
