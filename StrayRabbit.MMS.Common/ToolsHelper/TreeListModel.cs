using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Common.ToolsHelper
{
    public class TreeListModel
    {
        #region 字段属性

        private int id;
        /// <summary>
        /// 节点编号
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int parentID;
        /// <summary>
        /// 父节点编号
        /// </summary>
        /// <value>The parent ID.</value>
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private string menuName;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }

        private object tag;

        /// <summary>
        /// 用于装节点对象
        /// </summary>
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        #endregion
    }
}
