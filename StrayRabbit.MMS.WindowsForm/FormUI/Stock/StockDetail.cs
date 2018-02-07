using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StrayRabbit.MMS.Common;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.StockManage
{
    public partial class StockDetail : DevExpress.XtraEditors.XtraForm
    {
        private readonly IStockService _stockService;
        public int _detailId;        //详情Id

        public StockDetail()
        {
            InitializeComponent();

            _stockService = new StockService();
        }

        #region Load
        private void StockDetail_Load(object sender, EventArgs e)
        {
            InitQX();


            InitData();
        }
        #endregion

        #region 加载数据

        private void InitData()
        {
            if (_detailId <= 0) return;

            try
            {
                var entity = _stockService.GetStockInfo(_detailId);
                if (entity != null && entity.Id > 0)
                {
                    txt_name.Text = entity.MedicineName;
                    txt_commonName.Text = entity.MedicineCommonName;
                    txt_ssjyfw.Text = entity.JyfwName;
                    txt_bzgg.Text = entity.YPGG;
                    txt_dw.Text = entity.YPDW;
                    txt_sccj.Text = entity.SCCJ;
                    txt_beginDate.Text = entity.BeginDate;
                    txt_endDate.Text = entity.EndDate;
                    txt_cost.Text = entity.Cost.ToString();
                    txt_Amount.Text = entity.Amount.ToString();
                    txt_batchNum.Text = entity.BatchNum;
                    txt_sale.Text = entity.Sale.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"数据加载失败!{ex.Message}");
            }
        }
        #endregion

        #region 保存按钮

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (_detailId < 0)
                {
                    XtraMessageBox.Show("库存Id不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_batchNum.Text.Trim()))
                {
                    XtraMessageBox.Show("批号不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Amount.Text.Trim()))
                {
                    XtraMessageBox.Show("数量不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (!txt_Amount.Text.Trim().IsNumber())
                    {
                        XtraMessageBox.Show("数量必须为数字!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                if (string.IsNullOrWhiteSpace(txt_cost.Text.Trim()))
                {
                    XtraMessageBox.Show("进价不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (!txt_cost.Text.Trim().IsNumber())
                    {
                        XtraMessageBox.Show("进价必须为数字!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                if (string.IsNullOrWhiteSpace(txt_sale.Text.Trim()))
                {
                    XtraMessageBox.Show("零售价不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (!txt_sale.Text.Trim().IsNumber())
                    {
                        XtraMessageBox.Show("零售价必须为数字!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                using (var db = SugarDao.GetInstance())
                {
                    var result = db.Update<Stock>(
                         $" BatchNum='{txt_batchNum.Text.Trim()}',Amount={txt_Amount.Text.Trim()},Cost={txt_cost.Text.Trim()},Sale={txt_sale.Text.Trim()},BeginDate='{(string.IsNullOrWhiteSpace(txt_beginDate.Text.Trim()) ? null : DateTime.Parse(txt_beginDate.Text.Trim()).ToString("yyyy-MM-dd"))}',EndDate='{(string.IsNullOrWhiteSpace(txt_endDate.Text.Trim()) ? null : DateTime.Parse(txt_endDate.Text.Trim()).ToString("yyyy-MM-dd"))}',UpdateUserId='{UserInfo.Account}',UpdateTime='{DateTime.Now:yyyy-MM-dd}'",
                         t => t.Id == _detailId);

                    if (result)
                    {
                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.库存修改.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = $"【修改成功】 库存id:{_detailId},药品名称:{txt_name.Text.Trim()},批号:{txt_batchNum.Text.Trim()},库存数量:{txt_Amount.Text.Trim()},进价:{txt_cost.Text.Trim()},零售价:{txt_sale.Text.Trim()},生产日期:{txt_beginDate.Text.Trim()},到期日期:{txt_endDate.Text.Trim()}"
                        });

                        DialogResult = DialogResult.OK;
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

        #region 关闭按钮
        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 关闭窗体
        private void StockDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 根据权限加载按钮

        private void InitQX()
        {
            if (UserInfo.RoleId != 1)
            {
                btn_save.Visible = false;
                btn_close.Left = btn_close.Left - 50;
            }
        }
        #endregion
    }
}