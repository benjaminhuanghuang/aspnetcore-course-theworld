using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        //private WorldContext _context;
        private IWorldRepository _repository;
        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
        }
        public IActionResult Index()
        {
            //var data = _context.Trips.ToList();
            IEnumerable<Trip> data = NewMethod();

            return View(data);
        }

        private IEnumerable<Trip> NewMethod()
        {
            return _repository.GetAllTrips();
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
