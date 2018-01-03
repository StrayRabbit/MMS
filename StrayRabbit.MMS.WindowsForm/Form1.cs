using System.Windows.Forms;
using SQLiteSugar;
using StrayRabbit.MMS.Domain.Model;

namespace StrayRabbit.MMS.WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            var db = StrayRabbit.MMS.Domain.SugarDao.GetInstance();

            var user = db.Queryable<Sys_Role>().ToList();
        }
    }
}
