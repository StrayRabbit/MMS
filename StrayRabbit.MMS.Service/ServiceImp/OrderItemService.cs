using System;
using System.Collections.Generic;
using SQLiteSugar;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.OrderItem;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;

namespace StrayRabbit.MMS.Service.ServiceImp
{
    public class OrderItemService : IOrderItemService
    {
        #region 根据单号查询订单详情列表
        /// <summary>
        /// 根据单号查询订单详情列表
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public List<OrderItemListDto> GetOrderItemListByOrderNum(string orderNum)
        {
            var list = new List<OrderItemListDto>();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    list = db.Queryable<Domain.Model.OrderItem>()
                        .JoinTable<Medicine>((o, m) => o.MedicineId == m.Id)
                        .JoinTable<Medicine, BasicDictionary>((o, m, sccj) => m.SCCJId == sccj.Id)
                        .Where(" OrderNum='" + orderNum + "'")
                        .Select<OrderItemListDto>("o.Id,o.OrderId,o.MedicineId,o.BatchNum,o.Amount,o.Cost,o.Sale,o.BeginDate,o.EndDate,m.Name MedicineName,m.BZGG as YPGG,sccj.Name as SCCJ")
                        .OrderBy(m => m.Id, OrderByType.Asc)
                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }
        #endregion
    }
}
