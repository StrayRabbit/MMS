using System.Collections.Generic;
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

        /// <summary>
        /// 根据角色Id获取菜单列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        List<Sys_Module> GetModulesByRoleId(int roleId);
    }
}
