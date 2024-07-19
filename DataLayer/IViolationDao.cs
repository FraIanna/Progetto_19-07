using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IViolationDao
    {
        ViolationEntity Create (ViolationEntity violation);

        ViolationEntity GetViolation (int ViolationId);

        List<ViolationEntity> GetAllNotContestedViolation();

        Task<List<ViolationEntity>> GetViolationsOver400Async();

        ViolationEntity Update (int ViolationId, ViolationEntity violation); 

        ViolationEntity Delete (int ViolationId);
    }
}
