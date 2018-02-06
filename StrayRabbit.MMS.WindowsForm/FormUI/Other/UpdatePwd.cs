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
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;

namespace StrayRabbit.MMS.WindowsForm.FormUI.Other
{
    public partial class UpdatePwd : DevExpress.XtraEditors.XtraForm
    {
        public UpdatePwd()
        {
            InitializeComponent();
        }

        #region 修改密码
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_OldPwd.Text.Trim()))
                {
                    XtraMessageBox.Show("请输入原始密码!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_NewPwd.Text.Trim()))
                {
                    XtraMessageBox.Show("请输入新密码!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_NewPwd2.Text.Trim()))
                {
                    XtraMessageBox.Show("请输入确认新密码!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txt_NewPwd.Text != txt_NewPwd2.Text)
                {
                    XtraMessageBox.Show("两次密码输入不一致!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var db = SugarDao.GetInstance())
                {
                    var userInfo = db.Queryable<Sys_User>().FirstOrDefault(t => t.Id == UserInfo.UserId);
                    if (userInfo == null || userInfo.Id <= 0)
                    {
                        XtraMessageBox.Show("请您重新登录!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (userInfo.Password != MD5Encrypt.Encrypt(txt_OldPwd.Text))
                    {
                        XtraMessageBox.Show("原始密码不正确!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (db.Update<Sys_User>($"password='{MD5Encrypt.Encrypt(txt_NewPwd.Text)}'",
                        t => t.Id == UserInfo.UserId))
                    {
                        XtraMessageBox.Show("修改成功，请你重新登录!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return;
                    }
                    else
                    {
                        XtraMessageBox.Show("修改失败!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}