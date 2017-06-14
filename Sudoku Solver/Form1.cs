using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public partial class Form1 : Form
    {
        bool gameRunning = false;
        PointF[] boxPoints = new PointF[81];
        SizeF boxSize = new SizeF();
        RectangleF[] boxes = new RectangleF[81];
        int[] boxSelected = new int[81];
        int[] boxValue = new int[81];
        const int BOX_EMPTY = 0;
        const int BOX_SELECTED = 1;
        const int BOX_ORIGINAL = 2;
        Font numFont = new Font("Georgia", 25F);


        public Form1()
        {
            InitializeComponent();
            GameSetup();
        }

        void GameSetup()
        {
            boxSize.Width = 40;
            boxSize.Height = 40;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                    {
                        boxPoints[i * 9 + j].X = 3;
                    }
                    else if (j == 3 || j == 6)
                    {
                        boxPoints[i * 9 + j].X = boxPoints[i * 9 + j - 1].X + boxSize.Width + 3;
                    }

                    else
                    {
                        boxPoints[i * 9 + j].X = boxPoints[i * 9 + j - 1].X + boxSize.Width + 1;
                    }

                    if (i == 0)
                    {
                        boxPoints[i * 9 + j].Y = 3;
                    }
                    else if (i == 3 || i == 6)
                    {
                        boxPoints[i * 9 + j].Y = boxPoints[i * 9 + j - 9].Y + boxSize.Height + 3;
                    }

                    else
                    {
                        boxPoints[i * 9 + j].Y = boxPoints[i * 9 + j - 9].Y + boxSize.Height + 1;
                    }
                    boxes[i * 9 + j].Location = boxPoints[i * 9 + j];
                    boxes[i * 9 + j].Size = boxSize;
                }
            }
            for (int i = 0; i < 81; i++)
            {
                boxSelected[i] = BOX_EMPTY;
                boxValue[i] = 10;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (gameRunning == true)
            {
                e.Graphics.FillRectangle(Brushes.Black, 0, 0, boxSize.Width * 9 + 18, boxSize.Height * 9 + 18);
                for (int i = 0; i < 81; i++)
                {
                    if (boxSelected[i] == BOX_SELECTED)
                    {
                        e.Graphics.FillRectangle(Brushes.Blue, boxes[i]);
                    }

                    else if (boxSelected[i] == BOX_ORIGINAL)
                    {
                        e.Graphics.FillRectangle(Brushes.LightSalmon, boxes[i]);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.LightGray, boxes[i]);
                    }
                    if (boxValue[i] != 10)
                    {
                        e.Graphics.DrawString(boxValue[i].ToString(), numFont, Brushes.Black, boxes[i]);
                    }

                }
            }
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            Refresh();
            Application.DoEvents();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            gameRunning = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 81; i++)
            {
                if (boxSelected[i] == BOX_SELECTED)
                {
                    boxSelected[i] = BOX_EMPTY;
                }
                if (PointToClient(MousePosition).X > boxes[i].Left && PointToClient(MousePosition).X < boxes[i].Right && PointToClient(MousePosition).Y > boxes[i].Top && PointToClient(MousePosition).Y < boxes[i].Bottom)
                {
                    boxSelected[i] = BOX_SELECTED;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < 81; i++)
            {
                if (boxSelected[i] == BOX_SELECTED)
                {
                    if (e.KeyCode == Keys.D1)
                    {
                        boxValue[i] = 1;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D2)
                    {
                        boxValue[i] = 2;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D3)
                    {
                        boxValue[i] = 3;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D4)
                    {
                        boxValue[i] = 4;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D5)
                    {
                        boxValue[i] = 5;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D6)
                    {
                        boxValue[i] = 6;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D7)
                    {
                        boxValue[i] = 7;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D8)
                    {
                        boxValue[i] = 8;
                        boxSelected[i] = BOX_ORIGINAL;
                    }
                    else if (e.KeyCode == Keys.D9)
                    {
                        boxValue[i] = 9;
                        boxSelected[i] = BOX_ORIGINAL;
                    }

                }
                if (CheckError() == true)
                {
                    boxValue[i] = 10;
                    boxSelected[i] = BOX_SELECTED;
                }
            }
        }

        bool CheckSectionError(int[] values)
        {
            int error = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 9 - i; j++)
                {
                    if (values[i] == values[i+j] && values[i] != 10)
                    {
                        error = 1;
                        break;
                    }
                }
            }

            if (error == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckError()
        {
            int error = 0;
            //check horizontal
            for (int i = 0; i<27; i++)
            {
                int[] section = new int[9];
               
                for (int j = 0; j < 9; j++)
                {
                    if (i >= 0 && i <= 8)
                    {
                        section[j] = boxValue[i * 9 + j];

                    }
                    else if (i >=9 && i<= 17)
                    {
                        section[j] = boxValue[j * 9 + i - 9];
                    }

                    else if (i >= 18 && i <= 20)
                    {
                        if (j >=0 && j<=2)
                        {
                            section[j] = boxValue[j + (i-18)*3];
                        }
                        else if (j >=3 && j<=5)
                        {
                            section[j] = boxValue[j + (i - 18) * 3 + 6];
                        }
                        else
                        {
                            section[j] = boxValue[j + (i - 18) * 3 + 12];
                        }
                    }

                    else if (i >= 21 && i <= 23)
                    {
                        if (j >= 0 && j <= 2)
                        {
                            section[j] = boxValue[j + (i - 21) * 3 + 27];
                        }
                        else if (j >= 3 && j <= 5)
                        {
                            section[j] = boxValue[j + (i - 21) * 3 + 33];
                        }
                        else
                        {
                            section[j] = boxValue[j + (i - 21) * 3 + 39];
                        }
                    }
                    else
                    {
                        if (j >= 0 && j <= 2)
                        {
                            section[j] = boxValue[j + (i - 24) * 3 + 54];
                        }
                        else if (j >= 3 && j <= 5)
                        {
                            section[j] = boxValue[j + (i - 24) * 3 + 60];
                        }
                        else
                        {
                            section[j] = boxValue[j + (i - 24) * 3 + 66];
                        }
                    }
                }

                if (CheckSectionError(section) == true)
                {
                    error = 1;
                    break;
                }
            }

            if (error == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void lblSolve_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < 81; a++)
            {
                for (int i = a; i < 81 + a; i++)
                {
                    if (boxSelected[i%81] == BOX_EMPTY)
                    {
                        for (int j = 1; j < 10; j++)
                        {
                            boxValue[i%81] = j;
                            if (CheckError() == false)
                            {
                                break;
                            }
                        }
                    }
                }

                if (CheckError() == false)
                {
                    break;
                }
            }
        }
    }
}
