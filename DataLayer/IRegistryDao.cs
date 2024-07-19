using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRegistryDao
    {
        RegistryEntity Create(RegistryEntity registry);

        RegistryEntity GetRegistry(int registryId);

        RegistryEntity Update(int registryId, RegistryEntity registry);

        RegistryEntity Delete(int registryId);
    }
}
