using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    public class DbContext
    {
        public IRegistryDao RegistryDao { get; set; }
        public IVerbalDao VerbalDao { get; set; }
        public IViolationDao ViolationDao { get; set; }

        public DbContext(IRegistryDao registryDao, IVerbalDao verbalDao, IViolationDao violationDao) 
        { 
            RegistryDao = registryDao;
            VerbalDao = verbalDao;  
            ViolationDao = violationDao;
        }
    }
}
