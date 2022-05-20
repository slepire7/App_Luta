using App_Luta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App_Luta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.LutaService _lutaService;
        private const int QUANTIDADE_TORNEIO = 16;
        public HomeController(ILogger<HomeController> logger, Services.LutaService lutaService)
        {
            _logger = logger;
            _lutaService = lutaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.QuantidadeTorneio = QUANTIDADE_TORNEIO;
            var rest = await _lutaService.GetAll();
            return View(rest);
        }
        [HttpPost]
        public IActionResult Resultado([FromBody] DTOs.IniciarTorneio data)
        {
            if (ModelState.IsValid)
            {
                var campeao = _lutaService.ObterCampea(data.IdsLutadores);
                return Json(new { success = true, campeao });
            }
            string Errors = string.Join(" | ", ModelState.Values
                                          .SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage));
            return Json(new { success = false, erros = Errors });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
