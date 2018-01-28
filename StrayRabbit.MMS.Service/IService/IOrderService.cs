using System.Collections.Generic;
using StrayRabbit.MMS.Domain.Dto.Order;

namespace StrayRabbit.MMS.Service.IService
{
    public interface IOrderService
    {
        /// <summary>
        /// 根据类型获取订单列表
        /// </summary>
        /// <param name="type">1入库 2出库</param>
        /// <param name="strWhere">查询sql</param>
        /// <param name="orderCount">列表总数</param>
        /// <returns></returns>
        List<OrderListDto> GetOrderListByType(int type, string strWhere, int pageIndex, int pageSize, out int orderCount);
    }
}
