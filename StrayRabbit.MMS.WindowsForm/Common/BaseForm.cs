namespace StrayRabbit.MMS.WindowsForm
{
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        public static UserInfo userInfo;

        public BaseForm()
        {
            InitializeComponent();
        }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
        public int RoleId { get; set; }
    }
}