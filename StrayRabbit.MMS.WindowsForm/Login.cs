using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using StrayRabbit.MMS.Common.log4net;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;
using Log = StrayRabbit.MMS.Common.log4net.Log;

namespace StrayRabbit.MMS.WindowsForm
{
    public partial class Login : BaseForm
    {
        public Login()
        {
            InitializeComponent();
        }

        #region 登录
        private void btn_login_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_userName.Text.Trim()) ||
                   string.IsNullOrWhiteSpace(txt_password.Text.Trim()))
            {
                XtraMessageBox.Show("用户名或密码不能为空!");
                return;
            }

            IUserService userService = new UserService();

            var user = userService.CheckUser(txt_userName.Text.Trim(), txt_password.Text.Trim());
            if (user == null || user.Id <= 0)
            {
                XtraMessageBox.Show("用户或密码不正确!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                InitUserInfo(user);
                this.Hide();

                //业务日志
                Log.Info(new LoggerInfo()
                {
                    Message = $"{user.Name} 登录成功!",
                    CreateUserId = user.Account,
                    LogType = LogType.其他.ToString()
                });

                var form = new Main();
                form.ShowDialog();
            }
        }

        #endregion

        #region 取消
        private void btn_cancle_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 回车
        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车按钮
            if (e.KeyChar == 13)
            {
                btn_login_Click(sender, e);
            }
        }
        #endregion

        #region 自定义函数
        #region 加载全局变量UserInfo
        /// <summary>
        /// 加载全局变量UserInfo
        /// </summary>
        /// <param name="user"></param>
        private void InitUserInfo(Sys_User user)
        {
            if (user == null || user.Id <= 0) return;

            UserInfo.UserId = user.Id;
            UserInfo.RoleId = user.RoleId;
            UserInfo.UserName = user.Name;
            UserInfo.Account = user.Account;
        }
        #endregion

        #region 删除超出日期的业务日志

        private void DeleteLog()
        {
            try
            {
                int days = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LogDays"]);

                if (days > 0)
                {
                    using (var db = SugarDao.GetInstance())
                    {
                        db.Delete<StrayRabbit.MMS.Domain.Model.Log>($"julianday('now')-julianday(date)>={days}");
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #endregion

        #region Load
        private void Login_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblTitle.Text = System.Configuration.ConfigurationManager.AppSettings["Pharmacy"];

                Copy();

                DeleteLog();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 数据库备份
        public void Copy()        {            try            {                string fileName = DateTime.Now.ToString("yyyyMMdd") + ".db";

                var Path = System.Configuration.ConfigurationManager.AppSettings["Backup"];    //原文件的物理路径
                var targetPath = Path.Substring(0, Path.LastIndexOf("\\") + 1) + "backup\\" + fileName;    //复制到的新位置物理路径

                //判断到的新地址是否存在重命名文件
                if (!System.IO.File.Exists(targetPath))                {                    System.IO.File.Copy(Path, targetPath);  //复制到新位置,不允许覆盖现有文件
                }            }            catch (Exception)            {            }        }
        #endregion
    }
}