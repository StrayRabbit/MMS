using System;
using SQLiteSugar;

namespace StrayRabbit.MMS.Domain
{
    public class SugarDao
    {
        private SugarDao()
        {

        }

        public static string ConnectionString
        {
            get
            {
                //string reval = "DataSource=" + System.AppDomain.CurrentDomain.BaseDirectory + "DataBase\\demo.sqlite"; ; //这里可以动态根据cookies或session实现多库切换
                //return reval;

                return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }

        public static SqlSugarClient GetInstance()
        {

            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableLogEvent = true;//启用日志事件
            db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
            return db;
        }
    }
}
