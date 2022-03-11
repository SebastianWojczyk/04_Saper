using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04_Saper
{
    public partial class Form1 : Form
    {
        private const int fieldSize = 25;
        private Random generator = new Random();

        private FieldButton[,] gameFields;
        private int FieldsToUncover;

        public Form1()
        {
            InitializeComponent();
            małaToolStripMenuItem_Click(null, null);
        }

        private void prepareNewGame(int gameWidth, int gameHeight, int bombCount)
        {
            //czyszczenie przy ponownym włączniu gry
            panelGame.Controls.Clear();

            gameFields = new FieldButton[gameWidth, gameHeight];
            FieldsToUncover = gameWidth * gameHeight - bombCount;
            for (int x = 0; x < gameWidth; x++)
            {
                for (int y = 0; y < gameHeight; y++)
                {
                    FieldButton b = new FieldButton(new Point(x, y));
                    gameFields[x, y] = b;

                    b.Click += FieldButton_Click;

                    b.Size = new Size(fieldSize, fieldSize);
                    b.Location = new Point(x * fieldSize, y * fieldSize);
                    panelGame.Controls.Add(b);
                }
            }

            do
            {
                int x = generator.Next(gameWidth);
                int y = generator.Next(gameHeight);

                if (!gameFields[x, y].IsBomb)
                {
                    gameFields[x, y].IsBomb = true;
                    bombCount--;

                    for (int xx = x - 1; xx <= x + 1; xx++)
                    {
                        for (int yy = y - 1; yy <= y + 1; yy++)
                        {
                            //test na krawędziach planszy
                            if (xx >= 0 && yy >= 0 && xx < gameWidth && yy < gameHeight)
                            {
                                gameFields[xx, yy].BombAround++;
                            }
                        }
                    }
                }
            }
            while (bombCount > 0);
        }

        private void FieldButton_Click(object sender, EventArgs e)
        {
            if (sender is FieldButton)
            {
                Unvover((sender as FieldButton).Position);
            }
        }

        private void Unvover(Point position)
        {
            FieldsToUncover--;
            gameFields[position.X, position.Y].IsCovered = false;
            if (gameFields[position.X, position.Y].IsBomb)
            {
                UncoverAll();
                MessageBox.Show("Przegrałeś!");
            }
            else if (gameFields[position.X, position.Y].BombAround == 0)
            {
                for (int xx = position.X - 1; xx <= position.X + 1; xx++)
                {
                    for (int yy = position.Y - 1; yy <= position.Y + 1; yy++)
                    {
                        //test na krawędziach planszy
                        if (xx >= 0 && yy >= 0 &&
                            xx < gameFields.GetLength(0) &&
                            yy < gameFields.GetLength(1))
                        {
                            if (gameFields[xx, yy].IsCovered)
                            {
                                Unvover(new Point(xx, yy));
                            }
                        }
                    }
                }
                return;
            }

            if(FieldsToUncover==0)
            {
                UncoverAll();
                MessageBox.Show("Wygrałeś!");
            }
        }

        private void UncoverAll()
        {
            foreach(FieldButton fb in panelGame.Controls)
            {
                fb.IsCovered = false;
                fb.Enabled = false;
            }
        }

        private void małaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prepareNewGame(8, 8, 2);
        }

        private void dużaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prepareNewGame(16, 16, 20);
        }
    }
}
