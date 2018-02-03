using System.Collections.Generic;
using StrayRabbit.MMS.Domain.Dto.Stock;

namespace StrayRabbit.MMS.Service.IService
{
    public interface IStockService
    {

        /// <summary>
        /// 根据条件查询库存列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<StockListDto> GetStockList(string name);

        /// <summary>
        /// 根据id查询库存信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StockDto GetStockInfo(int id);
    }
}
