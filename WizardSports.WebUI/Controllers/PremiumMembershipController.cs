using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WizardSports.WebUI.Controllers
{
    [Authorize]
    public class PremiumMembershipController : Controller
    {
        // GET: PremiumMembership
        //main premium membership page
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Premium")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        //admin access - update news
        public ActionResult UpdateNews()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        //admin access - update discounts
        public ActionResult UpdateDiscounts()
        {
            return View();
        }

        //advert displayed to non premium members
        public ActionResult Advert()
        {
            return View();
        }
    }
}