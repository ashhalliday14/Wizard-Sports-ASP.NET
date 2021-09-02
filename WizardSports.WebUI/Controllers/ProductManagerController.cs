using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;
using WizardSports.Core.Contracts;
using WizardSports.Core.Models;
using WizardSports.Core.ViewModels;
using WizardSports.DataAccess.InMemory;

namespace WizardSports.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        //GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            
            return View(viewModel);
        }

        [HttpPost] //called once user hits create
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product); //returns the user if not valid
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Insert(product); //insert into products
                context.Commit(); //commit the saved changes

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id); //finds the product

            if(product == null)
            {
                return HttpNotFound(); //product not found
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(id); //find the product

            if (productToEdit == null)
            {
                return HttpNotFound(); //product could not be found
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.StockLevel = product.StockLevel;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);

            if(productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
               return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}