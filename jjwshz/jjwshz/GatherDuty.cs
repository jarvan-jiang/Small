using System;
using System.Collections.Generic;
using System.Text;

namespace jjwshz
{
    class GatherDuty
    {
        private static Dictionary<string, DutyInfo> dictDuty = new Dictionary<string, DutyInfo>();
        private static Dictionary<string, DateTime> dictTemp = new Dictionary<string, DateTime>();
        private static Dictionary<string, string> dictExp = new Dictionary<string, string>();
        private static Dictionary<KeyValuePair<string, DateTime>, DateTime> dictRmk = new Dictionary<KeyValuePair<string, DateTime>, DateTime>();
        private static int iLateHour = 8;
        private static int iLateMins = 30;
        private static int iLeaveHour = 18;
        private static int iLeaveMins = 0;

        private static int iOverHour = 8;
        private static int iStayHour = 20;
        private static int iStayMins = 30;

        private static int iLate = 0;
        private static int iStay = 0;



        public static System.Data.DataTable GetDutyDataTable()
        {
            System.Data.DataTable newdt = new System.Data.DataTable();
            newdt.Columns.Add("编号");
            newdt.Columns.Add("名字");
            newdt.Columns.Add("出勤天数");
            newdt.Columns.Add("迟到天数");
            newdt.Columns.Add("平时留岗");
            newdt.Columns.Add("周末加班");
            newdt.Columns.Add("备注");

            newdt.Columns["编号"].DataType = typeof(System.Int32);

            GetRemark();
            foreach (KeyValuePair<string, DutyInfo> item in dictDuty)
            {
                newdt.Rows.Add(new object[] { item.Key, item.Value.Name, item.Value.Duty, item.Value.Late, item.Value.Stay, item.Value.Overtime, item.Value.Remark });
            }
            newdt.DefaultView.Sort = " 编号 ASC";
            newdt = newdt.DefaultView.ToTable();
            return newdt;
        }
        public static void GetRemark()
        {
            foreach (KeyValuePair<string, string> item in dictExp)
            {
                dictDuty[item.Key].Remark = item.Value;
            }
        }
        public static void GetDutyInfo(System.Data.DataRow dr)
        {
            double iDuty = 0;
            iLate = 0;
            iStay = 0;
            double iOver = 0;
            string iKey = dr["EnNo"].ToString();
            DateTime dtDay = Convert.ToDateTime(dr["DateTime"].ToString());


            if (dtDay.DayOfWeek == DayOfWeek.Saturday || dtDay.DayOfWeek == DayOfWeek.Sunday)
            {
                iOver = GetOverTime(iKey, dtDay);
            }
            else
            {
                iDuty = GetDutyTime(iKey, dtDay);
            }

            if (dictDuty.ContainsKey(iKey))
            {
                dictDuty[iKey].Duty += iDuty;
                dictDuty[iKey].Late += iLate;
                dictDuty[iKey].Overtime += iOver;
                dictDuty[iKey].Stay += iStay;
            }
            else
            {
                dictDuty.Add(iKey, new DutyInfo(dr["Name"].ToString(), iDuty, iLate, iStay, iOver));
            }
        }

        public static string GetCurrDutyInfo(System.Data.DataRow dr)
        {
            string strRet = "";



            return strRet;
        }
        private static bool IsHoliDay(DateTime dt)
        {
            return false;
        }

