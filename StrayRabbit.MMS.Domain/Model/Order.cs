namespace StrayRabbit.MMS.Domain.Model
{
    /// <summary>
    /// 单据信息
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        /// <summary>
        /// 单据类型 1入库 2出库
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 状态 0保存 1提交 -1删除
        /// </summary>
        public int Status { get; set; }
    }
}
