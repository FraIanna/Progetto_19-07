using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace Progetto_19_07.Controllers
{
    public class VerbalController : Controller
    {
        private readonly IVerbalDao _verbalDao;

        public VerbalController(IVerbalDao verbalDao)
        {
            _verbalDao = verbalDao;
        }

        public IActionResult CreateVerbal() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult CreateVerbal(VerbalEntity verbal)
        {
            if (ModelState.IsValid)
            {
                var createdVerbal = _verbalDao.Create(verbal);
                if (createdVerbal != null)
                {
                    return Ok(createdVerbal);
                }
                return BadRequest("Errore nella creazione del verbale.");
            }
            return BadRequest("Dati del verbale non validi.");
        }

        public async Task<IActionResult> TotalByTrasgressor()
        {
            var summary = await _verbalDao.GetTotalVerbalByTrasgressorAsync();
            return View(summary);
        }

        public async Task<IActionResult> TotalPointsByTrasgressor()
        {
            var summary = await _verbalDao.GetTotalPointsByTrasgressorAsync();
            return View(summary);
        }

        public async Task<IActionResult> ViolationsOver10Points()
        {
            var details = await _verbalDao.GetViolationsOver10PointsAsync();
            return View(details);
        }
    }
}
