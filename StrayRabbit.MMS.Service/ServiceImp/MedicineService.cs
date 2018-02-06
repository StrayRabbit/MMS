using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteSugar;
using StrayRabbit.MMS.Domain;
using StrayRabbit.MMS.Domain.Dto.Medicine;
using StrayRabbit.MMS.Domain.Model;
using StrayRabbit.MMS.Service.IService;

namespace StrayRabbit.MMS.Service.ServiceImp
{
    public class MedicineService : IMedicineService
    {

        #region 根据药品Id获取药品信息
        /// <summary>
        /// 根据药品Id获取药品信息
        /// </summary>
        /// <param name="medicineId"></param>
        /// <returns></returns>
        public MedicineListDto GetMedicineInfoById(int medicineId)
        {
            var entity = new MedicineListDto();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var list = db.Queryable<Domain.Model.Medicine>()
                         .JoinTable<BasicDictionary>((m, jyfw) => m.JYFWId == jyfw.Id)
                         .JoinTable<BasicDictionary>((m, dw) => m.UnitId == dw.Id)
                         .JoinTable<BasicDictionary>((m, jgfl) => m.JGFLId == jgfl.Id)
                         .JoinTable<BasicDictionary>((m, ypfl) => m.TypeId == ypfl.Id)
                         .JoinTable<BasicDictionary>((m, gys) => m.SupplierId == gys.Id)
                         .Where(" m.Id=" + medicineId)
                         .Select<MedicineListDto>(
                             "m.Id,m.Name,m.NameCode,jyfw.Name as jyfwName,m.CommonName,BZGG BzggName,dw.Name as UnitName,jgfl.Name JgflName,ypfl.Name ypflName,gys.Name gysName,m.CPZC,ypfl.Name as YpflName")
                         .ToList();

                    if (list != null && list.Any())
                    {
                        entity = list[0];
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entity;
        }
        #endregion

        #region 获取药品列表
        /// <summary>
        /// 获取药品列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public List<MedicineListDto> GetMedicineList(string search)
        {
            var list = new List<MedicineListDto>();

            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    list = db.Queryable<Domain.Model.Medicine>()
                        .JoinTable<BasicDictionary>((m, jyfw) => m.JYFWId == jyfw.Id)
                        .JoinTable<BasicDictionary>((m, dw) => m.UnitId == dw.Id)
                        .JoinTable<BasicDictionary>((m, jgfl) => m.JGFLId == jgfl.Id)
                        .JoinTable<BasicDictionary>((m, ypfl) => m.TypeId == ypfl.Id)
                        .JoinTable<BasicDictionary>((m, gys) => m.SupplierId == gys.Id)
                        .Where(" Status=1 and (m.Name like '%" + search + "%' or m.NameCode like '%" +
                               search + "%')")
                        .Select<MedicineListDto>(
                            "m.Id,m.Name,m.NameCode,jyfw.Name as jyfwName,m.CommonName,BZGG BzggName,dw.Name as UnitName,jgfl.Name JgflName,ypfl.Name ypflName,gys.Name gysName,m.CPZC,ypfl.Name as YpflName")
                        .OrderBy(m => m.Id, OrderByType.Desc)
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
