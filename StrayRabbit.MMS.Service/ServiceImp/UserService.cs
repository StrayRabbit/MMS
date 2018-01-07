using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteSugar;
using StrayRabbit.MMS.Common;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Sys_User;
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

        /// <summary>
        /// 根据角色Id获取菜单列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public List<Sys_Module> GetModulesByRoleId(int roleId)
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var list = db.Queryable<Sys_Role_Module_Mapping>()
                        .JoinTable<Sys_Module>((s1, s2) => s1.ModuleId == s2.Id)
                        .Where<Sys_Module>((s1, s2) => s2.IsShow && s1.RoleId == roleId)
                        .OrderBy<Sys_Module>((s1, s2) => s2.OrderBy, OrderByType.Desc)
                        .OrderBy<Sys_Module>((s1, s2) => s2.Id)
                        .Select<Sys_Module, Sys_Module>((s1, s2) => new Sys_Module()
                        {
                            Id = s2.Id,
                            Name = s2.Name,
                            OrderBy = s2.OrderBy,
                            IsShow = s2.IsShow,
                            ModulePath = s2.ModulePath,
                            ParentId = s2.ParentId
                        }).ToList();

                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
