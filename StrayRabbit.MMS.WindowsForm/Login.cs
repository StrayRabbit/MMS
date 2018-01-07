using System.Windows.Forms;
using DevExpress.XtraEditors;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;

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

                var form = new Main();
                form.ShowDialog();
                this.Close();
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
        }
        #endregion
        #endregion
    }
}