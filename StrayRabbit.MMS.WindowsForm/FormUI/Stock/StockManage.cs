using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SQLiteSugar;
using StrayRabbit.MMS.Common;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Stock;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;

namespace StrayRabbit.MMS.WindowsForm.FormUI.StockManage
{
    public partial class StockManage : DevExpress.XtraEditors.XtraForm
    {
        private readonly IStockService _stockService;
        private int pageSize = 15;      //一页多少条
        private int pageIndex = 1;      //当前页数
        List<StockListDto> dataList;      //全部数据

        public StockManage()
        {
            InitializeComponent();

            _stockService = new StockService();

            panelControl1.BackColor = Color.FromArgb(65, 204, 212, 230);        //panel改成透明色
        }

        #region Load
        private void StockManage_Load(object sender, EventArgs e)
        {
            try
            {
                int syts = (!string.IsNullOrWhiteSpace(txt_syts.Text.Trim()) && txt_syts.Text.Trim().IsNumber())
                    ? int.Parse(txt_syts.Text.Trim())
                    : 0;

                dataList = _stockService.GetStockList(txt_search.Text.Trim(), syts);

                InitGridView();
                //加载页数
                InitPageCount(dataList.Count);

                lbl_Sum.Text = $"总数：{dataList.Count}";

                InitControlPosition();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region GridView相关

        #region 加载gridview
        /// <summary>
        /// 加载明细列表
        /// </summary>
        private void InitGridView()
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

                gd_list.DataSource = dataList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); ;
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
            catch (Exception ex)
            {
                XtraMessageBox.Show("数据加载失败!" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 递增列
        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
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

            InitGridView();
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

            InitGridView();
        }
        #endregion

        #region 末页
        private void btn_Last_Click(object sender, EventArgs e)
        {
            pageIndex = cmbPageNum.Properties.Items.Count;
            cmbPageNum.EditValue = cmbPageNum.Properties.Items.Count.ToString();

            InitGridView();
        }
        #endregion

        #region 首页
        private void btn_First_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            cmbPageNum.EditValue = 1;

            InitGridView();
        }
        #endregion

        #region 切换分页
        private void cmbPageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageIndex = Convert.ToInt32(cmbPageNum.EditValue);

            InitGridView();
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

        #region 双击GridView
        private void gd_list_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var frm = new StockDetail
                {
                    _detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    StockManage_Load(null, null);
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

        #endregion

        #region 查询按钮
        private void btn_search_Click(object sender, EventArgs e)
        {
            StockManage_Load(null, null);
        }
        #endregion

        #region 回车
        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车按钮
            if (e.KeyChar == 13)
            {
                StockManage_Load(null, null);
            }
        }
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

        #region 加载下拉列表
        /// <summary>
        /// 加载下拉列表
        /// </summary> <param name="lue">下拉控件</param>
        /// <param name="parentId">根节点ID</param>
        private void InitLookUpEdit(LookUpEdit lue, int parentId)
        {
            try
            {
                if (parentId <= 0)
                    return;

                using (var db = SugarDao.GetInstance())
                {
                    var list_lue = db.Queryable<BasicDictionary>().Where(t => t.ParentId == parentId).ToList();       //品牌

                    if (list_lue.Count > 0)
                    {
                        BasicDictionary p = new BasicDictionary();
                        var l = list_lue.Select(m => new { m.Id, m.Name, m.Character }).ToList();

                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id"));
                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "名称"));
                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Character", "简写"));
                        lue.Properties.NullText = "";
                        lue.Properties.ImmediatePopup = true;      //当用户在输入框按任一可见字符键时立即弹出下拉窗体
                        lue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;//要使用户可以输入，这里须设为Standard
                        lue.Properties.SearchMode = SearchMode.OnlyInPopup;//自动过滤掉不需要显示的数据，可以根据需要变化
                        lue.Properties.AutoSearchColumnIndex = 2;

                        //lue.Properties.NullText = "请选择";
                        //lue.EditValue = "Id";
                        lue.Properties.ValueMember = "Id";
                        lue.Properties.DisplayMember = "Name";
                        lue.Properties.ShowHeader = true;
                        //lue.ItemIndex = 0;        //选择第一项
                        lue.Properties.DataSource = list_lue;

                        //自适应宽度
                        lue.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                        //填充列
                        //lue.Properties.PopulateColumns();
                        //lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Character"));
                        lue.Properties.Columns[0].Visible = false;
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
}