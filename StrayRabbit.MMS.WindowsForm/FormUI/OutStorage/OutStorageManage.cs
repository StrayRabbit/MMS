using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;

namespace StrayRabbit.MMS.WindowsForm.FormUI.OutStorage
{
    public partial class OutStorageManage : DevExpress.XtraEditors.XtraForm
    {
        private readonly IOrderService _orderService;
        private int pageSize = 15;      //一页多少条
        private int pageIndex = 1;      //当前页数

        public OutStorageManage()
        {
            InitializeComponent();

            _orderService = new OrderService();
            panelControl1.BackColor = Color.FromArgb(65, 204, 212, 230);        //panel改成透明色
            panelControl1.BringToFront();        //将控件放到所有控件最前端
        }

        #region Load
        private void OutStorageManage_Load(object sender, EventArgs e)
        {
            InitControlPosition();

            InitData();
        }
        #endregion

        #region 新增出库单
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var xf = new OutStorage();
            if (DialogResult.OK == xf.ShowDialog())
            {
                InitData();
            }
        }
        #endregion

        #region 查询
        private void btn_search_Click(object sender, EventArgs e)
        {
            InitData();
        }
        #endregion

        #region GridView相关
        #region 加载gridview
        private void InitData()
        {
            try
            {
                string sqlWhere = " and 1=1";
                int orderCount = 0;

                if (!string.IsNullOrWhiteSpace(txt_OrderNum.Text.Trim()))
                {
                    sqlWhere += $" and OrderNum like '%{txt_OrderNum.Text.Trim()}%'";
                }

                if (!string.IsNullOrWhiteSpace(txt_begin.Text.Trim()))
                {
                    sqlWhere += $" and datetime(createtime) >= datetime('{DateTime.Parse(txt_begin.Text.Trim()):yyyy-MM-dd}')";
                }

                if (!string.IsNullOrWhiteSpace(txt_end.Text.Trim()))
                {
                    sqlWhere += $" and datetime(createtime) < datetime('{DateTime.Parse(txt_end.Text.Trim()).AddDays(1):yyyy-MM-dd}')";
                }

                var dataList = _orderService.GetOrderListByType(2, sqlWhere, pageIndex, pageSize, out orderCount);

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
                    InitPageCount(orderCount);
                    lbl_Sum.Text = $"总数：{orderCount}";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("加载数据失败!" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

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

            ////时间
            //if (e.Column.FieldName == "MobileInTime")
            //{
            //    e.Column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            //}

            ////显示备注
            //if (e.Column.FieldName == "MobileRemarks")
            //{
            //    if (e.DisplayText.Length > 5)
            //    {
            //        e.DisplayText = e.DisplayText.Substring(0, 5) + "...";
            //    }
            //}
        }
        #endregion

        #region 双击GridView
        private void gd_list_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var frm = new OutStorage
                {
                    _detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                    _orderNum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderNum").ToString(),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    InitData();
                }
                else
                {
                    XtraMessageBox.Show("编辑失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("编辑出错!" + ex.Message);
            }
        }
        #endregion

        #region 右键编辑
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new OutStorage
                {
                    _detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                    _orderNum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderNum").ToString(),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    InitData();
                }
                else
                {
                    XtraMessageBox.Show("编辑失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("编辑出错!" + ex.Message);
            }
        }
        #endregion

        #region 右键删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"));

                if (Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Status")) != 0)
                {
                    XtraMessageBox.Show("已经提交的数据不能删除!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (id <= 0)
                {
                    XtraMessageBox.Show("请您选择删除的数据!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DialogResult.OK == XtraMessageBox.Show("您确定将该条记录删除吗？", "操作", MessageBoxButtons.OKCancel))
                {
                    using (var db = SugarDao.GetInstance())
                    {
                        if (db.Update<Domain.Model.Order>("Status=-1", t => t.Id == id))
                        {
                            InitData();

                            Log.Info(new LoggerInfo()
                            {
                                LogType = LogType.销售出库.ToString(),
                                CreateUserId = UserInfo.Account,
                                Message = $"【删除成功】 Id:{id}",
                            });
                        }
                        else
                        {
                            XtraMessageBox.Show("删除失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("删除异常!" + ex.Message);
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
    }
}