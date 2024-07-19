using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_19_07.Controllers
{
    public class RegistryController : Controller
    {
        private readonly IRegistryDao _registryDao;

        public RegistryController(IRegistryDao registryDao) {
            _registryDao = registryDao;
        }

        public IActionResult RegisterOffender()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterOffender(RegistryEntity registry)
        {
            var createdRegistry = _registryDao.Create(registry);
            if(createdRegistry == null)
            {
                return Ok(createdRegistry);
            }
            return View(registry);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetRegistry(int id)
        //{
        //    var registry = _registryDao.GetRegistry(id);
        //    if (registry != null)
        //    {
        //        return Ok(registry);
        //    }
        //    return NotFound();
        //}
    }
}
