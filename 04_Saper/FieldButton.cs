using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04_Saper
{
    class FieldButton: Button
    {
        private bool isBomb;
        public bool IsBomb
        {
            get => isBomb;
            set
            {
                isBomb = value;
                prepareText();
            }
        }

        private int bombAround;
        public int BombAround
        {
            get => bombAround;
            set
            {
                bombAround = value;
                prepareText();
            }
        }
        private bool isCovered = true;
        public bool IsCovered
        {
            get => isCovered;
            set
            {
                isCovered = value;
                prepareText();
            }
        }
        private Point position;
        public Point Position
        {
            get => position;
        }
        public FieldButton(Point position)
        {
            this.position = position;
            prepareText();
        }
        private void prepareText()
        {
            if (!isCovered)
            {
                if (isBomb)
                {
                    this.Text = "*";
                }
                else if (bombAround > 0)
                {
                    this.Text = bombAround.ToString();
                }
                else
                {
                    this.Text = " ";
                }
                BackColor = Color.White;
            }
            else
            {
                this.Text = " ";
                BackColor = Color.Gray;
            }
        }
    }
}
