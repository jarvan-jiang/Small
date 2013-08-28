using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SudokuTest
{
    class SudokuTable
    {
        DataTable _dt = null;
        DataGridView dv = new DataGridView();
        List<int[]> lst = new List<int[]>();

        public SudokuTable()
        {
        }

        public DataGridView GetGrid(DataTable dt)
        {
            _dt = dt;
            InitDv();
            if (dt != null)
                InitValues();
            return dv;
        }
        public DataGridView GetGrid(DataTable dt, List<int[]> lstRed)
        {
            _dt = dt;
            if (lstRed != null)
                lst = lstRed;
            InitDv();
            if (dt != null)
                InitValues();
            return dv;
        }

        private void InitDv()
        {
            dv.Width = 300;
            dv.Height = 300;
            dv.ScrollBars = ScrollBars.None;
            dv.ColumnHeadersVisible = false;
            dv.RowHeadersVisible = false;
            dv.AllowUserToAddRows = false;
            dv.DataError += new DataGridViewDataErrorEventHandler(dv_DataError);
            dv.CellEndEdit += new DataGridViewCellEventHandler(dv_CellEndEdit);
            InitColumns();
            dv.Rows.Clear();
            InitRows();
        }

        void dv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(dv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1 || Convert.ToInt32(dv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) > 9)
            {
                MessageBox.Show("请输入1到9之间的数字！", "你丫玩过数独么");
                dv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            }
        }

        void dv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            e.ThrowException = false;
            MessageBox.Show("请写数字！", "你来捣乱的吧");
        }


        private void InitColumns()
        {
            for (int i = 1; i < 12; i++)
            {
                DataGridViewTextBoxColumn dc = new DataGridViewTextBoxColumn();
                dc.ValueType = typeof(Int32);
                dc.Name = i.ToString();
                dc.DisplayIndex = i;
                dc.Width = 30;
                dc.Resizable = DataGridViewTriState.False;
                dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dc.ReadOnly = false;
                if (i % 4 == 0)
                {
                    dc.DefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
                    dc.ReadOnly = true;
                    dc.Width = 15;
                }
                dv.Columns.Add(dc);
            }
        }
        private void InitRows()
        {
            for (int i = 1; i < 12; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.Height = 30;
                dr.Resizable = DataGridViewTriState.False;
                dr.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dr.ReadOnly = false;
                if (i % 4 == 0)
                {
                    dr.DefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
                    dr.ReadOnly = true;
                    dr.Height = 15;
                }
                dv.Rows.Add(dr);
            }
        }
        private void InitValues()
        {
            if (_dt != null)
            {
                int rowIndex = 0;
                for (int i = 1; i < 12; i++)
                {
                    if (i % 4 == 0)
                    {
                        continue;
                    }
                    int colIndex = 0;
                    for (int j = 1; j < 12; j++)
                    {
                        if (j % 4 == 0)
                        {
                            continue;
                        }
                        dv.Rows[i - 1].Cells[j - 1].Value = _dt.Rows[rowIndex][colIndex];
                        foreach (int[] item in lst)
                        {
                            if (item[0] == rowIndex && item[1] == colIndex)
                            {
                                dv.Rows[i - 1].Cells[j - 1].Style.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        colIndex++;
                    }
                    rowIndex++;
                }
            }
        }

    }
}
