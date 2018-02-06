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
using StrayRabbit.MMS.Common.ToolsHelper;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.Medicine
{
    public partial class MedicineDetail : DevExpress.XtraEditors.XtraForm
    {
        public int detailId;        //详情Id

        public MedicineDetail()
        {
            InitializeComponent();
        }

        #region Load
        private void MedicineDetail_Load(object sender, EventArgs e)
        {
            InitLookUpEdit(lue_ssjyfw, 2);      //所属经营范围
            InitLookUpEdit(lue_unit, 4);      //药品单位
            InitLookUpEdit(lue_type, 5);      //药剂分类
            InitLookUpEdit(lue_jgfl, 6);      //监管分类
            InitLookUpEdit(lue_gys, 7);      //供应商
            InitLookUpEdit(lue_sccj, 8);      //生产厂家

            if (detailId > 0)
            {
                using (var db = SugarDao.GetInstance())
                {
                    var entity = db.Queryable<Domain.Model.Medicine>().Single(t => t.Id == detailId);
                    if (entity != null && entity.Id > 0)
                    {
                        txt_name.Text = entity.Name;
                        txt_commonName.Text = entity.CommonName;
                        lue_ssjyfw.EditValue = entity.JYFWId;
                        txt_bzgg.Text = entity.BZGG;
                        lue_unit.EditValue = entity.UnitId;
                        lue_type.EditValue = entity.TypeId;
                        lue_jgfl.EditValue = entity.JGFLId;
                        lue_gys.EditValue = entity.SupplierId;
                        lue_sccj.EditValue = entity.SCCJId;
                        txt_cpzc.Text = entity.CPZC;
                        date_pzwh.Text = entity.PZWH;
                        ckb_isPrescription.Checked = entity.IsPrescription;
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

        #region 保存
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_name.Text.Trim()))
                {
                    XtraMessageBox.Show("请您输入药品名称!");
                    return;
                }

                var entity = new Domain.Model.Medicine()
                {
                    Id = detailId,
                    Name = txt_name.Text.Trim(),
                    NameCode = Pinyin.GetInitials(Pinyin.ConvertEncoding(txt_name.Text.Trim(), Encoding.UTF8, Encoding.GetEncoding("GB2312")), Encoding.GetEncoding("GB2312"))?.ToLower(),
                    CommonName = txt_commonName.Text.Trim(),
                    CommonNameCode = string.IsNullOrWhiteSpace(txt_commonName.Text.Trim()) ? "" : Pinyin.GetInitials(Pinyin.ConvertEncoding(txt_commonName.Text.Trim(), Encoding.UTF8, Encoding.GetEncoding("GB2312")), Encoding.GetEncoding("GB2312"))?.ToLower(),
                    JYFWId = lue_ssjyfw.EditValue == null ? 0 : int.Parse(lue_ssjyfw.EditValue.ToString()),
                    BZGG = txt_bzgg.Text.Trim(),
                    UnitId = lue_unit.EditValue == null ? 0 : int.Parse(lue_unit.EditValue.ToString()),
                    TypeId = lue_type.EditValue == null ? 0 : int.Parse(lue_type.EditValue.ToString()),
                    JGFLId = lue_jgfl.EditValue == null ? 0 : int.Parse(lue_jgfl.EditValue.ToString()),
                    SupplierId = lue_gys.EditValue == null ? 0 : int.Parse(lue_gys.EditValue.ToString()),
                    SCCJId = lue_sccj.EditValue == null ? 0 : int.Parse(lue_sccj.EditValue.ToString()),
                    CPZC = txt_cpzc.Text.Trim(),
                    PZWH = date_pzwh.Text.Trim(),
                    IsPrescription = ckb_isPrescription.Checked,
                    Status = 1,
                };

                using (var db = SugarDao.GetInstance())
                {
                    if (Convert.ToBoolean(db.InsertOrUpdate(entity)))
                    {
                        string msg = detailId > 0 ? $"【修改成功】 " : $"【新增成功】";

                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.药品信息.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = msg + $"药品Id:{entity.Id},药品名称:{entity.Name},药品简写:{entity.NameCode},药品通用名称:{entity.CommonName}" +
                                      $",药品通用名称简写:{entity.CommonNameCode},经营范围:{lue_ssjyfw.Text},药品规格:{txt_bzgg.Text},药品单位:{lue_unit.Text}" +
                                      $",药剂分类:{lue_type.Text},监管分类:{lue_jgfl.Text},供应商:{lue_gys.Text},生产厂家:{lue_sccj.Text},产品注册证批件号:{txt_cpzc.Text.Trim()}" +
                                      $",批准文号有效期:{ date_pzwh.Text.Trim()},是否处方药:{(entity.IsPrescription ? "是" : "否")}"

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

        #region 关闭
        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 窗体关闭
        private void MedicineDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}