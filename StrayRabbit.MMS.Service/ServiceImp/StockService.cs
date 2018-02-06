using System.Collections.Generic;
using System.Linq;
using SQLiteSugar;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.OrderItem;
using StrayRabbit.MMS.Domain.Dto.Stock;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;

namespace StrayRabbit.MMS.Service.ServiceImp
{
    public class StockService : IStockService
    {
        /// <summary>
        /// 根据条件查询库存列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dqts">到期天数</param>
        /// <returns></returns>
        public List<StockListDto> GetStockList(string name, int dqts = 0)
        {
            var result = new List<StockListDto>();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var sqlWhere = $"s.Amount>0 and (m.NameCode like '%{name}%' or m.Name like '%{name}%')";
                    if (dqts > 0)
                    {
                        sqlWhere += $" and julianday(enddate)-julianday('now')<={dqts}";
                    }

                    result = db.Queryable<Domain.Model.Stock>()
                        .JoinTable<Medicine>((s, m) => s.MedicineId == m.Id)
                        .JoinTable<Medicine, BasicDictionary>((s, m, sccj) => m.SCCJId == sccj.Id)      //生产厂家
                        .JoinTable<Medicine, BasicDictionary>((s, m, ypdw) => m.UnitId == ypdw.Id)      //药品单位
                        .JoinTable<Medicine, BasicDictionary>((s, m, gys) => m.SupplierId == gys.Id)      //药品单位
                                                                                                          //.Where<Medicine>((s, m) => s.Amount > 0 && (m.NameCode.Contains(name) || m.Name.Contains(name)))
                        .Where(sqlWhere)
                        .Select<StockListDto>("s.Id,m.Name MedicineName,m.CommonName MedicineCommonName,m.NameCode,m.IsPrescription,s.Sale,m.bzgg YPGG,ypdw.Name YPDW,s.BatchNum,s.Amount,sccj.Name SCCJ,s.BeginDate,s.EndDate,gys.Name GysName,s.TotalSales")
                        .OrderBy(m => m.Id, OrderByType.Asc)
                        .ToList();
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// 查询库存信息
        /// </summary>
        /// <param name="id">库存Id</param>
        /// <returns></returns>
        public StockDto GetStockInfo(int id)
        {
            var result = new StockDto();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var list = db.Queryable<Domain.Model.Stock>()
                        .JoinTable<Medicine>((s, m) => s.MedicineId == m.Id)
                        .JoinTable<Medicine, BasicDictionary>((s, m, sccj) => m.SCCJId == sccj.Id)      //生产厂家
                        .JoinTable<Medicine, BasicDictionary>((s, m, ypdw) => m.UnitId == ypdw.Id)      //药品单位
                        .JoinTable<Medicine, BasicDictionary>((s, m, gys) => m.SupplierId == gys.Id)      //供应商
                        .JoinTable<Medicine, BasicDictionary>((s, m, jyfw) => m.JYFWId == jyfw.Id)      //经营范围
                        .Where($" s.Id={id}")
                        .Select<StockDto>("s.Id,m.Name MedicineName,m.CommonName MedicineCommonName,m.NameCode,m.IsPrescription,s.Sale,m.BZGG YPGG,ypdw.Name YPDW,s.BatchNum,s.Amount,sccj.Name SCCJ,s.BeginDate,s.EndDate,gys.Name GysName,s.Cost,jyfw.Name JyfwName").ToList();

                    if (list != null && list.Any())
                    {
                        result = list[0];
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return result;
        }
    }
}
