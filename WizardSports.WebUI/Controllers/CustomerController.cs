using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WizardSports.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }
    }
}