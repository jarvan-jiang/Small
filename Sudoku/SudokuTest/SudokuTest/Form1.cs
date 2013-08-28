using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SudokuTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static DataTable dtRes = new DataTable();
        private static List<int[]> lstRed = new List<int[]>();

        private void start_Click(object sender, EventArgs e)
        {
            int iStart = Environment.TickCount;
            DataGridView dv1 = panel1.Controls[0] as DataGridView;
            DataTable dt = GetDataFromGrid(dv1);
            GetExistBlock(dt, lstRed);
            dtRes = GetSudokuSolution.GetSolution(dt);
            int iFin = Environment.TickCount - iStart;
            if (dtRes != null)
            {
                InitPanels(panel2, dtRes, lstRed);
            }
            else
            {
                MessageBox.Show("没有解！让你丫瞎填！");
            }
            MessageBox.Show("用时" + iFin + "毫秒");
        }

        private void DrawTables_Click(object sender, EventArgs e)
        {
            dtRes = new DataTable();
            lstRed = new List<int[]>();
            InitPanels(panel1, null, null);
            InitPanels(panel2, null, null);
        }

        private void InitPanels(Panel p, DataTable dt, List<int[]> lst)
        {
            p.Controls.Clear();
            SudokuTable st = new SudokuTable();
            p.Controls.Add(st.GetGrid(dt, lst));
        }
        private void GetExistBlock(DataTable dt, List<int[]> lst)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() != "0")
                        lst.Add(new int[] { i, j });
                }
            }
        }

        private DataTable GetDataFromGrid(DataGridView dv1)
        {
            DataTable dt = new DataTable();
            for (int j = 1; j < dv1.Columns.Count + 1; j++)
            {
                if (j % 4 == 0)
                {
                    continue;
                }
                dt.Columns.Add();
            }
            for (int i = 1, _i = -1; i < dv1.Rows.Count + 1; i++)
            {
                if (i % 4 == 0)
                {
                    continue;
                }
                dt.Rows.Add();
                _i++;
                for (int j = 1, _j = -1; j < dv1.Columns.Count + 1; j++)
                {
                    if (j % 4 == 0)
                    {
                        continue;
                    }
                    _j++;
                    dt.Rows[_i][_j] = Convert.ToInt32((dv1[j - 1, i - 1].Value == null || dv1[j - 1, i - 1].Value == DBNull.Value) ? 0 : dv1[j - 1, i - 1].Value);
                }
            }
            return dt;
        }
    }
}
