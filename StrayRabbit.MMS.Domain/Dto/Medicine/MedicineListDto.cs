using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Dto.Medicine
{
    public class MedicineListDto
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
        public string JyfwName { get; set; }
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
        public string BzggName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 监管分类
        /// </summary>
        public string JgflName { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string GysName { get; set; }
        
        /// <summary>
        /// 产品注册证批件号
        /// </summary>
        public string CPZC { get; set; }

        /// <summary>
        /// 药品分类
        /// </summary>
        public string YpflName { get; set; }
    }
}
