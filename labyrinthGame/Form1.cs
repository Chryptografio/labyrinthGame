﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labyrinthGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //  initializing size of a labyrinth cell
        const int w = 4;
        int currentX = w;
        int currentY = w;
        
        SolidBrush wall = new SolidBrush(Color.Black);
        SolidBrush coridor = new SolidBrush(Color.WhiteSmoke);
        SolidBrush hero = new SolidBrush(Color.Tomato);
        SolidBrush path = new SolidBrush(Color.Plum);
        SolidBrush finish = new SolidBrush(Color.DarkBlue);
        int[,] labyrinthMap = new int[100, 100];

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            //creating a randon labyrinth 100 * 100
            
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    labyrinthMap[i, j] = 0;
                    if (i == 0 || i == 99 || j == 0 || j == 99) labyrinthMap[i, j] = 1;
                }
            }
            Random rnd = new Random();
            int rand = rnd.Next(0, 100);
            for (var i = 1; i < 99; i++)
            {
                for (var j = 1; j < 99; j++)
                {
                    rand = rnd.Next(0, 100);

                    if (labyrinthMap[i - 1, j] + labyrinthMap[i - 1, j - 1] + labyrinthMap[i - 1, j + 1] + labyrinthMap[i, j - 1] + labyrinthMap[i, j + 1] + labyrinthMap[i + 1, j - 1] + labyrinthMap[i + 1, j] + labyrinthMap[i + 1, j + 1] <= 3)
                    {
                        if (rand < 33) labyrinthMap[i, j] = 1;
                    }

                }
            }
            labyrinthMap[1, 1] = 0;
            labyrinthMap[98, 98] = 0;

            for (int i = 0; i < 100; i++)
            {
                for(int j = 0; j < 100; j++)
                {
                    if(labyrinthMap[i, j] == 1)
                    {
                        g.FillRectangle(wall, j * w, i * w, w, w);
                    } else
                    {
                        g.FillRectangle(coridor, j * w, i * w, w, w);
                    }
                }
            }
            g.FillRectangle(finish, 98 * w, 98 * w, w, w);
            currentX = w;
            currentY = w;
            g.FillRectangle(hero, currentX, currentY, w, w);
        }

        enum Directions
        {
            up = -1,
            down = 1,
            left = -1,
            right = 1
        }

        enum Axis
        {
            vertical = 0,
            horizontal = 1
        }

        void upd(Directions dir, Axis ax)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.FillRectangle(coridor, currentX, currentY, w, w);
            if (checker == 1) g.FillRectangle(path, currentX, currentY, w, w);
            if(ax == Axis.vertical)
            {
                currentY += (int)dir * w;
            } else
            {
                currentX += (int)dir * w;
            }
            g.FillRectangle(hero, currentX, currentY, w, w);
            if (currentX == 98 * w && currentY == 98 * w) label1.Text = "HERE YOU ARE";
        }

        void move(Directions dir, Axis ax)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            if(ax == Axis.vertical && labyrinthMap[currentY / w + (int)dir, currentX / w] == 0)
            {
                upd(dir, ax);
            }
            if (ax == Axis.horizontal && labyrinthMap[currentY / w, currentX / w + (int)dir] == 0)
            {
                upd(dir, ax);
            }

        }
        
        private void buttonLeft_Click(object sender, EventArgs e) => move(Directions.left, Axis.horizontal);

        private void buttonDown_Click(object sender, EventArgs e) => move(Directions.down, Axis.vertical);

        private void buttonRight_Click(object sender, EventArgs e) => move(Directions.right, Axis.horizontal);

        private void buttonUp_Click(object sender, EventArgs e) => move(Directions.up, Axis.vertical);

        int checker = 0;
        private void addPath_Click(object sender, EventArgs e)
        {
            checker = 1;
        }

        private void removePath_Click(object sender, EventArgs e)
        {
            checker = 0;
        }
    }
}
