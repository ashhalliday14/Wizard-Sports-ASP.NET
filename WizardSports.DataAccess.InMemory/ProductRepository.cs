using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using WizardSports.Core.Models;

namespace WizardSports.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default; //object cache created
        List<Product> products; //list for all products

        //product repository constructor
        public ProductRepository()
        {
            products = cache["products"] as List<Product>; //from in memory to product list
            if (products == null)
            {
                products = new List<Product>(); //create a new list
            }

        }

        public void Commit()
        {
            cache["products"] = products; //commit products to memory
        }

        public void Insert(Product product)
        {
            products.Add(product); //add product to list
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id); //finds the product

            if (productToUpdate != null)
            {
                productToUpdate = product; //updates the product
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.Id == id); //finds the product

            if (product != null)
            {
                return product; //returns the product once found
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productToDelete = products.Find(p => p.Id == id); //finds the product

            if (productToDelete != null)
            {
                products.Remove(productToDelete); //removes the product
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
