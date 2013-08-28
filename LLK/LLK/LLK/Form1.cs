using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LLK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        const int BlcSize = 30;
        int x; int y;
        BlockControl[] matrix = null;
        int[,] block = null;
        public void initial(int x, int y)
        {
            clean();
            this.x = x ;
            this.y = y ;
            matrix = new BlockControl[x * y];
            blockinitial(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {   
                    BlockControl blc = new BlockControl();
                    blc.Width = BlcSize;
                    blc.Height = BlcSize;
                    blc.Top = BlcSize * i+25;
                    blc.Left = BlcSize * j;
                    blc.Visible = true;
                    blc.value = block[i,j];
                    //blc.MouseStatusDown += mousedown;
                    //blc.MouseStatusUp += mouseup;
                    blc.MouseStaClick += mouseclick;
                    blc.Lx = i;
                    blc.Ly = j;
                    this.Controls.Add(blc);
                    matrix[i * y + j] = blc;
                }
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || j == 0 || i == x - 1 || j == y - 1)
                    {
                        block[i, j] = 0;
                        matrix[i * y + j].isClear = true;
                    }
                }
            }
            //this.Width = 255;
            //this.Height = 300;
            this.Refresh();
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.initial(8, 8);
        }
        private void clean()
        {
            if (matrix != null)
            {
                for (int i = 0; i < matrix.Length; i++)
                {
                        matrix[i].Dispose();
                }
            }
        }
        private void clearblock(BlockControl blc)
        {
            blc.Clear();
            block[blc.Lx, blc.Ly] = 0;
        }
        private void blockinitial(int bx, int by)
        {
            block = new int[bx, by];
            Random r = new Random();
            for (int i = bx-2; i >= 1; i--)
            {
                for (int j = by-2; j >= 1; j--)
                {
                    
                    if (block[i, j] == 0)
                    {
                        block[i, j] = r.Next(1,6);
                        int rx = r.Next(1,i);
                        int ry = r.Next(1,j);
                        while (block[rx, ry] != 0)
                        {
                            rx = r.Next(1,x-1);
                            ry = r.Next(1,y-1);
                        }
                        if (block[rx, ry] == 0)
                        {
                            block[rx, ry] = block[i, j];
                        }
                    }
                }
            }   
        }
        System.Collections.Generic.List<BlockControl> Hline = new System.Collections.Generic.List<BlockControl>();
        System.Collections.Generic.List<BlockControl> Vline = new System.Collections.Generic.List<BlockControl>();
        System.Collections.Generic.List<BlockControl> BtoL = new System.Collections.Generic.List<BlockControl>();
        System.Collections.Generic.List<BlockControl> LtoT = new System.Collections.Generic.List<BlockControl>();
        System.Collections.Generic.List<BlockControl> TtoR = new System.Collections.Generic.List<BlockControl>();
        System.Collections.Generic.List<BlockControl> RtoB = new System.Collections.Generic.List<BlockControl>();

        private bool FindPath(BlockControl blc1, BlockControl blc2)
        {
            if (FindPathDir(blc1, blc2))
                return true;
            else if (FindPathOneCorner(blc1,blc2))
                return true;
            else if (FindPathTwoCorner(blc1, blc2) || FindPathTwoCorner(blc2, blc1))
                return true;
            else
            {
                BtoL.Clear();
                LtoT.Clear();
                TtoR.Clear();
                RtoB.Clear();
                Hline.Clear();
                Vline.Clear();
                return false;
            }
        }

        private bool FindPathDir(BlockControl blc1, BlockControl blc2)
        { 
            if (blc1.Lx == blc2.Lx)
            {
                if (Math.Abs(blc1.Ly - blc2.Ly) == 1)
                    return true;
                else
                {
                    for (int i = Math.Min(blc1.Ly, blc2.Ly)+1 ; i < Math.Max(blc1.Ly, blc2.Ly); i++)
                    {
                        if (block[blc1.Lx, i] != 0)
                            return false;
                        else
                            Hline.Add(matrix[blc1.Lx * y + i]);
                    }
                    return true;
                }
            }
            else if (blc1.Ly == blc2.Ly)
            {
                if (Math.Abs(blc1.Lx - blc2.Lx) == 1)
                    return true;
                else
                {
                    for (int i = Math.Min(blc1.Lx, blc2.Lx) + 1; i < Math.Max(blc1.Lx, blc2.Lx); i++)
                    {
                        if (block[i, blc1.Ly] != 0)
                            return false;
                        else
                            Vline.Add(matrix[i * y + blc1.Ly]);
                    }
                    return true;
                }
            }
            else
            {
                BtoL.Clear();
                LtoT.Clear();
                TtoR.Clear();
                RtoB.Clear();
                Hline.Clear();
                Vline.Clear();
                return false;
            }
        }
        private bool FindPathOneCorner(BlockControl blc1, BlockControl blc2)
        {
            if (FindPath1(blc1, blc2))
                return true;
            else if (FindPath1(blc2, blc1))
                return true;
            else return false;
        }
        private bool FindPath1(BlockControl blc1, BlockControl blc2)
        {
          
            BlockControl blc3 = new BlockControl();
            blc3 = matrix[blc2.Lx * y + blc1.Ly];
            if (blc3.value != 0)
               return false;
            else if (FindPathDir(blc1, blc3) && FindPathDir(blc2, blc3))
            {
                if (blc1.Ly > blc2.Ly && blc1.Lx < blc2.Lx)
                {
                    LtoT.Add(blc3);
                }
                else if (blc1.Ly < blc2.Ly && blc1.Lx > blc2.Lx)
                {
                    RtoB.Add(blc3);
                }
                else if (blc1.Ly > blc2.Ly && blc1.Lx > blc2.Lx)
                {
                    BtoL.Add(blc3);
                }
                else if (blc1.Ly < blc2.Ly && blc1.Lx < blc2.Lx)
                {
                    TtoR.Add(blc3);
                }
                return true;
            }

            else
            {
                BtoL.Clear();
                LtoT.Clear();
                TtoR.Clear();
                RtoB.Clear();
                Hline.Clear();
                Vline.Clear();
                return false;
            }
        }

        private bool FindPathTwoCorner(BlockControl blc1, BlockControl blc2)
        {
            BlockControl blc3 = new BlockControl();
            for (int i = blc1.Lx + 1; i < x; i++)
            {
                blc3=matrix[i*y+blc1.Ly];
                if (blc3.value != 0)
                    break;
                else if (FindPathOneCorner(blc3, blc2) && FindPathDir(blc1, blc3))
                {
                    if (blc1.Ly < blc2.Ly)
                        TtoR.Add(blc3);
                    else 
                        LtoT.Add(blc3);
                    return true;
                }
            }
            for (int i = blc1.Lx - 1; i >= 0; i--)
            {
                blc3 = matrix[i * y + blc1.Ly];
                if (blc3.value != 0)
                    break;
                else if (FindPathOneCorner(blc3, blc2) && FindPathDir(blc1, blc3))
                {
                    if (blc1.Ly < blc2.Ly)
                        RtoB.Add(blc3);
                    else 
                        BtoL.Add(blc3);
                    return true;
                }
            }
            for (int i = blc1.Ly + 1; i < y; i++)
            {
                blc3 = matrix[blc1.Lx * y + i];
                if (blc3.value != 0)
                    break;
                else if (FindPathOneCorner(blc3, blc2) && FindPathDir(blc1, blc3))
                {
                    if (blc1.Lx < blc2.Lx)
                        BtoL.Add(blc3);
                    else
                        LtoT.Add(blc3);
                    return true;
                }
            }
            for (int i = blc1.Ly - 1; i >= 0; i--)
            {
                blc3 = matrix[blc1.Lx * y + i];
                if (blc3.value != 0)
                    break;
                else if (FindPathOneCorner(blc3, blc2) && FindPathDir(blc1, blc3))
                {
                    if (blc1.Lx < blc2.Lx)
                        RtoB.Add(blc3);
                    else
                        TtoR.Add(blc3);
                    return true;
                }
            }
            BtoL.Clear();
            LtoT.Clear();
            TtoR.Clear();
            RtoB.Clear();
            Hline.Clear();
            Vline.Clear();
            return false;
        }


        private void drawline(BlockControl blc1, BlockControl blc2)
        {
            foreach (BlockControl b in Vline)
            {
                b.value = 11;
                b.DrawLine();
            }
            foreach (BlockControl b in Hline)
            {
                b.value = 12;   
                b.DrawLine();
            }
            foreach (BlockControl b in BtoL)
            {
                b.value = 13;
                b.DrawLine();
            }
            foreach (BlockControl b in LtoT)
            {
                b.value = 14;
                b.DrawLine();
            }
            foreach (BlockControl b in TtoR)
            {
                b.value = 15;
                b.DrawLine();
            }
            foreach (BlockControl b in RtoB)
            {
                b.value = 16;
                b.DrawLine();
            }
            Thread.Sleep(500);
            foreach (BlockControl b in Vline)
            {
                b.value = 11;
                b.Clear();
            }
            foreach (BlockControl b in Hline)
            {
                b.value = 12;
                b.Clear();
            }
            foreach (BlockControl b in BtoL)
            {
                b.value = 13;
                b.Clear();
            }
            foreach (BlockControl b in LtoT)
            {
                b.value = 14;
                b.Clear();
            }
            foreach (BlockControl b in TtoR)
            {
                b.value = 15;
                b.Clear();
            }
            foreach (BlockControl b in RtoB)
            {
                b.value = 16;
                b.Clear();
            }
            BtoL.Clear();
            LtoT.Clear();
            TtoR.Clear();
            RtoB.Clear();
            Hline.Clear();
            Vline.Clear();
            clearblock(blc1);
            clearblock(blc2);
        }
        private void cleartwo(BlockControl blc)
        {
            BlockControl blc1 = new BlockControl();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (matrix[i * y + j].ClickT % 2 == 1 && matrix[i * y + j].isClear == false)
                    {
                        if (i != blc.Lx || j != blc.Ly)
                        {
                            blc1 = matrix[i * y + j];
                            if (block[blc.Lx, blc.Ly] == block[i, j])
                            {
                                if (FindPath(blc, blc1))
                                {
                                    drawline(blc, blc1);
                                }
                                else
                                {
                                    blc.unClick();
                                    blc1.unClick();
                                }
                            }
                            else
                            {
                                blc.unClick();
                                blc1.unClick();
                            }
                        }
                    }
                }
            }
        }
        private void mouseclick(object sender, EventArgs e)
        {
            BlockControl blc = (BlockControl)sender;
            if (blc.ClickT % 2 == 1)
            {
                blc._Click();
                cleartwo(blc);
            }
            else if (blc.ClickT % 2 == 0)
                blc.unClick();
        }
    }
}
