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
        //shows the list of all products
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products); //returns view of all products
        }

        //allows admin to create a new product
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

                return RedirectToAction("Index"); //return user to products page
            }
        }

        //allows admin to edit products
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

        [HttpPost] //called when user hits edit
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

                //changes details about products
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.StockLevel = product.StockLevel;

                context.Commit(); //commit the changes

                return RedirectToAction("Index"); //return the user
            }
        }

        //allows user to delete a product
        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id); //finds the product

            if(productToDelete == null)
            {
                return HttpNotFound(); //product could not be found
            }
            else
            {
                return View(productToDelete); //allows user to delete product
            }
        }

        [HttpPost] //called when user hits delete
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = context.Find(id); //finds the product

            if (productToDelete == null)
            {
               return HttpNotFound(); //product could not be found
            }
            else
            {
                context.Delete(id); //delete the product
                context.Commit(); //commits the changes
                return RedirectToAction("Index"); //returns the user
            }
        }
    }
}