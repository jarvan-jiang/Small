using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string strConnect = DBAdapte.DBAdapter.GetConnectionString(DBAdapte.DBType.ORACLE, "ztjxdata", "rlst", "ztjx", "ztjxysh");
            //ztjxdata是本地服务名。rlst表空间，ztjx用户名，ztjxysh密码
            DBAdapte.DBAdapter db = DBAdapte.DBAdapter.CreateAdapter(DBAdapte.DBType.ORACLE, strConnect);
            using (db.Open())//连接数据库，成功
            {
                using (IDataReader dr=db.ExecuteReader("select * from rlst.bhqx"))//尝试查询保护缺陷，出错，提示找不到表或视图
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(dr[0].ToString());    
                    }
                }

                using (IDataReader dr = db.ExecuteReader("select table_name from all_tables"))//查询所有表，输出没有保护缺陷等表，只有系统自带的表
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(dr[0].ToString());
                    }
                }
            }
        }
    }
}
