using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        //private WorldContext _context;
        private IWorldRepository _repository;
        private ILogger _logger;
        public AppController(IMailService mailService, IConfigurationRoot config,
            IWorldRepository repository, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                //var data = _context.Trips.ToList();
                IEnumerable<Trip> trips = _repository.GetAllTrips();

                return View(trips);
            }
            catch (Exception exp)
            {
                _logger.LogError($"Failed to get trips in Index page:{exp.Message}");
                return Redirect("/error");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPostAttribute]
        public IActionResult Contact(ContactVM model)
        {
            if (model.Email.Contains("aol.com"))
            {
                //Show error at client side as summary
                ModelState.AddModelError("", "We don't support AOL address");
            }
            if (ModelState.IsValid)
            {
                var to = _config["MailSettings:ToAddress"];
                _mailService.SendMail("ben@gmail.com", model.Email, "From TheWorld", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message Send";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
