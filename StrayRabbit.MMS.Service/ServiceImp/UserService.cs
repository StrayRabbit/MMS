using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteSugar;
using StrayRabbit.MMS.Common;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;

namespace StrayRabbit.MMS.Service.ServiceImp
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 检查账号密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Sys_User CheckUser(string userName, string passWord)
        {
            Sys_User result = null;
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord))
                {
                    return result;
                }

                using (var db = SugarDao.GetInstance())
                {
                    string uName = userName.ToLower();
                    string pwd = MD5Encrypt.Encrypt(passWord);

                    result = db.Queryable<Sys_User>().FirstOrDefault(t => t.Account == uName && t.Password == pwd && t.IsEnabled);
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
