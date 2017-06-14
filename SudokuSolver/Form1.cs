using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuSolver : Form
    {
        TextBox[,] textBoxes = new TextBox[9, 9];
        int[,] grid = new int[9, 9];

        public SudokuSolver()
        {
            InitializeComponent();
            InitializeTextBoxes();
        }

        void InitializeTextBoxes()
        {
            int x = 20, y = 20;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBoxes[i, j] = new TextBox();
                    textBoxes[i, j].Location = new Point(x, y);
                    textBoxes[i, j].Size = new Size(20, 20);
                    Controls.Add(textBoxes[i, j]);
                    x += 20;
                }
                x = 20;
                y += 20;
            }
        }

        bool ReadInput()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!int.TryParse(textBoxes[i, j].Text, out grid[i, j]))
                    {
                        return false;
                    }
                    if (grid[i, j] < 0 || grid[i, j] > 9)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (!ReadInput())
            {
                MessageBox.Show("Error: Grid not valid!");
            }
        }
    }
}
