using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WizardSports.Core.Contracts;
using WizardSports.Core.Models;
using WizardSports.Core.ViewModels;

namespace WizardSports.WebUI.Controllers
{
    public class MembersOnlyController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public MembersOnlyController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        // GET: MembersOnly
        //directs user to members only page
        public ActionResult Index()
        {
            //List<Product> products = context.Collection().ToList();
            return View("Index"); //returns view of all products
        }
    } 
}