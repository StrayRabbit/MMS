using System.Collections.Generic;
using StrayRabbit.MMS.Domain.Dto.OrderItem;

namespace StrayRabbit.MMS.Service.IService
{
    public interface IOrderItemService
    {
        /// <summary>
        /// 根据单号查询订单详情列表
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        List<OrderItemListDto> GetOrderItemListByOrderNum(string orderNum);
    }
}
