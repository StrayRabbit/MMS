using System.Collections.Generic;
using StrayRabbit.MMS.Domain.Dto.Medicine;

namespace StrayRabbit.MMS.Service.IService
{
    public interface IMedicineService
    {
        /// <summary>
        /// 根据药品Id获取药品信息
        /// </summary>
        /// <param name="medicineId"></param>
        /// <returns></returns>
        MedicineListDto GetMedicineInfoById(int medicineId);

        /// <summary>
        /// 获取药品列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        List<MedicineListDto> GetMedicineList(string search);
    }
}
