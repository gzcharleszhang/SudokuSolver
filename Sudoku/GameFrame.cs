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

        public bool CheckError(Grid[,] grids)
        {
            // 1 is taken, index is the value in the box
            bool[] lib;
            bool[] lib2;
            
            // Check Horizontal
            for (int i = 0; i < 9; i++)
            {
                lib = new bool[10];
                lib2 = new bool[10];
                for (int j = 0; j < 9; j++)
                {
                    int val = grids[i, j].Value;
                    int val2 = grids[j, i].Value;
                    if (lib[val])
                    {
                        return true;
                    }
                    else if (val != 0)
                    {
                        lib[val] = true;
                    }

                    if (lib2[val2])
                    {
                        return true;
                    }
                    else if (val2 != 0)
                    {
                        lib[val2] = true;
                    }
                }
            }
            
            // check sections
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (CheckSectionError(j * 3, i * 3, grids))
                    {
                        return true;
                    } 
                }
            }

            return false;
        }

        public bool CheckSectionError(int x, int y, Grid[,] grids)
        {
            bool[] lib = new bool[10];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int val = grids[x + j, y + i].Value;
                    if(lib[val])
                    {
                        return true;
                    }
                    else if (val != 0)
                    {
                        lib[val] = true;
                    }
                }
            }
            return false;
        }
    }
}