        private static double GetDutyTime(string iKey, DateTime dtDay)
        {
            double iDuty = 0;
            DateTime dtLate = new DateTime(dtDay.Year, dtDay.Month, dtDay.Day, iLateHour, iLateMins, 0);
            DateTime dtStay = new DateTime(dtDay.Year, dtDay.Month, dtDay.Day, iStayHour, iStayMins, 0);
            if (dictTemp.ContainsKey(iKey))
            {
                if (dtDay.Day == dictTemp[iKey].Day)
                {
                    TimeSpan ts = (dtDay - dictTemp[iKey]);
                    if ((ts.Hours <= 3 && dtDay.Hour >= 14) || (ts.Hours <= 1))//连续打卡按照最后一个时间
                    {
                        dictTemp[iKey] = dtDay;
                    }
                    else
                    {
                        if (dtDay > dtLate && (dtDay - dtLate).Hours <= 1)//迟到
                        {
                            iLate++;
                        }
                        if (dtDay >= dtStay)//加班
                        {
                            iStay++;
                        }
                        if (ts.Hours >= 7.5)//正常
                        {
                            iDuty++;
                        }
                        if (ts.Hours >= 3 && ts.Hours < 7.5)//半天
                        {
                            iDuty += 0.5;
                        }
                        dictTemp.Remove(iKey);
                    }
                }
                else
                {
                    if (dictTemp[iKey].Hour <= 14)//漏按
                    {
                        if (!dictExp.ContainsKey(iKey))
                            dictExp.Add(iKey, dictTemp[iKey].ToString("dd") + "日下班漏打卡");
                        else
                            dictExp[iKey] += "；" + dictTemp[iKey].ToString("dd") + "日下班漏打卡";
                        iDuty++;
                    }
                    else
                    {
                        if (!dictExp.ContainsKey(iKey))
                            dictExp.Add(iKey, dictTemp[iKey].ToString("dd") + "日上班漏打卡");
                        else
                            dictExp[iKey] += "；" + dictTemp[iKey].ToString("dd") + "日上班漏打卡";
                        iDuty++;
                    }
                    if ((dtDay - dictTemp[iKey]).Days > 1)
                    {
                        DateTime dtTemp = dictTemp[iKey];
                        while ((dtDay - dtTemp).Days > 1)
                        {
                            dtTemp = dtTemp.AddDays(1);
                            if (dtTemp.DayOfWeek != DayOfWeek.Saturday && dtTemp.DayOfWeek != DayOfWeek.Sunday)
                            {
                                if (!dictExp.ContainsKey(iKey))
                                    dictExp.Add(iKey, dtTemp.ToString("dd日") + "未出勤");
                                else
                                    dictExp[iKey] += "；" + dtTemp.ToString("dd日") + "未出勤";
                            }
                        }
                    }
                    dictTemp[iKey] = dtDay;
                }
            }
            else
            {
                dictTemp.Add(iKey, dtDay);
            }
            return iDuty;
        }

        private static double GetOverTime(string iKey, DateTime dtDay)
        {
            double iOver = 0;
            if (dictTemp.ContainsKey(iKey))
            {
                if (dtDay.Day == dictTemp[iKey].Day)
                {
                    if ((dtDay - dictTemp[iKey]).Minutes < 10 && (dtDay - dictTemp[iKey]).Hours < 1)//误打卡
                    {
                        dictTemp[iKey] = dtDay;
                        return 0;
                    }
                    iOver += (dtDay - dictTemp[iKey]).Hours / iOverHour;
                    if (iOver > 1)
                    {
                        iOver = 1;
                    }
                    dictTemp.Remove(iKey);
                }
                else
                {
                    iOver++;
                    if (dictExp.ContainsKey(iKey))
                        dictExp[iKey] += "；" + dtDay.ToString("dd") + "日漏打卡";
                    else
                        dictExp.Add(iKey, dtDay.ToString("dd") + "日漏打卡");
                    dictTemp[iKey] = dtDay;
                }
            }
            else
            {
                dictTemp.Add(iKey, dtDay);
            }
            return iOver;
        }

        public static string GetRemark(System.Data.DataRow dr)
        {
            string rmk = "";


            DateTime dt = Convert.ToDateTime(dr["DateTime"].ToString());
            DateTime dtTemp = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            string strNum = dr["EnNo"].ToString();
            DateTime dtStay = new DateTime(dt.Year, dt.Month, dt.Day, iStayHour, iStayMins, 0);
            KeyValuePair<string, DateTime> kv = new KeyValuePair<string, DateTime>(strNum, dtTemp);

            if (dt >= dtStay.AddMinutes(-10))
            {
                rmk = "晚上留岗";
            }
            if (dictRmk.ContainsKey(kv))
            {

            }
            else
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    rmk = "周末加班";
                }
                dictRmk.Add(kv, dt);
            }

            return rmk;
        }
    }
    public class DutyInfo
    {
        private double _duty;
        private string _name;
        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DutyInfo(string name, double d, int l, int s, double o)
        {
            _name = name;
            _duty = d;
            _late = l;
            _stay = s;
            _overtime = o;
        }

        public double Duty
        {
            get { return _duty; }
            set { _duty = value; }
        }
        private int _late;

        public int Late
        {
            get { return _late; }
            set { _late = value; }
        }
        private int _stay;

        public int Stay
        {
            get { return _stay; }
            set { _stay = value; }
        }
        private double _overtime;

        public double Overtime
        {
            get { return _overtime; }
            set { _overtime = value; }
        }
    }
}
