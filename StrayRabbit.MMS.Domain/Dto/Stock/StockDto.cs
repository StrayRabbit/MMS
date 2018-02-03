namespace StrayRabbit.MMS.Domain.Dto.Stock
{
    public class StockDto
    {
        /// <summary>
        /// 库存Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string MedicineName { get; set; }

        /// <summary>
        /// 药品通用名称
        /// </summary>
        public string MedicineCommonName { get; set; }

        /// <summary>
        /// 简写
        /// </summary>
        public string NameCode { get; set; }

        /// <summary>
        /// 是否处方药
        /// </summary>
        public bool IsPrescription { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal Sale { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string YPGG { get; set; }

        /// <summary>
        /// 药品单位
        /// </summary>
        public string YPDW { get; set; }

        /// <summary>
        /// 药品批号
        /// </summary>
        public string BatchNum { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string SCCJ { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 经营范围名称
        /// </summary>
        public string JyfwName { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary>
        public string BzggName { get; set; }
    }
}
