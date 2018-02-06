using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 药品信息
    /// </summary>
    public class Medicine
    {
        public int Id { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 药品名称编码
        /// </summary>
        public string NameCode { get; set; }
        /// <summary>
        /// 所属经营范围
        /// </summary>
        public int? JYFWId { get; set; }
        /// <summary>
        /// 通用名称
        /// </summary>
        public string CommonName { get; set; }
        /// <summary>
        /// 通用名称编码
        /// </summary>
        public string CommonNameCode { get; set; }
        /// <summary>
        /// 包装规格
        /// </summary>
        public string BZGG { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public int? UnitId { get; set; }
        /// <summary>
        /// 药剂分类
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// 监管分类
        /// </summary>
        public int? JGFLId { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public int? SupplierId { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public int? SCCJId { get; set; }
        /// <summary>
        /// 批准文号
        /// </summary>
        public string CPZC { get; set; }
        /// <summary>
        /// 批准文号有效期
        /// </summary>
        public string PZWH { get; set; }
        /// <summary>
        /// 是否处方药
        /// </summary>
        public bool IsPrescription { get; set; }

        /// <summary>
        /// 状态 1有效 -1无效
        /// </summary>
        public int Status { get; set; }

        public int? OldId { get; set; }
    }
}
