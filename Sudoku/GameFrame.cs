using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class GameFrame
    {
        public GameFrame()
        {

        }

        public bool IsError(Grid[,] grid)
        {
            // True means the value is being used
            // lib is for the current row; lib2 for the column
            bool[] lib;
            bool[] lib2;
            
            // Check conflicts in the row and column simultaneously
            for (int i = 0; i < 9; i++)
            {
                lib = new bool[10];
                lib2 = new bool[10];

                for (int j = 0; j < 9; j++)
                {
                    int val = grid[i, j].Value;
                    int val2 = grid[j, i].Value;

                    if (lib[val] || val == 0)
                    {
                        return true;
                    }
                    if (val != 0)
                    {
                        lib[val] = true;
                    }

                    if (lib2[val2] || val2 == 0)
                    {
                        return true;
                    }
                    if (val2 != 0)
                    {
                        lib2[val2] = true;
                    }
                }
            }
            
            // Check squares
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (IsError(j * 3, i * 3, grid))
                    {
                        return true;
                    } 
                }
            }

            return false;
        }

        public bool IsError(int x, int y, Grid[,] grid)
        {
            bool[] lib = new bool[10];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int val = grid[x + j, y + i].Value;

                    if(lib[val] || val == 0)
                    {
                        return true;
                    }
                    if (val != 0)
                    {
                        lib[val] = true;
                    }
                }
            }

            return false;
        }
    }
}
