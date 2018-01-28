using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrayRabbit.MMS.Domain.Dto.OrderItem
{
    public class OrderItemListDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 药品id
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNum { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal Sale { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string MedicineName { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string YPGG { get; set; }

        /// <summary>
        /// 成产厂家
        /// </summary>
        public string SCCJ { get; set; }
    }
}
