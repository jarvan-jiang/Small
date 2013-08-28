using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int iSize = 16;
        const int iWindowSize = 30;
        private BaseBlock[,] BgArray = null;
        private SnakeClass snake = null;
        private List<SnakePoint> path = null;
        private BaseBlock food = null;
        private bool bLost = false;
        private int speed = 5;
        private System.Timers.Timer t = null;
        private int iCount = 0;

        private void Begin_Click(object sender, EventArgs e)
        {
            this.Clean();
            this.InitForm();
        }
        private void Clean()
        {
            BgArray = null;
            snake = null;
            path = null;
            bLost = false;
            t = null;
            iCount = 0;
        }
        private void InitForm()
        {
            BgArray = new BaseBlock[iWindowSize, iWindowSize];
            snake = new SnakeClass(6);
            snake.MaxPos = iWindowSize;
            List<Control> c = new List<Control>();
            for (int i = 0; i < iWindowSize; i++)
            {
                for (int j = 0; j < iWindowSize; j++)
                {
                    BaseBlock b = new BaseBlock();
                    b.Top = iSize * i + 25;
                    b.Left = iSize * j;
                    b.X = j;
                    b.Y = i;
                    b.Visible = true;
                    BgArray[j, i] = b;
                    c.Add(b);
                }
            }
            this.Controls.AddRange(c.ToArray());
            RefreshSnake();
            this.Width = 10 + iSize * iWindowSize;
            this.Height = 45 + iSize * iWindowSize + iSize;
            t = new System.Timers.Timer(3000 / speed);
            t.AutoReset = true;
            t.Elapsed += TimerEvent;
            t.SynchronizingObject = this;
            t.Start();
            lbName.Visible = true;
            lbSpeed.Visible = true;
            lbSpeed.Text = speed.ToString();
            this.Refresh();
        }
        private void TimerEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            if (!MoveAndEat())
            {
                Lost();
            }
            else
            {
                RefreshSnake();
            }
            iCount++;
            if (iCount == 5)
            {
                food = SetFood();
            }
            if (food == null && iCount > 5)
            {
                food = SetFood();
                if (iCount > 5 && snake.SnakeLength > 5 && snake.SnakeLength % 5 == 0)
                {
                    speed++;
                    t.Interval = 3000 / (speed * 2);
                    lbSpeed.Text = speed.ToString();
                }
            }
        }
        private BaseBlock SetFood()
        {
            Random r = new Random();
            int x = r.Next() % iWindowSize;
            int y = r.Next() % iWindowSize;
            while (BgArray[x, y].Active)
            {
                x = r.Next() % iWindowSize;
                y = r.Next() % iWindowSize;
            }

            BgArray[x, y].Active = true;
            BgArray[x, y].Refresh();
            return BgArray[x, y];
        }
        private void RefreshSnake()
        {
            BgArray[snake.SnakeTail.X, snake.SnakeTail.Y].Active = false;
            BgArray[snake.SnakeTail.X, snake.SnakeTail.Y].Refresh();
            path = snake.GetSnakePath();
            foreach (SnakePoint p in path)
            {
                BgArray[p.X, p.Y].Active = true;
                BgArray[p.X, p.Y].Refresh();
            }
        }
        protected override bool IsInputKey(Keys keyData)
        {
            bool result = base.IsInputKey(keyData) ||
              keyData == Keys.Left || keyData == Keys.Up ||
              keyData == Keys.Right || keyData == Keys.Down;
            return result;
        }
        private bool MoveAndEat()
        {
            SnakePoint p = snake.GetSnakeNextPoint();
            if (food != null && p.X == food.X && p.Y == food.Y)
            {
                snake.AddSnakeBody(p.X, p.Y);
                food = null;
                return true;
            }
            else
            {
                return snake.MoveSnake();
            }
        }
        private void Lost()
        {
            t.Stop();
            t.Close();
            t.Dispose();
            MessageBox.Show("你输了！");
            bLost = true;
            iCount = 0;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (bLost)
                return;
            Towards t = snake.SnakeDirect;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    {
                        t = Towards.Left;
                    }
                    break;
                case Keys.Right:
                    {
                        t = Towards.Right;
                    }
                    break;
                case Keys.Up:
                    {
                        t = Towards.Up;
                    }
                    break;
                case Keys.Down:
                    {
                        t = Towards.Down;
                    }
                    break;
                default:
                    return;
            }
            if (path != null)
            {
                foreach (SnakePoint p in path)
                {
                    if (BgArray[p.X, p.Y].Active)
                    {
                        BgArray[p.X, p.Y].Active = false;
                        BgArray[p.X, p.Y].Refresh();
                    }
                }
            }
            snake.ChangeSnakeDirect(t);
            if (!MoveAndEat())
            {
                Lost();
            }
            else
            {
                RefreshSnake();
            }
        }
    }
}
