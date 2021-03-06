﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch_Example_Project.Domain.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ElasticSearch_Example_Project.Models;
using ElasticSearch_Example_Project.Services;

namespace ElasticSearch_Example_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Searches Product Documents
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns></returns>
        public async Task<IActionResult> Search(string query)
        {
            return Ok(await _productService.Search(query));
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
