using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeping
{
    public partial class Form1 : Form
    {

        Form2 form2 = new Form2();

        public Form1()
        {
            InitializeComponent();
        }
        int height, width, mines;
        
        public Form1(int h, int w, int m)
        {
            height = h;
            width = w;
            mines = m;
        }
        
        private void start_Click(object sender, EventArgs e)
        {
            form2.Left= 50000;
            form2.ShowDialog();
            bool bg = form2.begin;
            if(bg)
                start_game();
        }
        public void start_game()
        {
            //InitializeComponent();
            ArrayList al = form2.al;
            height = int.Parse(al[0].ToString());
            width = int.Parse(al[1].ToString());
            mines = int.Parse(al[2].ToString());
            this.mineinitial(height, width, mines);
        }
        const int btnSize = 16;
        int x, y;
        MineControl[] matrix = null;
        int[,] mine = null;
        public void mineinitial(int x, int y, int mines)
        {
            clean();
            this.x = x;
            this.y = y;
            setmine(x, y, mines);
            matrix = new MineControl[x*y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    MineControl btn = new MineControl();
                    btn.Width = btnSize;
                    btn.Height = btnSize;
                    btn.Top = btnSize * i + 25;
                    btn.Left = btnSize * j;
                    btn.Visible = true;
                    btn.value = mine[i, j];
                    btn.MouseStatusDown += mousedown;
                    btn.MouseStatusUp += mouseup;
                    btn.Lx = i;
                    btn.Ly = j;
                    this.Controls.Add(btn);
                    matrix[i * y + j] = btn;
                }
            }
            this.Width = 10 + btnSize * y;
            this.Height = 45 + btnSize * x + btnSize;
            this.Refresh();
        }
        private int minecount(int i, int j, int mx, int my)
        {
            //MessageBox.Show(Convert.ToString(mx));
            int count = 0;
            for (int ri = i - 1; ri <= i + 1; ri++)
            {
                if (ri < 0 || ri >= mx) continue;
                for (int rj = j - 1; rj <= j + 1; rj++)
                {
                    if (rj < 0 || rj >= my) 
                        continue;
                    if (mine[ri, rj] == 9)
                        count++;
                }
            }
            //MessageBox.Show(Convert.ToString(count));
            return count;
        }
        private void setmine(int mx, int my, int minenum)
        {
            mine = new int[mx, my];
            Random r = new Random();
            for (int i = 0; i < minenum; i++)
            {
                int minex = r.Next() % x;
                int miney = r.Next() % y;
                mine[minex, miney] = 9;
            }
            for (int i = 0; i < mx; i++)
            {
                for (int j = 0; j < my; j++)
                {
                    if (mine[i, j] == 0)
                    {
                        mine[i, j] = minecount(i, j,mx, my);
                    }
                }
            }
        }
        private void fill(int i, int j)
        {
            if (i < 0 || i >= x || j < 0 || j >= y) return;
            if (mine[i, j] == 0)
            {

                mine[i, j] = -1;
                if (i - 1 >= 0)
                    fill(i - 1, j);
                if (j - 1 >= 0)
                    fill(i, j - 1);
                if (i + 1 < x)
                    fill(i + 1, j);
                if (j + 1 < y)
                    fill(i, j + 1);
                if (i - 1 >= 0 && j - 1 >= 0)
                    fill(i - 1, j - 1);
                if (i - 1 >= 0 && j + 1 < y)
                    fill(i - 1, j + 1);
                if (i + 1 < x && j - 1 >= 0)
                    fill(i + 1, j - 1);
                if (i + 1 < x && j + 1 < y)
                    fill(i + 1, j + 1);
                matrix[i * x + j].Open();
            }
            else if (mine[i, j] != 0 && mine[i, j] != 9 && mine[i, j] != -1)
            {
                mine[i, j] = -1;
                matrix[i * x + j].Open();
            }
        }
        private void clean()
        {
            if (matrix != null)
            {
                for (int i = 0; i < matrix.Length; i++)
                    matrix[i].Dispose();
            }
        }
        private void mousedown(object sender, EventArgs e)
        {
                MineControl btn = (MineControl)sender;
                switch (btn.mouseStatus)
                {
                    case 1:
                        btn.Press();
                        break;
                    case 2:
                        btn.Putflag();
                        if (checkwin())
                        {
                            MessageBox.Show("You Win!! O(∩_∩)O");
                            break;
                        }
                        break;
                    case 3:
                        for (int i = btn.Lx - 1; i <= btn.Lx + 1; i++)
                        {
                            if (btn.Lx < 0 || btn.Lx > x) continue;
                            for (int j = btn.Ly - 1; j <= btn.Ly + 1; j++)
                            {
                                if (btn.Ly < 0 || btn.Ly > y) continue;
                                {
                                    if (checkround(i,j))
                                        matrix[i * x + j].Press();
                                }
                            }
                        }
                        //btn.Press();
                        break;
                }
        }
        private void mouseup(object sender, EventArgs e)
        {
            MineControl btn = (MineControl)sender;
            switch (btn.mouseStatus)
            {
                case 4:
                    btn.Open();
                    if (mine[btn.Lx, btn.Ly] == 9 && !matrix[btn.Lx*x+btn.Ly].IsFlag)
                    {
                        MessageBox.Show("You Lose!! %>_<%");
                        foreach( MineControl m in matrix)
                        {
                            if (mine[m.Lx, m.Ly] == 9)
                                m.Open(); 
                           
                        }
                        break;
                    }
                    else if (checkwin())
                    {
                        MessageBox.Show("You Win!! O(∩_∩)O"); 
                        break;
                    }
                    if (btn.value == 0)
                        fill(btn.Lx, btn.Ly);
                    break;
                case 5:
                    break;
                case 6:
                    bool f = false;
                    if (openfield(btn.Lx, btn.Ly))
                    {
                        for (int i = btn.Lx - 1; i <= btn.Lx + 1; i++)
                        {
                            
                            if (f) break;
                            if (btn.Lx < 0 || btn.Lx > x) continue;
                            for (int j = btn.Ly - 1; j <= btn.Ly + 1; j++)
                            {

                                if (btn.Ly < 0 || btn.Ly > y) continue;
                                {
                                    if (checkround(i, j))
                                    {
                                        if (mine[i, j] == 9 && !matrix[i * x + j].IsFlag)
                                        {
                                            matrix[i * x + j].Open();
                                            MessageBox.Show("You Lose!! %>_<%");
                                            foreach (MineControl m in matrix)
                                            {
                                                if (mine[m.Lx, m.Ly] == 9)
                                                    m.Open();

                                            }
                                            f = true;
                                        }
                                        else
                                        {
                                            matrix[i * x + j].Open();
                                            fill(i, j);
                                            if (checkwin())
                                            {
                                                MessageBox.Show("You Win!! O(∩_∩)O");
                                                f = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    else
                    {
                        for (int i = btn.Lx - 1; i <= btn.Lx + 1; i++)
                        {
                            if (btn.Lx < 0 || btn.Lx > x) continue;
                            for (int j = btn.Ly - 1; j <= btn.Ly + 1; j++)
                            {
                                if (btn.Ly < 0 || btn.Ly > y) continue;
                                {
                                    if (checkround(i, j))
                                        matrix[i * x + j].UnPress();
                                }
                            }
                        }
                    }
                    break;
            }
        }
        private bool openfield(int i, int j)
        {
            if (!matrix[i * x + j].IsOpen) return false;
            //else return true;
            int checkf = 0;
            for (int ri = i - 1; ri <= i + 1; ri++)
            {
                if (ri < 0 || ri > x) continue;
                for (int rj = j - 1; rj <= j + 1; rj++)
                {
                    if (rj < 0 || rj > y)
                        continue;
                    if (checkround(ri, rj))
                    {
                        if (matrix[ri * x + rj].IsFlag)
                            checkf++;
                    }
                }
            }
            //MessageBox.Show(Convert.ToString(checkf));
            if (checkf == matrix[i * x + j].value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool checkwin()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (matrix[i * x + j].IsFlag)
                    {
                        if(mine[i,j]!=9)
                            return false;
                    }
                    else if (!matrix[i * x + j].IsOpen)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool checkround(int i,int j)
        {
            if (i - 1 >= -1 && i + 1 <= x && j - 1 >= -1  && j + 1 <= y)
                return true;
            else
                return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
