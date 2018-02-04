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
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Medicine;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.IntoStorage
{
    public partial class IntoStorageDetail : DevExpress.XtraEditors.XtraForm
    {
        private readonly IMedicineService _medicineService;
        public string _orderNum;     //单号
        public int _detailId;        //详情Id
        public int _supplierId;     //供应商id

        public IntoStorageDetail()
        {
            InitializeComponent();
            _medicineService = new MedicineService();
        }

        #region Load
        private void IntoStorageDetail_Load(object sender, EventArgs e)
        {
            try
            {
                InitLookUpEdit(lue_name);

                using (var db = SugarDao.GetInstance())
                {
                    if (_detailId > 0)
                    {
                        var entity = db.Queryable<Domain.Model.OrderItem>().Single(t => t.Id == _detailId);
                        if (entity != null && entity.Id > 0)
                        {
                            var medicineInfo = _medicineService.GetMedicineInfoById(entity.MedicineId);

                            lue_name.EditValue = entity.MedicineId;
                            txt_commonName.Text = medicineInfo.CommonName;
                            txt_ssjyfw.Text = medicineInfo.JyfwName;
                            txt_bzgg.Text = medicineInfo.BzggName;
                            txt_dw.Text = medicineInfo.UnitName;
                            txt_sccj.Text = medicineInfo.GysName;
                            txt_beginDate.Text = entity.BeginDate;
                            txt_endDate.Text = entity.EndDate;
                            txt_cost.Text = entity.Cost.ToString();
                            txt_Amount.Text = entity.Amount.ToString();
                            txt_batchNum.Text = entity.BatchNum;
                            txt_sale.Text = entity.Sale.ToString();
                        }
                    }

                    var order = db.Queryable<Order>()?.SingleOrDefault(t => t.OrderNum == _orderNum);

                    if (order != null && order.Status != 0)
                    {
                        btn_save.Visible = false;

                        btn_close.Left = btn_close.Left - 50;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"数据加载异常!{ex.Message}", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    var list_lue = db.Queryable<Domain.Model.Medicine>().Where(t => t.Status == 1 && t.SupplierId == _supplierId).ToList();       //品牌

                    if (list_lue.Count > 0)
                    {
                        Domain.Model.Medicine p = new Domain.Model.Medicine();
                        var l = list_lue.Select(m => new { m.Id, m.Name, m.NameCode }).ToList();

                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id"));
                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "名称"));
                        lue.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NameCode", "简写"));
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

        #region 选择药品触发事件
        private void lue_name_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(lue_name.EditValue.ToString());

                if (id > 0)
                {
                    var entity = _medicineService.GetMedicineInfoById(id);
                    if (entity != null && entity.Id > 0)
                    {
                        txt_commonName.Text = entity.CommonName;
                        txt_ssjyfw.Text = entity.JyfwName;
                        txt_bzgg.Text = entity.BzggName;
                        txt_dw.Text = entity.UnitName;
                        txt_sccj.Text = entity.GysName;
                    }

                    txt_batchNum.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 保存
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_orderNum))
                {
                    XtraMessageBox.Show("单号不能为空!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var entity = new OrderItem()
                {
                    Id = _detailId,
                    MedicineId = int.Parse(lue_name.EditValue.ToString()),
                    OrderNum = _orderNum,
                    Amount = decimal.Parse(txt_Amount.Text.Trim()),
                    Cost = decimal.Parse(txt_cost.Text.Trim()),
                    Sale = decimal.Parse(txt_sale.Text.Trim()),
                    BatchNum = txt_batchNum.Text.Trim(),
                    BeginDate = string.IsNullOrWhiteSpace(txt_beginDate.Text.Trim()) ? null : DateTime.Parse(txt_beginDate.Text.Trim()).ToString("yyyy-MM-dd"),
                    EndDate = string.IsNullOrWhiteSpace(txt_endDate.Text.Trim()) ? null : DateTime.Parse(txt_endDate.Text.Trim()).ToString("yyyy-MM-dd"),
                };

                using (var db = SugarDao.GetInstance())
                {
                    var result = db.InsertOrUpdate(entity);

                    if (Convert.ToBoolean(result))
                    {
                        string msg = _detailId > 0 ? $"【修改成功】 " : $"【新增成功】";

                        var medicine =
                            db.Queryable<Domain.Model.Medicine>().SingleOrDefault(t => t.Id == entity.MedicineId);
                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.入库明细.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = msg + $"单号:{entity.OrderNum},Id:{entity.Id},药品名称:{medicine?.Name},数量:{entity.Amount},进价:{entity.Cost},零售价:{entity.Sale},批号:{entity.BatchNum},生产日期:{entity.BeginDate},到期日期:{entity.EndDate}"
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

        #region 自动计算到期日期
        private void txt_yxq_EditValueChanged(object sender, EventArgs e)
        {
            if (!txt_yxq.Text.Trim().IsNumber())
            {
                XtraMessageBox.Show("有效期必须为数字!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txt_beginDate.Text.Trim()))
            {
                var beginDate = DateTime.Parse(txt_beginDate.Text.Trim());

                txt_endDate.Text = beginDate.AddMonths(int.Parse(txt_yxq.Text.Trim())).ToString("yyyy/MM/dd");
            }
        }
        #endregion

        #region 关闭
        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 关闭窗体
        private void IntoStorageDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}