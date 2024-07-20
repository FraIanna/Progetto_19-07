using DataLayer;
using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Progetto_19_07.Services;

namespace Progetto_19_07.Controllers
{
    public class RegistryController : Controller
    {
        private readonly RegistryService _registryService;

        public RegistryController(RegistryService registryService) {
            _registryService = registryService;
        }

        public IActionResult RegisterOffender()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterOffender(RegistryEntity registry)
        {
            try
            {
                int registryId = _registryService.CreateRegistry(registry);

                TempData["SuccessMessage"] = "Anagrafica registrata con successo. Puoi ora aggiungere il verbale.";
                HttpContext.Session.SetInt32("RegistryId", registryId);

                return RedirectToAction("CreateVerbal", "Verbal");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Errore: {ex.Message}");
            }
            return View();
        }
    }
}
