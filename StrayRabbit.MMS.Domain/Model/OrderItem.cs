using System;
using System.Security.AccessControl;

namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 药品id
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// 库存id
        /// </summary>
        public int StockId { get; set; }

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
    }
}
