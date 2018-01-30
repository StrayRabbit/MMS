using System.Collections.Generic;
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
        /// <returns></returns>
        public List<StockListDto> GetStockList(string name)
        {
            var result = new List<StockListDto>();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    result = db.Queryable<Domain.Model.Stock>()
                        .JoinTable<Medicine>((s, m) => s.MedicineId == m.Id)
                        .JoinTable<Medicine, BasicDictionary>((s, m, ypgg) => m.PackModelId == ypgg.Id)     //药品规格
                        .JoinTable<Medicine, BasicDictionary>((s, m, sccj) => m.SCCJId == sccj.Id)      //生产厂家
                        .JoinTable<Medicine, BasicDictionary>((s, m, ypdw) => m.UnitId == ypdw.Id)      //药品单位
                        .Where<Medicine>((s, m) => s.Amount > 0 && (m.NameCode.Contains(name) || m.Name.Contains(name)))
                        .Select<StockListDto>("s.Id,m.Name MedicineName,m.CommonName MedicineCommonName,m.NameCode,m.IsPrescription,s.Sale,ypgg.Name YPGG,ypdw.Name YPDW,s.BatchNum,s.Amount,sccj.Name SCCJ,s.BeginDate,s.EndDate")
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
    }
}
