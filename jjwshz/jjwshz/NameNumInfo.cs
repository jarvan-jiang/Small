using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace jjwshz
{
    class NameNumInfo
    {
        public static string Path = "/NameNumInfo.txt";
        public static Dictionary<string, string> dictInfo = new Dictionary<string, string>();

        public static void ReadName()
        {
            using (StreamReader s = new StreamReader(Directory.GetCurrentDirectory() + "/NameNumInfo.txt", Encoding.GetEncoding("utf-8")))
            {
                string strLine = "";
                strLine = s.ReadLine();
                while (strLine != null)
                {
                    string[] info = strLine.Split(' ');
                    if (info.Length > 1)
                    {
                        string num = info[0];
                        string name = info[info.Length - 1];
                        if (!dictInfo.ContainsKey(num))
                        {
                            dictInfo.Add(num, name);
                        }
                    }
                    else
                    {
                        continue;
                    }
                    strLine = s.ReadLine();
                }
            }
        }

        public static System.Data.DataTable ReadNameToData(string filePath, int iMonth)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            int iyear = iMonth < 0 ? DateTime.Now.AddYears(-1).Year : DateTime.Now.Year;
            string strFilt = new DateTime(iyear, iMonth < 0 ? iMonth + 12 : iMonth, 1).ToString("yyyy-MM");
            using (StreamReader s = File.OpenText(filePath))
            {
                string strLine = "";
                strLine = s.ReadLine();
                foreach (string item in NameNumInfo.TrimEmpty(strLine))
                {
                    dt.Columns.Add(item);
                }
                dt.Columns["EnNo"].DataType = typeof(System.Int32);
                strLine = s.ReadLine();
                while (strLine != null)
                {
                    List<string> value = NameNumInfo.TrimEmpty(strLine);
                    value[value.Count - 2] += " " + value[value.Count - 1];
                    value.RemoveAt(value.Count - 1);
                    value[value.Count - 1] = Convert.ToDateTime(value[value.Count - 1]).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = "";
                    if (NameNumInfo.dictInfo.ContainsKey(value[2]))
                        name = NameNumInfo.dictInfo[value[2]];
                    else
                        name = "<<没有对应！>>";
                    value.Insert(3, name);
                    if (value[value.Count - 1].IndexOf(strFilt) >= 0)
                        dt.Rows.Add(value.ToArray());
                    strLine = s.ReadLine();
                }
                DataView dv = dt.DefaultView;
                dv.Sort = " EnNo ASC";
                dt = dv.ToTable();
            }
            return dt;
        }
        public static List<string> TrimEmpty(string strLine)
        {
            List<string> lst = new System.Collections.Generic.List<string>();
            string complete = "";
            string single = "";
            CharEnumerator c = strLine.GetEnumerator();
            while (c.MoveNext())
            {
                single = c.Current.ToString().ToString();
                if (single.Trim() != "")
                {
                    complete += single.Trim();
                }
                else if (complete != "")
                {
                    lst.Add(complete);
                    complete = "";
                }
            }
            if (complete != "")
            {
                lst.Add(complete);
                complete = "";
            }
            return lst;
        }
    }
    public class Item
    {
        private string _Text;
        private string _Value;

        public Item(string text, string value)
        {
            _Text = text;
            _Value = value;
        }

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public override string ToString()
        {
            return this._Text;
        }
    }
}
