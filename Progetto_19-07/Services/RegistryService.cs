using DataLayer.Data;
using DataLayer.SqlServer;

namespace Progetto_19_07.Services
{
    public class RegistryService
    {
        private readonly DbContext _dbcontext;
        public RegistryService(DbContext dbContext) => _dbcontext = dbContext;

        public int CreateRegistry(RegistryEntity registryEntity)
        {
            RegistryEntity createdRegistry = _dbcontext.RegistryDao.Create(registryEntity);
            return createdRegistry.Id;
        }

        public void CreateVerbal(int registryId, VerbalEntity verbalEntity)
        {
            verbalEntity.RegistryId = registryId;
            _dbcontext.VerbalDao.Create(verbalEntity);
        }
    }
}
