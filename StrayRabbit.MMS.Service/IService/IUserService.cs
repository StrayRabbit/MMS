using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrayRabbit.MMS.Domain.Model;

namespace StrayRabbit.MMS.Service.IService
{
    public interface IUserService
    {
        /// <summary>
        /// 检查账号密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        Sys_User CheckUser(string userName, string passWord);
    }
}
