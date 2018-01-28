namespace StrayRabbit.MMS.Domain.Dto.Order
{
    public class OrderListDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }


        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 状态 0保存 1提交
        /// </summary>
        public int Status { get; set; }
    }
}
