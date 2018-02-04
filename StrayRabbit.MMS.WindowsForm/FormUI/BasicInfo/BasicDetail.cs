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
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm.FormUI.BasicInfo
{
    public partial class BasicDetail : DevExpress.XtraEditors.XtraForm
    {
        public int parentId;        //父级Id
        public string parentName;       //父级名称
        public int id;      //详情Id
        public string name;     //详情名称

        public BasicDetail()
        {
            InitializeComponent();
        }

        #region 保存
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (parentId <= 0)
                {
                    XtraMessageBox.Show("请您先选择树形菜单!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                using (var db = SugarDao.GetInstance())
                {
                    if (string.IsNullOrWhiteSpace(txt_name.Text.Trim()))
                    {
                        XtraMessageBox.Show("请您输入名称!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var model = new BasicDictionary()
                    {
                        Id = id,
                        ParentId = parentId,
                        Name = txt_name.Text.Trim(),
                        Character = txt_character.Text.Trim()
                    };

                    if (Convert.ToBoolean(db.InsertOrUpdate(model)))
                    {
                        string msg = id > 0 ? $"【{parentName} 修改成功】 " : $"【{parentName} 新增成功】";

                        Log.Info(new LoggerInfo()
                        {
                            LogType = LogType.基础信息.ToString(),
                            CreateUserId = UserInfo.Account,
                            Message = msg + $" 名称:{Name},简写:{model.Character}"
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
        private void btn_cancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 初始化加载
        private void BasicDetail_Load(object sender, EventArgs e)
        {
            lbl_ParentName.Text = parentName;
            txt_name.Text = name;
            if (id > 0)
            {
                using (var db = SugarDao.GetInstance())
                {
                    var model = db.Queryable<BasicDictionary>().SingleOrDefault(t => t.Id == id);
                    if (model != null && model.Id > 0)
                    {
                        txt_name.Text = model.Name;
                        txt_character.Text = model.Character;
                    }
                }
            }
        }
        #endregion

        #region 根据名称生成简码
        private void txt_name_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_name.Text.Trim()))
            {
                Encoding gb2312 = Encoding.GetEncoding("GB2312");
                txt_character.Text =
                    Pinyin.GetInitials(Pinyin.ConvertEncoding(txt_name.Text.Trim(), Encoding.UTF8, gb2312), gb2312)?
                        .ToLower();
            }
        }
        #endregion

        #region 窗体关闭
        private void BasicDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 回车
        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车按钮
            if (e.KeyChar == 13)
            {
                btn_save_Click(sender, e);
            }
        }
        #endregion
    }
}