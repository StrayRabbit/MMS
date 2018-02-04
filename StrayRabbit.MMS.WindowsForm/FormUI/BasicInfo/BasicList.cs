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
using SQLiteSugar;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Common.ToolsHelper;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;

namespace StrayRabbit.MMS.WindowsForm.FormUI.BasicInfo
{
    public partial class BasicList : DevExpress.XtraEditors.XtraForm
    {
        private int pageSize = 15;      //一页多少条
        private int pageIndex = 1;      //当前页数

        List<BasicDictionary> dataList;      //全部数据

        public BasicList()
        {
            InitializeComponent();
        }

        #region 加载数据
        private void BasicInfo_Load(object sender, EventArgs e)
        {
            splitContainerControl1.Height = UserInfo.ChildHeight + 10;
            splitContainerControl1.Width = UserInfo.ChildWidth;
            tl_dict.Height = UserInfo.ChildHeight; gd_list.Height = UserInfo.ChildHeight - 65;
            lbl_Sum.Top = UserInfo.ChildHeight - 22;

            btn_First.Top = UserInfo.ChildHeight - 26;
            btn_Preview.Top = UserInfo.ChildHeight - 26;
            cmbPageNum.Top = UserInfo.ChildHeight - 25;
            btn_MoveNext.Top = UserInfo.ChildHeight - 26;
            btn_Last.Top = UserInfo.ChildHeight - 26;

            groupBox1.Width = UserInfo.ChildWidth - 250;
            gd_list.Width = UserInfo.ChildWidth - 250;
            //btn_search.Left= UserInfo.ChildWidth - 150;
            //btn_search.Top = btn_Add.Top;

            InitTree();
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTree()
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var list = db.Queryable<BasicDictionary>()
                        .Where(t => t.ParentId == 1)
                        .ToList();

                    if (list == null || !list.Any())
                    {
                        return;
                    }

                    //绑定树
                    List<TreeListModel> treeList = new List<TreeListModel>();

                    foreach (var item in list)
                    {
                        TreeListModel temp = new TreeListModel();
                        temp.ID = item.Id;
                        temp.MenuName = item.Name;
                        temp.ParentID = item.ParentId;
                        temp.Tag = item;

                        treeList.Add(temp);
                    }

                    tl_dict.KeyFieldName = "ID";
                    tl_dict.ParentFieldName = "ParentID";
                    tl_dict.DataSource = treeList;
                    tl_dict.ExpandAll();
                    tl_dict.RefreshDataSource();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 点击树
        private void tl_dict_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                int id = int.Parse(this.tl_dict.FocusedNode.GetValue("ID").ToString());

                gridView1.GroupPanelText = this.tl_dict.FocusedNode.GetValue("MenuName").ToString();

                using (var db = SugarDao.GetInstance())
                {
                    dataList = db.Queryable<BasicDictionary>().Where(t => t.ParentId == id).ToList();

                    if (!string.IsNullOrWhiteSpace(txt_search.Text.Trim()))
                    {
                        dataList =
                            dataList.Where(
                                t =>
                                    t.Name.Contains(txt_search.Text.Trim()) ||
                                    t.Character.Contains(txt_search.Text.Trim())).ToList();
                    }
                }
                InitGridView();
                //加载页数
                InitPageCount(dataList.Count);
                lbl_Sum.Text = string.Format("总数：{0}", dataList.Count);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region GridView相关

        #region 加载Grid

        private void InitGridView()
        {
            try
            {
                if (dataList.Count > 0)
                {
                    gd_list.DataSource = dataList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    ;
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
                else
                {
                    gd_list.DataSource = null;
                    gd_list.Refresh();
                    gd_list.RefreshDataSource();
                }
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

        #endregion

        #region 新增
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                int parentId = int.Parse(this.tl_dict.FocusedNode.GetValue("ID").ToString());
                string parentName = this.tl_dict.FocusedNode.GetValue("MenuName").ToString();

                if (parentId > 0)
                {
                    var frm = new BasicDetail
                    {
                        parentId = parentId,
                        parentName = parentName
                    };

                    if (DialogResult.OK == frm.ShowDialog())
                    {
                        tl_dict_FocusedNodeChanged(null, null);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 右键菜单（编辑）
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int parentId = int.Parse(this.tl_dict.FocusedNode.GetValue("ID").ToString());
                string parentName = this.tl_dict.FocusedNode.GetValue("MenuName").ToString();

                var frm = new BasicDetail
                {
                    parentId = parentId,
                    parentName = parentName,
                    id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                    name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Name").ToString()
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    tl_dict_FocusedNodeChanged(null, null);
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

        #region 右键菜单（删除）
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
                        if (db.Delete<BasicDictionary, int>(id))
                        {
                            tl_dict_FocusedNodeChanged(null, null);

                            string name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Name").ToString();
                            Common.log4net.Log.Info(new LoggerInfo()
                            {
                                LogType = LogType.基础信息.ToString(),
                                CreateUserId = UserInfo.Account,
                                Message = $"【{this.tl_dict.FocusedNode.GetValue("MenuName")} 删除成功】 Id:{id},名称:{name}",
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

        #region 查询
        private void btn_search_Click(object sender, EventArgs e)
        {
            tl_dict_FocusedNodeChanged(null, null);
        }
        #endregion

        #region 回车
        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车按钮
            if (e.KeyChar == 13)
            {
                tl_dict_FocusedNodeChanged(null, null);
            }
        }
        #endregion
    }
}