using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 系统菜单模块
    /// </summary>
    public class Sys_Module
    {
        public int Id { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单链接模块
        /// </summary>
        public string ModulePath { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderBy { get; set; }
    }
}
