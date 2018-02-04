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
using StrayRabbit.MMS.Common;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Stock;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.OutStorage
{
    public partial class OutStorage : DevExpress.XtraEditors.XtraForm
    {
        private readonly IStockService _stockService;
        private readonly IOrderItemService _orderItemService;

        public int _detailId;        //详情Id
        public string _orderNum;        //单号

        public OutStorage()
        {
            InitializeComponent();

            _stockService = new StockService();
            _orderItemService = new OrderItemService();
        }


        #region Load
        private void OutStorage_Load(object sender, EventArgs e)
        {
            try
            {
                if (_detailId <= 0)
                {
                    _orderNum = "CK" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                }
                else
                {
                    InitGridViewList();
                    CalcHJ();

                    btn_add.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_Submit.Enabled = false;
                }

                groupBox2.Text = "销售订单--" + _orderNum;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 查询库存列表
        private void txt_search_KeyUp(object sender, KeyEventArgs e)
        {
            InitGridView();

            if (gridView2.RowCount > 0)
            {
                if (e.KeyValue == 38 || e.KeyValue == 40)
                {

                    gd_stock.Focus();
                }

                gridView2_FocusedRowChanged(null, null);
            }
        }
        #endregion

        #region 功能按钮
        #region 计算金额
        /// <summary>
        /// 计算金额
        /// </summary>
        private void CalcJE()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_dj.Text.Trim()) || string.IsNullOrWhiteSpace(txt_sl.Text.Trim()) ||
                        !txt_dj.Text.Trim().IsNumber() || !txt_sl.Text.Trim().IsNumber()) return;

                txt_je.Text = (decimal.Parse(txt_dj.Text.Trim()) * decimal.Parse(txt_sl.Text.Trim())).ToString();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 单价修改事件
        private void txt_dj_EditValueChanged(object sender, EventArgs e)
        {
            CalcJE();
        }
        #endregion

        #region 数量修改事件
        private void txt_sl_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_sl.Text.Trim()) || !txt_sl.Text.Trim().IsNumber())
                {
                    txt_sl.Text = string.Empty;
                    return;
                }

                int amount = int.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Amount").ToString());

                if (amount < int.Parse(((DevExpress.XtraEditors.BaseEdit)sender).EditValue.ToString()))
                {
                    txt_sl.Text = txt_sl.OldEditValue.ToString();
                    groupBox3.Focus();      //离开焦点后，文本框数量才会变化
                    txt_sl.Focus();
                    this.txt_sl.Select(this.txt_sl.Text.Length, 1);     //光标移到最后
                    CalcJE();
                    XtraMessageBox.Show("不能大于库存数量!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CalcJE();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 找零
        private void txt_sk_EditValueChanged(object sender, EventArgs e)
        {
            CalcHJ();
        }
        #endregion

        #region 添加明细
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount <= 0)
            {
                XtraMessageBox.Show("不能大于库存数量!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_dj.Text.Trim()))
            {
                XtraMessageBox.Show("单价不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!txt_dj.Text.Trim().IsNumber())
            {
                XtraMessageBox.Show("单价必须为数字!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_sl.Text.Trim()))
            {
                XtraMessageBox.Show("数量不能空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!txt_sl.Text.Trim().IsNumber())
            {
                XtraMessageBox.Show("单价必须为数字!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int stockId = int.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id").ToString());        //库存Id

            using (var db = SugarDao.GetInstance())
            {
                var entity = db.Queryable<Stock>().SingleOrDefault(t => t.Id == stockId);
                if (entity != null && entity.Id > 0)
                {
                    var currCount = int.Parse(txt_sl.Text.Trim());      //录入数量

                    if (entity.Amount < currCount)
                    {
                        XtraMessageBox.Show("当前数量不能大于库存数量!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var item = new OrderItem()
                    {
                        OrderNum = _orderNum,
                        BatchNum = entity.BatchNum,
                        Cost = entity.Cost,
                        Sale = decimal.Parse(txt_dj.Text.Trim()),
                        Amount = decimal.Parse(txt_sl.Text.Trim()),
                        MedicineId = entity.MedicineId,
                        BeginDate = entity.BeginDate,
                        EndDate = entity.EndDate,
                        StockId = stockId,
                    };

                    //检查是否已经添加
                    var IsExist = db.Queryable<OrderItem>()?.SingleOrDefault(t => t.OrderNum == _orderNum && t.MedicineId == item.MedicineId && t.BatchNum == item.BatchNum);
                    if (IsExist != null && IsExist.Id > 0)
                    {
                        DialogResult dr = XtraMessageBox.Show("该批次药品已经添加，您确认重复添加吗？", "操作提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information);
                        if (dr == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    if (!Convert.ToBoolean(db.InsertOrUpdate(item)))
                    {
                        XtraMessageBox.Show("添加失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        InitGridViewList();

                        CalcHJ();

                        var medicine =
                            db.Queryable<Domain.Model.Medicine>().SingleOrDefault(t => t.Id == entity.MedicineId);
                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.出库明细.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = $"【新增成功】单号:{item.OrderNum},库存Id:{entity.Id},药品名称:{medicine?.Name},批号:{entity.BatchNum},数量:{item.Amount},进价:{entity.Cost},零售价:{item.Sale}"
                        });
                    }
                }
                else
                {
                    XtraMessageBox.Show("未查到该库存!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }


        }
        #endregion

        #region 删除明细
        private void btn_delete_Click(object sender, EventArgs e)
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
                        if (db.Delete<OrderItem>(t => t.Id == id))
                        {
                            InitGridViewList();
                            CalcHJ();

                            var medicineName = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MedicineName").ToString();
                            Log.Info(new LoggerInfo()
                            {
                                LogType = LogType.出库明细.ToString(),
                                CreateUserId = UserInfo.Account,
                                Message = $"【删除成功】明细Id{id},单号:{_orderNum},药品名称:{medicineName}"
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

        #region 确定结账
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("明细列表不能空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            var db = SugarDao.GetInstance();
            try
            {
                db.CommandTimeOut = 30000;//设置超时时间
                db.BeginTran();     //开启事务


                var entity = new Order()
                {
                    Id = _detailId,
                    Status = 1,
                    OrderNum = _orderNum,
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateUserId = UserInfo.Account,
                    Type = 2,
                };

                var result = db.InsertOrUpdate(entity);

                if (Convert.ToBoolean(result))
                {
                    var orderId = _detailId > 0 ? _detailId : int.Parse(result.ToString());
                    db.Update<OrderItem>(" OrderId= " + orderId, t => t.OrderNum == _orderNum && t.OrderId <= 0);

                    var errorMsg = CalcStockByOrderNum(db);
                    if (string.IsNullOrWhiteSpace(errorMsg))
                    {
                        db.CommitTran();

                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.销售出库.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = $"【结账成功】单号:{_orderNum},单据Id:{orderId},描述:{entity.Description},合计:{txt_hj.Text.Trim()},创建日期:{entity.CreateTime},创建人:{entity.CreateUserId}",
                        });

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        db.RollbackTran();
                        XtraMessageBox.Show(errorMsg, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                db.RollbackTran();
                XtraMessageBox.Show("结账异常!" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ;
            }
        }
        #endregion

        #region 窗体关闭
        private void OutStorage_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
        #endregion

        #region GridView相关

        #region 加载gridview
        private void InitGridView()
        {
            try
            {
                var dataList = _stockService.GetStockList(txt_search.Text.Trim());


                if (dataList == null || !dataList.Any())
                {
                    gd_stock.DataSource = null;
                    gd_stock.Refresh();
                    gd_stock.RefreshDataSource();
                    return;
                }

                gd_stock.DataSource = dataList;
                gd_stock.Refresh();
                gd_stock.RefreshDataSource();

                this.gridView2.BestFitColumns(); //自动调整所有字段宽度
                gridView2.IndicatorWidth = 30; //设置显示行号的列宽

                //设置奇、偶行交替颜色
                gridView2.OptionsView.EnableAppearanceEvenRow = true;
                gridView2.OptionsView.EnableAppearanceOddRow = true;
                gridView2.Appearance.EvenRow.BackColor = Color.GhostWhite;
                gridView2.Appearance.OddRow.BackColor = Color.White;

                //view行中值居左
                this.gridView2.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                //view列标题居左
                this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 加载明细列表
        /// </summary>
        private void InitGridViewList()
        {
            try
            {
                var dataList = _orderItemService.GetOrderItemListByOrderNum(_orderNum);


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

        #region 切换行
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txt_dj.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Sale").ToString();
            txt_sl.Text = "1";

            CalcJE();
        }
        #endregion

        #endregion

        #region 自定义函数

        #region 计算合计
        /// <summary>
        /// 计算合计
        /// </summary>
        private void CalcHJ()
        {
            try
            {
                if (gridView1.RowCount <= 0)
                    txt_hj.Text = "0";


                decimal sum = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    sum += decimal.Parse(gridView1.GetRowCellValue(i, gridView1.Columns["gridColumn9"]).ToString());
                }

                txt_hj.Text = sum.ToString();

                if (string.IsNullOrWhiteSpace(txt_sk.Text.Trim()) || !txt_sk.Text.Trim().IsNumber()) return;
                txt_zl.Text = (decimal.Parse(txt_sk.Text.Trim()) - decimal.Parse(txt_hj.Text.Trim())).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 减去库存
        /// <summary>
        /// 减去库存
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private string CalcStockByOrderNum(SqlSugarClient db)
        {
            string result = "";

            try
            {
                if (string.IsNullOrWhiteSpace(_orderNum))
                {
                    return "单号不能为空!";
                }

                var orderItems = db.Queryable<OrderItem>().Where(t => t.OrderNum == _orderNum).ToList();
                if (orderItems == null || !orderItems.Any())
                {
                    return "未找到单据明细列表!";
                }

                var stockCurr = new Stock();
                foreach (var orderItem in orderItems)
                {
                    stockCurr = db.Queryable<Stock>()
                        .SingleOrDefault(
                            t =>
                                t.Id == orderItem.StockId);

                    if (stockCurr != null && stockCurr.Id > 0)
                    {
                        if (stockCurr.Amount < orderItem.Amount)
                        {
                            var medicineName =
                                db.Queryable<Domain.Model.Medicine>()
                                    .SingleOrDefault(t => t.Id == orderItem.MedicineId)?.Name;
                            return $"名称[{medicineName}]，批号[{orderItem.BatchNum}]的药品库存不足!";
                        }

                        if (
                            Convert.ToBoolean(
                                db.Update<Stock>($" Amount=Amount-{orderItem.Amount},TotalSales=TotalSales+{orderItem.Amount}", t => t.Id == orderItem.StockId)))
                        {
                            result = "";
                        }
                        else
                        {
                            return "修改库存失败!";
                        }
                    }
                    //新建一条库存记录
                    else
                    {
                        return "未找到该库存!";
                    }

                    //插入库存日志
                    if (Convert.ToBoolean(db.Insert(new StockLog()
                    {
                        Amount = orderItem.Amount * -1,
                        BatchNum = orderItem.BatchNum,
                        Cost = orderItem.Cost,
                        CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        CreateUserId = UserInfo.Account,
                        MedicineId = orderItem.MedicineId,
                        Sale = orderItem.Sale,
                        Type = Common.StockLogType.出库.ToString(),
                    })))
                    {
                        result = "";
                    }
                    else
                    {
                        return "插入库存日志失败!";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "异常!" + ex.Message;
            }

            return result;
        }

        #endregion

        #endregion


    }
}