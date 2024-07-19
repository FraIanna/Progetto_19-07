using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IVerbalDao
    {
        VerbalEntity Create(VerbalEntity verbal);

        VerbalEntity GetVerbal(int verbalId);

        Task<List<VerbalSummary>> GetTotalVerbalByTrasgressorAsync();

        Task<List<PointsSummary>> GetTotalPointsByTrasgressorAsync();

        Task<List<ViolationDetails>> GetViolationsOver10PointsAsync();

        VerbalEntity Update (int verbalId, VerbalEntity verbal);

        VerbalEntity DeleteVerbal(int verbalId);
    }
}
