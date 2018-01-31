namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 库存日志
    /// </summary>
    public class StockLog
    {
        public int Id { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string Type { get; set; }

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
        /// 创建人账号
        /// </summary>
        public string CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
