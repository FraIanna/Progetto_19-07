using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_19_07.Controllers
{
    public class ViolationController : Controller
    {
        private readonly IViolationDao _violationDao;

        public ViolationController(IViolationDao violationDao)
        {
            _violationDao = violationDao;
        }

        public IActionResult GetNotContestedViolations()
        {
            var violations = _violationDao.GetAllNotContestedViolation();
            return View(violations);
        }

        public async Task<IActionResult> ViolationsOver400()
        {
            var violations = await _violationDao.GetViolationsOver400Async();
            return View(violations);
        }
    }
}
