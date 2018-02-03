namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 库存表
    /// </summary>
    public class Stock
    {
        public int Id { get; set; }

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

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 累计销售
        /// </summary>
        public decimal TotalSales { get; set; }
    }
}
