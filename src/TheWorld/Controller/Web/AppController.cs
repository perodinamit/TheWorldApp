using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controles.Web
{
    public class AppController : Controller
    {
        private IMailService _mailservice;
        private IWorldRepository _repository;

        public AppController(IMailService service, IWorldRepository repository) //constructor
        {
            _mailservice = service;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var trips = _repository.GetAllTrips();
            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration problem");
                }

                if (_mailservice.SendMail(email, email, $"Contact Page from {model.Name} ({model.Email})", model.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail sent. Thanks!";
                }
            }
            return View();
        }

    }
}
