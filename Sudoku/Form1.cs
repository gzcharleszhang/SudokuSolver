using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        Grid[,] grid = new Grid[9,9];
        GameFrame game = new GameFrame();

        RectangleF submitButton;
        SizeF gridSize = new SizeF(40, 40);

        Font numFont = new Font("Average", 25F);
        Font buttonFont = new Font("Georgia", 20F);

        bool isRunning = false;
        bool submitHovered = false;

        const int BIG_BORDER = 3;
        const int SMALL_BORDER = 1;
        const int INTERACT_SIZE = 200;

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        void SetupForm()
        {
            int boardWidth = (int)(gridSize.Width * 9 + BIG_BORDER * 2 + SMALL_BORDER * 2);
            int boardHeight = (int)(gridSize.Height * 9 + BIG_BORDER * 4 + SMALL_BORDER * 6);
            SetClientSizeCore(boardWidth + INTERACT_SIZE, boardHeight);

            PointF gridPoint = new PointF();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                    {
                        gridPoint.X = BIG_BORDER;
                    }
                    else if (j == 3 || j == 6)
                    {
                        gridPoint.X = grid[i, j - 1].Point.X + gridSize.Width + BIG_BORDER;
                    }
                    else
                    {
                        gridPoint.X = grid[i, j - 1].Point.X + gridSize.Width +  SMALL_BORDER;
                    }

                    if (i == 0)
                    {
                        gridPoint.Y = BIG_BORDER;
                    }
                    else if (i == 3 || i == 6)
                    {
                        gridPoint.Y = grid[i - 1, j].Point.Y + gridSize.Height + BIG_BORDER;
                    }
                    else
                    {
                        gridPoint.Y = grid[i - 1, j].Point.Y + gridSize.Height + SMALL_BORDER;
                    }

                    grid[i, j] = new Grid(gridSize, gridPoint, StateType.Empty);
                }
            }

            SizeF submitButtonSize = new SizeF(100, 30);
            PointF submitButtonPoint = new PointF(boardWidth + submitButtonSize.Width/2,
                                                  ClientSize.Height - submitButtonSize.Height- 10);

            submitButton.Size = submitButtonSize;
            submitButton.Location = submitButtonPoint;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isRunning)
            {
                e.Graphics.FillRectangle(Brushes.BurlyWood, 0, 0, ClientSize.Width, ClientSize.Height);
                e.Graphics.FillRectangle(Brushes.Black, 0, 0, gridSize.Width * 9 + 18,
                                         gridSize.Height * 9 + 18);

                if (submitHovered)
                {
                    e.Graphics.FillRectangle(Brushes.Black, submitButton);
                    e.Graphics.DrawString("Submit", buttonFont, Brushes.White, submitButton);
                }
                else
                {
                    e.Graphics.DrawString("Submit", buttonFont, Brushes.Brown, submitButton);
                }

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (grid[i,j].State == StateType.Empty)
                        {
                            e.Graphics.FillRectangle(Brushes.Gray, grid[i, j].Box);
                        }
                        else if (grid[i,j].State == StateType.Filled)
                        {
                            e.Graphics.FillRectangle(Brushes.Salmon, grid[i, j].Box);
                            e.Graphics.DrawString(grid[i, j].Value.ToString(), numFont, Brushes.Black,
                                                  grid[i, j].Point);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.Blue, grid[i, j].Box);
                        }
                    }
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            isRunning = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i, j].State == StateType.Selected)
                    {
                        grid[i, j].State = StateType.Empty;
                    }
                    if (PointToClient(MousePosition).X > grid[i, j].Box.Left &&
                        PointToClient(MousePosition).X < grid[i, j].Box.Right &&
                        PointToClient(MousePosition).Y > grid[i, j].Box.Top &&
                        PointToClient(MousePosition).Y < grid[i, j].Box.Bottom)
                    {
                        grid[i, j].State = StateType.Selected;
                    }
                }
            }

            if (PointToClient(MousePosition).X > submitButton.Left &&
                PointToClient(MousePosition).X < submitButton.Right &&
                PointToClient(MousePosition).Y > submitButton.Top &&
                PointToClient(MousePosition).Y < submitButton.Bottom)
            {
                if (game.IsError(grid))
                {
                    MessageBox.Show("The solution is incorrect");
                }
            }
        }
        
        private void tmr_Tick(object sender, EventArgs e)
        {
            Refresh();
            MouseUpdate();
            Application.DoEvents();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i,j].State == StateType.Selected)
                    {
                        if (e.KeyCode == Keys.D1)
                        {
                            grid[i, j].Value = 1;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D2)
                        {
                            grid[i, j].Value = 2;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D3)
                        {
                            grid[i, j].Value = 3;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D4)
                        {
                            grid[i, j].Value = 4;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D5)
                        {
                            grid[i, j].Value = 5;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D6)
                        {
                            grid[i, j].Value = 6;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D7)
                        {
                            grid[i, j].Value = 7;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D8)
                        {
                            grid[i, j].Value = 8;
                            grid[i, j].State = StateType.Filled;
                        }
                        else if (e.KeyCode == Keys.D9)
                        {
                            grid[i, j].Value = 9;
                            grid[i, j].State = StateType.Filled;
                        }
                    }
                }
            }
        }

        void MouseUpdate()
        {
            if (PointToClient(MousePosition).X > submitButton.Left &&
                PointToClient(MousePosition).X < submitButton.Right &&
                PointToClient(MousePosition).Y > submitButton.Top &&
                PointToClient(MousePosition).Y < submitButton.Bottom)
            {
                submitHovered = true;
            }
            else
            {
                submitHovered = false;
            }
        }
    }
}
