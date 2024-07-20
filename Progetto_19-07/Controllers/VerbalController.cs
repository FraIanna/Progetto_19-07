using DataLayer;
using DataLayer.Data;
using DataLayer.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Progetto_19_07.Services;

namespace Progetto_19_07.Controllers
{
    public class VerbalController : Controller
    {
        private readonly DbContext _dbcontext;
        private readonly RegistryService _registryService;

        public VerbalController(DbContext dbContext, RegistryService registryService)
        {
            _dbcontext = dbContext;
            _registryService = registryService;
        }

        public IActionResult CreateVerbal() 
        {
            int? registryId = HttpContext.Session.GetInt32("RegistryId");
            ViewBag.RegistryId = registryId.Value;
            return View();
        }

        [HttpPost]
        public IActionResult CreateVerbal(VerbalEntity verbal)
        {
            try
            {
                if (verbal.RegistryId == 0)
                {
                    throw new Exception("ID anagrafica non valido.");
                }
                _registryService.CreateVerbal(verbal.RegistryId, verbal);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            // ci ho provato non trovo una soluzione
            {
                ModelState.AddModelError("", $"Errore: {ex.Message}");
            }
            return View();
        }

        public async Task<IActionResult> TotalByTrasgressor()
        {
            var summary = await _dbcontext.VerbalDao.GetTotalVerbalByTrasgressorAsync();
            return View(summary);
        }

        public async Task<IActionResult> TotalPointsByTrasgressor()
        {
            var summary = await _dbcontext.VerbalDao.GetTotalPointsByTrasgressorAsync();
            return View(summary);
        }

        public async Task<IActionResult> ViolationsOver10Points()
        {
            var details = await _dbcontext.VerbalDao.GetViolationsOver10PointsAsync();
            return View(details);
        }
    }
}
