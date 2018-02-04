using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Design;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering.Templates;
using SQLiteSugar;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using Log = StrayRabbit.MMS.Domain.Model.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.BusinessLog
{
    public partial class LogManage : DevExpress.XtraEditors.XtraForm
    {
        private int pageSize = 19;      //一页多少条
        private int pageIndex = 1;      //当前页数

        public LogManage()
        {
            InitializeComponent();

            panelControl1.BackColor = Color.FromArgb(65, 204, 212, 230);        //panel改成透明色
            panelControl1.BringToFront();        //将控件放到所有控件最前端
        }

        #region Load
        private void LogManage_Load(object sender, EventArgs e)
        {
            InitControlPosition();
            InitLookUpEdit(lue_LogType);
            InitData();
        }
        #endregion

        #region 数据加载

        private void InitData()
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    string sqlWhere = "1=1";

                    if (!string.IsNullOrWhiteSpace(date_begin.Text.Trim()))
                    {
                        sqlWhere += $" and datetime(Date) >= datetime('{DateTime.Parse(date_begin.Text.Trim()):yyyy-MM-dd}')";
                    }

                    if (!string.IsNullOrWhiteSpace(date_end.Text.Trim()))
                    {
                        sqlWhere += $" and datetime(Date) < datetime('{DateTime.Parse(date_end.Text.Trim()).AddDays(1):yyyy-MM-dd}')";
                    }

                    if (!string.IsNullOrWhiteSpace(lue_LogType.EditValue?.ToString()))
                    {
                        sqlWhere += $" and LogType='{lue_LogType.Text}'";
                    }

                    int count = 0;
                    var dataList = db.Queryable<Log>().Where(sqlWhere).OrderBy(t => t.LogId, OrderByType.Desc).ToPageList(pageIndex, pageSize, ref count);

                    if (dataList == null || !dataList.Any())
                    {
                        gd_list.DataSource = null;
                        gd_list.Refresh();
                        gd_list.RefreshDataSource();
                        return;
                    }

                    gd_list.DataSource = dataList;
                    gd_list.Refresh();
                    gd_list.RefreshDataSource();

                    this.gridView1.BestFitColumns(); //自动调整所有字段宽度
                    gridView1.IndicatorWidth = 30; //设置显示行号的列宽

                    //设置奇、偶行交替颜色
                    gridView1.OptionsView.EnableAppearanceEvenRow = true;
                    gridView1.OptionsView.EnableAppearanceOddRow = true;
                    gridView1.Appearance.EvenRow.BackColor = Color.GhostWhite;
                    gridView1.Appearance.OddRow.BackColor = Color.White;

                    //view行中值居左
                    this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    //view列标题居左
                    this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

                    if (pageIndex == 1)
                    {
                        InitPageCount(count);
                        lbl_Sum.Text = $"总数：{count}";
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("业务日志加载失败!" + ex.Message);
            }
        }
        #endregion

        #region GridView相关

        #region 递增列
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = ((e.RowHandle + 1) + ((pageIndex - 1) * pageSize)).ToString();
            }
        }
        #endregion

        #region 分页

        #region 下一页
        private void btn_MoveNext_Click(object sender, EventArgs e)
        {
            if (pageIndex < cmbPageNum.Properties.Items.Count)
            {
                pageIndex++;
                cmbPageNum.EditValue = pageIndex.ToString();
            }
            else
            {
                cmbPageNum.EditValue = cmbPageNum.Properties.Items.Count.ToString();
            }

            InitData();
        }
        #endregion

        #region 上一页
        private void btn_Preview_Click(object sender, EventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex--;
                cmbPageNum.EditValue = pageIndex;
            }
            else
            {
                cmbPageNum.SelectedIndex = 0;
            }

            InitData();
        }
        #endregion

        #region 末页
        private void btn_Last_Click(object sender, EventArgs e)
        {
            pageIndex = cmbPageNum.Properties.Items.Count;
            cmbPageNum.EditValue = cmbPageNum.Properties.Items.Count.ToString();

            InitData();
        }
        #endregion

        #region 首页
        private void btn_First_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            cmbPageNum.EditValue = 1;

            InitData();
        }
        #endregion

        #region 切换分页
        private void cmbPageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageIndex = Convert.ToInt32(cmbPageNum.EditValue);

            InitData();
        }
        #endregion

        #endregion

        #region 加载页数
        private void InitPageCount(int count)
        {
            if (cmbPageNum.Properties.Items.Count > 0)
                cmbPageNum.Properties.Items.Clear();

            if (count <= 0)
            {
                cmbPageNum.Properties.Items.Add(1);
            }
            else
            {
                int num = 1;
                for (int i = 0; i <= count; i += pageSize)
                {
                    cmbPageNum.Properties.Items.Add(num);
                    num++;
                }
            }

            cmbPageNum.SelectedIndex = 0;       //默认选择第一页
        }
        #endregion

        #region 格式化字段
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "Status") return;

            //状态
            if (e.Column.FieldName == "Status")
            {
                if (Convert.ToInt32(e.Value) == 0)
                {
                    e.DisplayText = "保存";
                }
                else if (Convert.ToInt32(e.Value) == 1)
                {
                    e.DisplayText = "已提交";
                }
                else
                {
                    e.DisplayText = "";
                }
            }
        }
        #endregion

        #endregion

        #region 初始化控件位置
        /// <summary>
        /// 初始化控件位置
        /// </summary>
        private void InitControlPosition()
        {
            groupBox2.Width = UserInfo.ChildWidth - 30;
            gd_list.Width = UserInfo.ChildWidth - 30;
            gd_list.Height = UserInfo.ChildHeight - 55;

            panelControl1.Width = UserInfo.ChildWidth - 40;
            panelControl1.Top = UserInfo.ChildHeight - 28;
            panelControl1.Left = 5;
        }
        #endregion

        #region 查询按钮
        private void btn_search_Click(object sender, EventArgs e)
        {
            InitData();
        }
        #endregion

        #region 加载下拉列表
        /// <summary>
        /// 加载下拉列表
        /// </summary> <param name="lue">下拉控件</param>
        private void InitLookUpEdit(LookUpEdit lue)
        {
            try
            {

                using (var db = SugarDao.GetInstance())
                {
                    int i = 0;
                    var list = new List<SelectItem>()
                    {
                      new SelectItem(){Key =i++.ToString(),Value = LogType.采购入库.ToString()},
                      new SelectItem(){Key = i++.ToString(),Value = LogType.入库明细.ToString()},
                      new SelectItem(){Key = i++.ToString(),Value = LogType.销售出库.ToString()},
                      new SelectItem(){Key =i++.ToString(),Value = LogType.出库明细.ToString()},
                      new SelectItem(){Key =i++.ToString(),Value = LogType.库存修改.ToString()},
                      new SelectItem(){Key = i++.ToString(),Value = LogType.药品信息.ToString()},
                      new SelectItem(){Key = i++.ToString(),Value = LogType.基础信息.ToString()},
                      new SelectItem(){Key =i++.ToString(),Value = LogType.其他.ToString()},
                    };


                    if (list.Count > 0)
                    {
                        var l = list.Select(t => new { t.Key, t.Value }).ToList();

                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "类型"));
                        lue.Properties.NullText = "";
                        lue.Properties.ImmediatePopup = true;      //当用户在输入框按任一可见字符键时立即弹出下拉窗体
                        lue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;//要使用户可以输入，这里须设为Standard
                        lue.Properties.SearchMode = SearchMode.OnlyInPopup;//自动过滤掉不需要显示的数据，可以根据需要变化
                        lue.Properties.AutoSearchColumnIndex = 1;

                        //lue.Properties.NullText = "请选择";
                        //lue.EditValue = "Id";
                        lue.Properties.DisplayMember = "Value";
                        lue.Properties.ShowHeader = true;
                        //lue.ItemIndex = 0;        //选择第一项
                        lue.Properties.DataSource = list;

                        //自适应宽度
                        lue.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                        //填充列
                        //lue.Properties.PopulateColumns();
                        //lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Character"));
                        //lue.Properties.Columns[0].Visible = false;
                    }
                    else
                    {
                        lue.Properties.NullText = "";
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("加载数据出错!" + ex.Message);
            }
        }
        #endregion
    }

    public class SelectItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}