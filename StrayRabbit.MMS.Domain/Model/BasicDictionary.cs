using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Model
{
    public class BasicDictionary
    {
        public int Id { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 拼音编码
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// 顺序排序
        /// </summary>
        public int OrderBy { get; set; }
    }
}
