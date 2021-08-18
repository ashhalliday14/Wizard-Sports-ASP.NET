using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WizardSports.WebUI.Controllers
{
    public class MembersOnlyController : Controller
    {
        // GET: MembersOnly
        public ActionResult Index()
        {
            return View();
        }
    }
}