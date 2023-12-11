﻿using DoAn_PTUDWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoAn_PTUDWEB.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
        {
            return View();
        }

		

		[Route("/Contact")]
		public IActionResult Contact()
        {
            return View();
        }

		[Route("/About")]
		public IActionResult About()
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