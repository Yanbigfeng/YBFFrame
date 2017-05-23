using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace ClassHelp.MethodKeep
{
    class SqlBulkCopyUser
    {
        /// <summary>
        /// SqlBulkCopy大数据插入示例
        /// </summary>
        public void Run()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //创建内存表结构
            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("id");
            dtSource.Columns.Add("name");
            dtSource.Columns.Add("age");
            dtSource.Columns.Add("introduce");
            dtSource.Columns.Add("addTime");
            //向内存表结果添加数据
            DataRow dr;
            for (int i = 0; i < 10; i++)
            {
                // 创建与dt结构相同的DataRow对象  
                dr = dtSource.NewRow();
                dr["id"] = Guid.NewGuid().ToString("N");
                dr["name"] = "Name" + i;
                dr["age"] = i;
                dr["introduce"] = "Address" + i;
                dr["addTime"] = DateTime.Now;
                // 将dr追加到dt中  
                dtSource.Rows.Add(dr);
            }
            //插入操作
            string connStr = ConfigurationManager.ConnectionStrings["BluckCopy"].ToString();
            AddByBluckCopy(connStr, dtSource, "表名");
            Console.WriteLine("最终运行时间" + stopWatch.ElapsedMilliseconds + " ms.");
        }

        /// <summary>
        /// 插入或更新方法
        /// （该数据不存在执行插入，存在执行更新操作，存在与否的依据为主键是否存在）
        /// (调用该方法需要注意，DataTable中的字段名称必须和数据库中的字段名称一一对应)
        /// </summary>
        /// <param name="connectstring">数据连接字符串</param>
        /// <param name="table">内存表数据</param>
        /// <param name="tableName">目标数据的名称</param>
        public static void AddByBluckCopy(string connectstring, DataTable table, string tableName)
        {
            if (table != null && table.Rows.Count > 0)
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(connectstring))
                {
                    bulk.BatchSize = 1000;
                    bulk.BulkCopyTimeout = 100;
                    bulk.DestinationTableName = tableName;
                    bulk.WriteToServer(table);
                }
            }
        }
    }
}
