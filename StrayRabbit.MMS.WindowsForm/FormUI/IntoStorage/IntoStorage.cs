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
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.IntoStorage
{
    public partial class IntoStorage : DevExpress.XtraEditors.XtraForm
    {
        private readonly IOrderItemService _orderItemService;

        public int detailId;        //详情Id

        public IntoStorage()
        {
            InitializeComponent();

            _orderItemService = new OrderItemService();
        }

        #region Load
        private void IntoStorage_Load(object sender, EventArgs e)
        {
            try
            {
                InitLookUpEdit(lue_gys, 7);      //供应商

                InitData();

                InitGridView();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"数据加载异常!{ex.Message}", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 数据加载
        private void InitData()
        {
            if (detailId <= 0)
            {
                txt_OrderNum.Text = "RK" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                txt_UserName.Text = UserInfo.UserName;
                txt_CreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //if (lue_gys.Properties.DropDownRows == 1)
                //{
                //    lue_gys.ItemIndex = 0;
                //}
            }
            else
            {
                using (var db = SugarDao.GetInstance())
                {
                    var entity = db.Queryable<Domain.Model.Order>().Single(t => t.Id == detailId);
                    if (entity != null && entity.Id > 0)
                    {
                        txt_OrderNum.Text = entity.OrderNum;
                        lue_gys.EditValue = entity.SupplierId;
                        txt_UserName.Text = db.Queryable<Sys_User>().Single(t => t.Account == entity.CreateUserId).Name;
                        txt_CreateTime.Text = entity.CreateTime;
                        txt_Description.Text = entity.Description;

                        if (entity.Status != 0)
                        {
                            btn_save.Visible = false;
                            btn_submit.Visible = false;

                            btn_close.Left = btn_close.Left - 130;
                        }
                    }
                }
            }
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

        #region 新增明细
        private void hlk_Add_Click(object sender, EventArgs e)
        {
            if (lue_gys.EditValue == null)
            {
                XtraMessageBox.Show("请您选择供应商!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var xf = new IntoStorageDetail()
            {
                _supplierId = int.Parse(lue_gys.EditValue.ToString()),
                _orderNum = txt_OrderNum.Text.Trim(),
            };
            if (DialogResult.OK == xf.ShowDialog())
            {
                InitGridView();
            }
        }
        #endregion

        #region Gridview相关
        #region 加载gridview
        private void InitGridView()
        {
            try
            {
                var dataList = _orderItemService.GetOrderItemListByOrderNum(txt_OrderNum.Text.Trim());


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
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        #endregion

        #region 右键编辑
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new IntoStorageDetail
                {
                    _detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                    _supplierId = int.Parse(lue_gys.EditValue.ToString()),
                    _orderNum = txt_OrderNum.Text.Trim(),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    InitGridView();
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
                if (!btn_save.Visible)
                {
                    XtraMessageBox.Show("对不起已经提交的单据，不能删除!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"));

                if (id <= 0)
                {
                    XtraMessageBox.Show("请您选择删除的数据!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = SugarDao.GetInstance())
                {
                    if (db.Delete<Domain.Model.OrderItem>(t => t.Id == id))
                    {
                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.入库明细.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = $"【删除成功】 Id:{id},单号:{txt_OrderNum.Text.Trim()}",
                        });

                        InitGridView();
                    }
                    else
                    {
                        XtraMessageBox.Show("删除失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("删除异常!" + ex.Message);
            }
        }
        #endregion 

        #region 双击
        private void gd_list_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var frm = new IntoStorageDetail
                {
                    _detailId = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id")),
                    _supplierId = int.Parse(lue_gys.EditValue.ToString()),
                    _orderNum = txt_OrderNum.Text.Trim(),
                };
                if (DialogResult.OK == frm.ShowDialog())
                {
                    InitGridView();
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

        #region 保存
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                var entity = new Order()
                {
                    Id = detailId,
                    SupplierId = lue_gys.EditValue == null ? 0 : int.Parse(lue_gys.EditValue.ToString()),
                    Status = 0,
                    OrderNum = txt_OrderNum.Text.Trim(),
                    CreateTime = detailId <= 0 ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : txt_CreateTime.Text.Trim(),
                    CreateUserId = UserInfo.Account,
                    Description = txt_Description.Text.Trim(),
                    Type = 1,
                };

                using (var db = SugarDao.GetInstance())
                {
                    var result = db.InsertOrUpdate(entity);

                    if (Convert.ToBoolean(result))
                    {
                        DialogResult = DialogResult.OK;

                        var orderId = detailId > 0 ? detailId : int.Parse(result.ToString());
                        db.Update<OrderItem>(" OrderId= " + orderId, t => t.OrderNum == txt_OrderNum.Text.Trim() && t.OrderId <= 0);

                        string msg = detailId > 0 ? $"【修改成功】" : $"【保存成功】";
                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.采购入库.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = msg + $" 单号:{entity.OrderNum},供应商:{lue_gys.Text},描述:{entity.Description},创建日期:{entity.CreateTime},创建人:{entity.CreateUserId}",
                        });

                        this.Close();
                    }
                    else
                    {
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 窗体关闭
        private void IntoStorage_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 提交
        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("单据明细列表不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var db = SugarDao.GetInstance();

            try
            {
                var entity = new Order()
                {
                    Id = detailId,
                    SupplierId = lue_gys.EditValue == null ? 0 : int.Parse(lue_gys.EditValue.ToString()),
                    Status = 1,
                    OrderNum = txt_OrderNum.Text.Trim(),
                    CreateTime = detailId <= 0 ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : txt_CreateTime.Text.Trim(),
                    CreateUserId = UserInfo.Account,
                    Description = txt_Description.Text.Trim(),
                    Type = 1,
                };

                db.CommandTimeOut = 30000;//设置超时时间

                db.BeginTran();     //开启事务

                var result = db.InsertOrUpdate(entity);

                if (Convert.ToBoolean(result))
                {
                    //根据单号更新明细列表id
                    var orderId = detailId > 0 ? detailId : int.Parse(result.ToString());
                    db.Update<OrderItem>(" OrderId= " + orderId, t => t.OrderNum == txt_OrderNum.Text.Trim() && t.OrderId <= 0);

                    if (InsertStockByOrderNum(db))
                    {
                        db.CommitTran();

                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.采购入库.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = $"【提交成功】 Id:{orderId},单号:{entity.OrderNum},供应商:{lue_gys.Text},描述:{entity.Description},创建日期:{entity.CreateTime},创建人:{entity.CreateUserId}",
                        });
                    }
                    else
                    {
                        db.RollbackTran();

                        XtraMessageBox.Show("提交失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }


            }
            catch (Exception ex)
            {
                db.RollbackTran();
                XtraMessageBox.Show("提交失败!" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 添加库存
        /// <summary>
        /// 添加库存
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private bool InsertStockByOrderNum(SqlSugarClient db)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrWhiteSpace(txt_OrderNum.Text.Trim()))
                {
                    return result;
                }

                var orderItems = db.Queryable<OrderItem>().Where(t => t.OrderNum == txt_OrderNum.Text.Trim()).ToList();
                if (orderItems == null || !orderItems.Any())
                {
                    return result;
                }

                var stockCurr = new Stock();
                var gysId = int.Parse(lue_gys.EditValue.ToString());
                foreach (var orderItem in orderItems)
                {
                    stockCurr = db.Queryable<Stock>()
                        .SingleOrDefault(
                            t =>
                                t.MedicineId == orderItem.MedicineId && t.BatchNum == orderItem.BatchNum &&
                                t.Cost == orderItem.Cost);

                    //若存在相同药品，相同批号，相同进价，则修改库存数量和最新销售价格
                    if (stockCurr != null && stockCurr.Id > 0)
                    {
                        if (
                            Convert.ToBoolean(
                                db.Update<Stock>($" Amount=Amount+{orderItem.Amount},Sale={orderItem.Sale}",
                                    t => t.Id == stockCurr.Id)))
                        {
                            result = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    //新建一条库存记录
                    else
                    {
                        if (Convert.ToBoolean(db.Insert(new Stock()
                        {
                            Cost = orderItem.Cost,
                            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            CreateUserId = UserInfo.Account,
                            MedicineId = orderItem.MedicineId,
                            Sale = orderItem.Sale,
                            Amount = orderItem.Amount,
                            BatchNum = orderItem.BatchNum,
                            BeginDate = orderItem.BeginDate,
                            EndDate = orderItem.EndDate,
                        })))
                        {
                            result = true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    //插入库存日志
                    if (Convert.ToBoolean(db.Insert(new StockLog()
                    {
                        Amount = orderItem.Amount,
                        BatchNum = orderItem.BatchNum,
                        Cost = orderItem.Cost,
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        CreateUserId = UserInfo.Account,
                        MedicineId = orderItem.MedicineId,
                        Sale = orderItem.Sale,
                        Type = Common.StockLogType.入库.ToString(),
                    })))
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
        #endregion
    }
}