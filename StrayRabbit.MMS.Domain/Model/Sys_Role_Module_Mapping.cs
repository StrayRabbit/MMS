using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 角色模块映射表
    /// </summary>
    public class Sys_Role_Module_Mapping
    {
        public int Id { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public int ModuleId { get; set; }
    }
}
