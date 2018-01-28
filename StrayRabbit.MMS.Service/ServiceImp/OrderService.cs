using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteSugar;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Order;
using StrayRabbit.MMS.Domain.Dto.OrderItem;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;

namespace StrayRabbit.MMS.Service.ServiceImp
{
    public class OrderService : IOrderService
    {
        #region 根据类型获取订单列表

        /// <summary>
        /// 根据类型获取订单列表
        /// </summary>
        /// <param name="type">1入库 2出库</param>
        /// <param name="strWhere">查询sql</param>
        /// <param name="orderCount">列表总数</param>
        /// <returns></returns>
        public List<OrderListDto> GetOrderListByType(int type, string strWhere, int pageIndex, int pageSize, out int orderCount)
        {
            var list = new List<OrderListDto>();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    list = db.Queryable<Domain.Model.Order>()
                        .JoinTable<BasicDictionary>((o, gys) => o.SupplierId == gys.Id)
                        .JoinTable<Sys_User>((o, u) => o.CreateUserId == u.Account)
                        .Where($" Status>=0 and  Type={type} {strWhere}")
                        .Select<OrderListDto>("o.Id,o.OrderNum,o.SupplierId,gys.Name SupplierName,u.Name CreateUserName,o.CreateTime,o.Status")
                        .OrderBy(o => o.Status, OrderByType.Asc)
                        .OrderBy(o => o.Id, OrderByType.Desc)
                        .ToPageList(pageIndex, pageSize);

                    orderCount = db.Queryable<Domain.Model.Order>()
                        .JoinTable<BasicDictionary>((o, gys) => o.SupplierId == gys.Id)
                        .JoinTable<Sys_User>((o, u) => o.CreateUserId == u.Account)
                        .Where($" Status>=0 and Type={type} {strWhere}").Count();
                }

                return list;
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
