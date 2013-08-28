using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;


namespace jjwshz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshList();
            InitList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            openFileDialog1.Title = "打开要转换的txt";
            openFileDialog1.ShowDialog();
            OpenToData(openFileDialog1.FileName);
        }
        private void RefreshList()
        {
            listBox1.Items.Clear();
            string FilePath = "";
            if (File.Exists(Directory.GetCurrentDirectory() + NameNumInfo.Path))
            {
                FilePath = Directory.GetCurrentDirectory() + NameNumInfo.Path;
            }
            else
            {
                DialogResult dr = MessageBox.Show("没找到NameNumInfo.txt,要不要指定一个名字信息文件?", "没找到", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
                    openFileDialog1.Title = "打开名字信息文件";
                    openFileDialog1.ShowDialog();
                    FilePath = openFileDialog1.FileName;
                }
            }
            if (FilePath != "")
                NameNumInfo.ReadName();
            foreach (KeyValuePair<string, string> item in NameNumInfo.dictInfo)
            {
                listBox1.Items.Add(new Item("        " + item.Key + (item.Key.Length > 1 ? "" : " ") + "               " + item.Value, item.Key));
            }
            listBox1.Refresh();
        }
        private void InitList()
        {
            int iMonth = DateTime.Now.Month;
            for (int i = iMonth - 2; i < iMonth + 3; i++)
            {
                if (i > 0)
                    comboBox1.Items.Add(i.ToString() + "月");
                else
                    comboBox1.Items.Add("去年" + (i + 12).ToString() + "月");
            }
            comboBox1.Items.Add("导出全部");
            comboBox1.SelectedIndex = 1;
        }

        private void OpenToData(string filePath)
        {
            int iMonth = DateTime.Now.Month;
            string strDate = "";
            if (comboBox1.SelectedIndex == 5)
            {
                strDate = "天津" + System.DateTime.Now.ToString("yyyy年") + "考勤";
                iMonth = -1;
            }
            else
            {
                iMonth = iMonth - 2 + comboBox1.SelectedIndex;
                strDate = "天津" + (iMonth < 0 ? (System.DateTime.Now.AddYears(-1).ToString("yyyy年") + (iMonth + 12).ToString()) : (System.DateTime.Now.ToString("yyyy年") + (iMonth).ToString())) + "月考勤";
            }
            try
            {
                DataToExcel("保存转换后的考勤表", strDate + "表", strDate, NameNumInfo.ReadNameToData(filePath, iMonth));
                MessageBox.Show("考勤表转换完毕，接着统计信息", "统计");
                DataToExcel("保存统计信息表", strDate + "信息", strDate + "统计", GatherDuty.GetDutyDataTable());
                DialogResult dr = MessageBox.Show("统计完毕~要打开吗?", "统计完毕", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("出现错误:" + e.Message + "在" + e.StackTrace + "，和你的亲爱的联系！", "出错了");
            }
        }
        private void DataToExcel(string title, string sheetName, string filename, System.Data.DataTable dt)
        {
            saveFileDialog1.Filter = "EXCEL文件(*.xls)|*.xls";
            saveFileDialog1.Title = title;
            saveFileDialog1.FileName = filename;
            saveFileDialog1.ShowDialog();
            MessageBox.Show("亲~转换要一些时间哦", "温馨提示");
            ApplicationClass ExcelApp = new ApplicationClass();
            Workbook wb = ExcelApp.Workbooks.Add();
            Worksheet st = ((Worksheet)wb.ActiveSheet);
            st.Name = sheetName;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                st.Cells[1, i + 1] = dt.Columns[i].ColumnName;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    st.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
                }
                if (dt.Columns.Contains("EnNo"))
                {
                    GatherDuty.GetDutyInfo(dt.Rows[i]);
                    st.Cells[i + 2, dt.Columns.Count + 1] = GatherDuty.GetRemark(dt.Rows[i]);
                }
            }
            Microsoft.Office.Interop.Excel.Range oRange = st.get_Range(st.Cells[1, 1],
                      st.Cells[dt.Rows.Count + 1, dt.Columns.Count]);
            oRange.EntireColumn.AutoFit();
            wb.SaveAs(saveFileDialog1.FileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value);
            wb.Close(Missing.Value);
            wb = null;
            ExcelApp.Quit();
            ExcelApp = null;
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.StartPosition = FormStartPosition.CenterParent;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.PeopleNum != "" && dlg.PeopleName != "")
                {
                    if (!NameNumInfo.dictInfo.ContainsKey(dlg.PeopleNum))
                    {
                        NameNumInfo.dictInfo.Add(dlg.PeopleNum, dlg.PeopleName);
                    }
                    else
                    {
                        NameNumInfo.dictInfo[dlg.PeopleNum] = dlg.PeopleName;
                    }
                }
                RefreshList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                string value = ((Item)listBox1.SelectedItem).Value;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                NameNumInfo.dictInfo.Remove(value);
                listBox1.Refresh();
            }
            else
            {
                MessageBox.Show("亲,选择一个先", "不要调皮");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string file = Directory.GetCurrentDirectory() + "/NameNumInfo.txt";
            using (StreamWriter sw = new StreamWriter(file, false))
            {
                foreach (KeyValuePair<string, string> item in NameNumInfo.dictInfo)
                {
                    sw.WriteLine(item.Key + " " + item.Value);
                }
            }
            MessageBox.Show("保存成功~", "嘿嘿");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> listItem = new System.Collections.Generic.List<KeyValuePair<string, string>>(NameNumInfo.dictInfo);
            switch (((System.Windows.Forms.Label)sender).Text)
            {
                case "编号":
                    listItem.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                    {
                        return (Convert.ToInt32(s1.Key)).CompareTo(Convert.ToInt32(s2.Key));
                    });
                    break;
                case "名字":
                    listItem.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                    {
                        return s1.Value.CompareTo(s2.Value);
                    });
                    break;
                default:
                    break;
            }
            NameNumInfo.dictInfo.Clear();
            foreach (KeyValuePair<string, string> item in listItem)
            {
                NameNumInfo.dictInfo.Add(item.Key, item.Value);
            }
            RefreshList();
        }
    }


}
