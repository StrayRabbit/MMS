using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using StrayRabbit.MMS.Service.IService;
using StrayRabbit.MMS.Service.ServiceImp;

namespace StrayRabbit.MMS.WindowsForm
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Main()
        {
            InitializeComponent();
        }

        #region 加载菜单
        /// <summary>
        /// 加载菜单
        /// </summary>
        private void InitMenu()
        {
            try
            {
                IUserService userService = new UserService();
                var list = userService.GetModulesByRoleId(UserInfo.RoleId);

                if (list != null && list.Any())
                {
                    NavBarGroup group;
                    NavBarItem nbItem;

                    foreach (var m in list.Where(t => t.ParentId == 0))
                    {
                        group = new NavBarGroup(m.Name);
                        navBarControl.Groups.Add(group);

                        foreach (var c in list.Where(l => l.ParentId == m.Id))
                        {
                            nbItem = new NavBarItem()
                            {
                                Caption = c.Name,
                                Tag = c.ModulePath,
                            };
                            group.ItemLinks.Add(nbItem);
                            nbItem.LinkClicked += this.navBarItem_ItemClick;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"加载菜单异常!{ex.Message}");
                throw;
            }
        }
        #endregion

        #region 数据加载
        private void Main_Load(object sender, EventArgs e)
        {
            InitMenu();
        }
        #endregion

        #region 动态调出窗体事件
        /// <summary>
        /// 动态调出窗体事件 注：动态调用窗体名需和数据库中名称完全一致
        /// </summary>
        private void navBarItem_ItemClick(object sender, NavBarLinkEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Link.Item.Tag.ToString()) && !IsOpen(e.Link.Caption))
            {
                Assembly asm = Assembly.Load("StrayRabbit.MMS.WindowsForm");
                var childForm = (XtraForm)asm.CreateInstance(e.Link.Item.Tag.ToString());
                if (childForm != null)
                {
                    UserInfo.ChildHeight = this.Height -230;
                    UserInfo.ChildWidth = this.Width - 200;

                    childForm.MdiParent = this;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
            }
        }
        #endregion

        #region 判断是否已打开
        /// <summary>
        /// 判断是否已打开
        /// </summary>
        private bool IsOpen(string caption)
        {
            foreach (XtraForm f in MdiChildren)
            {
                if (f.Text == caption)
                {
                    //xfs.Remove(f);
                    //f.Close();
                    //break;

                    f.Select();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 关闭窗体
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        } 
        #endregion
    }
}