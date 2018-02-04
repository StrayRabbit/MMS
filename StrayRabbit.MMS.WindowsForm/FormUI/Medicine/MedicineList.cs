using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SQLiteSugar;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Medicine;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.ServiceImp;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.Medicine
{
    public partial class MedicineList : DevExpress.XtraEditors.XtraForm
    {
        private int pageSize = 15;      //一页多少条
        private int pageIndex = 1;      //当前页数

        List<MedicineListDto> dataList;      //全部数据

        public MedicineList()
        {
            InitializeComponent();
        }

        #region 新增药品详情
        private void btn_Add_Click(object sender, EventArgs e)
        {
            MedicineDetail xf = new MedicineDetail();
            if (DialogResult.OK == xf.ShowDialog())
            {
                MedicineList_Load(null, null);
            }
        }
        #endregion

        #region Load
        private void MedicineList_Load(object sender, EventArgs e)
        {
            try
            {
                InitControlPosition();

                var service = new MedicineService();
                dataList = service.GetMedicineList(txt_search.Text.Trim());

                InitData();

                //加载页数
                InitPageCount(dataList.Count);
                lbl_Sum.Text = $"总数：{dataList.Count}";
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GridView相关
        #region 加载gridview
        private void InitData()
        {
            try
            {
                if (dataList == null || !dataList.Any())
                {
                    gd_list.DataSource = null;
                    gd_list.Refresh();
                    gd_list.RefreshDataSource();
                    return;
                }

                gd_list.DataSource = dataList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
            }
            catch (Exception)
            {

                throw;
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
        #endregion

        #region 初始化控件位置
        /// <summary>
        /// 初始化控件位置
        /// </summary>
        private void InitControlPosition()
        {
            gd_list.Width = UserInfo.ChildWidth - 30;
            gd_list.Height = UserInfo.ChildHeight - 55;
            groupBox1.Width = UserInfo.ChildWidth - 30;

            lbl_Sum.Top = UserInfo.ChildHeight - 22;

            btn_First.Top = UserInfo.ChildHeight - 23;
            btn_Preview.Top = UserInfo.ChildHeight - 23;
            cmbPageNum.Top = UserInfo.ChildHeight - 22;
            btn_MoveNext.Top = UserInfo.ChildHeight - 23;
            btn_Last.Top = UserInfo.ChildHeight - 23;

            btn_First.Left = UserInfo.ChildWidth - 255;
            btn_Preview.Left = UserInfo.ChildWidth - 215;
            cmbPageNum.Left = UserInfo.ChildWidth - 175;
            btn_MoveNext.Left = UserInfo.ChildWidth - 115;
            btn_Last.Left = UserInfo.ChildWidth - 75;
        }
        #endregion

        #region 查询
        private void btn_search_Click(object sender, EventArgs e)
        {
            MedicineList_Load(null, null);
        }
        #endregion

        #region 右键删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"));

                if (id <= 0)
                {
                    XtraMessageBox.Show("请您选择删除的数据!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DialogResult.OK == XtraMessageBox.Show("您确定将该条记录删除吗？", "操作", MessageBoxButtons.OKCancel))
                {
                    using (var db = SugarDao.GetInstance())
                    {
                        if (db.Update<Domain.Model.Medicine>("Status=-1", t => t.Id == id))
                        {
                            string name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Name").ToString();

                            MedicineList_Load(null, null);

                            
                            Log.Info(new LoggerInfo()
                            {
                                LogType = LogType.药品信息.ToString(),
                                CreateUserId = UserInfo.Account,
                                Message = $"【删除成功】 Id:{id},名称:{name}",
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

        #region 右键编辑
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new MedicineDetail
                {
                    detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    MedicineList_Load(null, null);
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

        #region 双击
        private void gd_list_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var frm = new MedicineDetail
                {
                    detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    MedicineList_Load(null, null);
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

        #region 回车
        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车按钮
            if (e.KeyChar == 13)
            {
                MedicineList_Load(null, null);
            }
        }
        #endregion

        
    }
}