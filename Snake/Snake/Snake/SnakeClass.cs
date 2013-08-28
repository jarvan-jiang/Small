using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class SnakeClass
    {
        private int _maxPos = 30;

        public int MaxPos
        {
            get { return _maxPos; }
            set { _maxPos = value; }
        }

        private int iSnake = 3;

        public int SnakeLength
        {
            get { return iSnake; }
        }
        private Towards _direct = Towards.Right;

        public Towards SnakeDirect
        {
            get { return _direct; }
        }

        private List<SnakePoint> SnakeBody;

        private SnakePoint _snakeTail;

        public SnakePoint SnakeTail
        {
            get { return _snakeTail; }
        }
        private SnakePoint _snakeHead;
        public SnakePoint SnakeHead
        {
            get { return _snakeHead; }
        }

        public SnakeClass(int length)
        {
            iSnake = length;
            SnakeBody = new List<SnakePoint>();
            for (int i = 0; i < iSnake; i++)
            {
                SnakeBody.Add(new SnakePoint(i, 0));
            }
        }
        public bool MoveSnake()
        {
            SnakePoint p = GetSnakeNextPoint();
            if (SnakeBody.Contains(p))
                return false;
            SnakeBody.Add(p);
            _snakeTail = SnakeBody[0];
            _snakeHead = SnakeBody.Last();
            SnakeBody.RemoveAt(0);
            return true;
        }
        public void AddSnakeBody(int x, int y)
        {
            SnakePoint p = new SnakePoint(x, y);
            SnakeBody.Add(p);
            iSnake++;
        }
        public void ChangeSnakeDirect(Towards newDirect)
        {
            if (Math.Abs((int)newDirect - (int)_direct) != 2)
            {
                _direct = newDirect;
            }
        }
        public List<SnakePoint> GetSnakePath()
        {
            return SnakeBody;
        }
        public SnakePoint GetSnakeNextPoint()
        {
            SnakePoint p = SnakeBody[SnakeBody.Count - 1];
            switch (_direct)
            {
                case Towards.Left:
                    {
                        p.X = p.X - 1;
                        if (p.X < 0)
                        {
                            p.X = p.X + MaxPos;
                        }
                    }
                    break;
                case Towards.Right:
                    {
                        p.X = p.X + 1;
                        if (p.X >= MaxPos)
                        {
                            p.X = p.X - MaxPos;
                        }
                    }
                    break;
                case Towards.Up:
                    {
                        p.Y = p.Y - 1;
                        if (p.Y < 0)
                        {
                            p.Y = p.Y + MaxPos;
                        }
                    }
                    break;
                case Towards.Down:
                    {
                        p.Y = p.Y + 1;
                        if (p.Y >= MaxPos)
                        {
                            p.Y = p.Y - MaxPos;
                        }
                    }
                    break;
                default:
                    break;
            }
            return p;
        }
        
    }
    enum Towards
    {
        Left = 1, Down = 2, Right = 3, Up = 4
    }
    struct SnakePoint
    {
        int _X;

        public SnakePoint(int x, int y)
        {
            _X = x; _Y = y;
        }
        public int X
        {
            get { return _X; }
            set { _X = value; }
        }
        int _Y;

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
    }
}
