using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using SQLiteSugar;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;

namespace StrayRabbit.MMS.WindowsForm.FormUI.Report
{
    public partial class ProfitLineChart : DevExpress.XtraEditors.XtraForm
    {
        public ProfitLineChart()
        {
            InitializeComponent();
        }

        #region Load
        private void ProfitLineChart_Load(object sender, EventArgs e)
        {
            InitControlPosition();
        }
        #endregion

        #region 初始化控件位置
        /// <summary>
        /// 初始化控件位置
        /// </summary>
        private void InitControlPosition()
        {
            chartControl1.Width = UserInfo.ChildWidth - 30;
            chartControl1.Height = UserInfo.ChildHeight - 55;

            panelControl1.Width = UserInfo.ChildWidth - 40;
        }
        #endregion

        #region 统计类型
        private void cb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cb_Type.EditValue.ToString()))
            {
                lbl_Date.Visible = true;
                txt_Date.Visible = true;
            }
            else
            {
                lbl_Date.Visible = false;
                txt_Date.Visible = false;
            }

            if (cb_Type.EditValue.ToString() == "年度统计")
            {
                lbl_Date.Text = "选择年份：";
                txt_Date.Properties.DisplayFormat.FormatString = "yyyy";

                if (txt_Date.EditValue != null && !string.IsNullOrEmpty(txt_Date.EditValue.ToString()))
                {
                    InitChart(GetData_Year(Convert.ToDateTime(txt_Date.EditValue)));
                }
            }
            else if (cb_Type.EditValue.ToString() == "月度统计")
            {
                lbl_Date.Text = "选择月份：";
                txt_Date.Properties.DisplayFormat.FormatString = "yyyy-MM";

                if (txt_Date.EditValue != null && !string.IsNullOrEmpty(txt_Date.EditValue.ToString()))
                {
                    InitChart(GetData_Month(Convert.ToDateTime(txt_Date.EditValue)));
                }
            }
        }
        #endregion

        #region 选择日期
        private void txt_Date_EditValueChanged(object sender, EventArgs e)
        {
            if (cb_Type.EditValue.ToString() == "年度统计")
            {
                InitChart(GetData_Year(Convert.ToDateTime(txt_Date.EditValue)));
            }
            else if (cb_Type.EditValue.ToString() == "月度统计")
            {
                InitChart(GetData_Month(Convert.ToDateTime(txt_Date.EditValue)));
            }
        }
        #endregion

        #region 加载线形图
        /// <summary>
        /// 加载线形图
        /// </summary>
        /// <param name="dt"></param>
        private void InitChart(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return;

            chartControl1.DataSource = dt;
            Series s1 = this.chartControl1.Series[0];//新建一个series类并给控件赋值  
            s1.ArgumentDataMember = "Date";        //绑定图表的横坐标
            s1.ValueDataMembers[0] = "Sum";        //绑定图表的纵坐标
            s1.LegendText = "利润";//设置图例文字 就是右上方的小框框   
        }
        #endregion

        #region 根据日期获取月度统计数据
        /// <summary>
        /// 根据月份统计
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DataTable GetData_Month(DateTime time)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Sum", typeof(decimal));       //个数
            dt.Columns.Add("Date", typeof(string));      //日期

            DataRow dr;
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var firstDay = Convert.ToDateTime(time.Year + "-" + time.Month + "-01").ToString("yyyy-MM-dd");
                    var lastDay = Convert.ToDateTime(time.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd 23:59");

                    var list = db.Queryable<StockLog>().Where($" Type=='出库' and CreateTime>='{firstDay}' and CreateTime<'{lastDay}'").ToList();



                    for (int i = 1; i <= DateTime.DaysInMonth(time.Year, time.Month); i++)
                    {
                        dr = dt.NewRow();

                        dr["Date"] = Convert.ToDateTime(time.Year + "-" + time.Month + "-" + i).ToString("yyyy-MM-dd");
                        dr["Sum"] = list.Where(p => DateTime.Parse(p.CreateTime) >= Convert.ToDateTime(dr["Date"].ToString()) && DateTime.Parse(p.CreateTime) < Convert.ToDateTime(dr["Date"].ToString() + " 23:59")).ToList().Sum(t => (t.Sale - t.Cost) * t.Amount * -1);
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("根据月份统计出错!");
            }


            return dt;
        }
        #endregion

        #region 根据日期获取年度统计数据
        /// <summary>
        /// 根据年份统计
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DataTable GetData_Year(DateTime time)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Sum", typeof(decimal));       //个数
            dt.Columns.Add("Date", typeof(string));      //日期

            DataRow dr;
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var firstMonth = time.Year + "-01-01";
                    var lastMonth = Convert.ToDateTime(time.Year + "-12-31").ToString("yyyy-MM-dd 23:59");

                    var list = db.Queryable<StockLog>().Where($" Type=='出库' and CreateTime>='{firstMonth}' and CreateTime<'{lastMonth}'").ToList();

                    for (int i = 1; i <= 12; i++)
                    {
                        dr = dt.NewRow();
                        var firstDay = Convert.ToDateTime(time.Year + "-" + i + "-01");
                        var lastDay = Convert.ToDateTime(Convert.ToDateTime(Convert.ToDateTime(time.Year + "-" + i + "-01").ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59"));

                        dr["Date"] = Convert.ToDateTime(time.Year + "-" + i).ToString("yyyy-MM");
                        dr["Sum"] = list.Where(p => DateTime.Parse(p.CreateTime) >= firstDay && DateTime.Parse(p.CreateTime) < lastDay).ToList().Sum(t => (t.Sale - t.Cost) * t.Amount * -1);
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("根据日期获取年度统计数据出错!");
            }


            return dt;
        }
        #endregion
    }
}