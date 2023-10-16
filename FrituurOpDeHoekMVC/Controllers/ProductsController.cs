using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrituurOpDeHoekMVC.Data;
using FrituurOpDeHoekMVC.Models;

namespace FrituurOpDeHoekMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products | Retrieves a list of all products and displays it to the user
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync("products");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();

                    products = readTask.Result;
                }
                else
                {
                    //Error response received   
                    products = Enumerable.Empty<Product>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(products);
        }

        // GET: Products/Details/5 | Retireves a specific product to display its contents to the user
        public IActionResult Details(Product product)
        {

            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/products/");

                //Converting the id of the requested product to a string for routing purposes
                string selectedProductId = product.Id.ToString();

                //Making a HttpGet Request
                var responseTask = client.GetAsync(selectedProductId);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(product);
        }

        //Post : products | Allows the user to create a new product in the database
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Product product)
        {

            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/");

                //Making a HttpPost Request
                var responseTask = client.PostAsJsonAsync("products", product);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Product product = new Product();
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/products/");

                //Making the HttpGet Request so the user can see the data they are about to edit  
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/products/");

                //Converting the id of the selected item for routing purposes
                string selectedProductId = product.Id.ToString();

                //Making the HttpPut request aswell as converting the data to a json file
                var responseTask = client.PutAsJsonAsync(selectedProductId, product);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Product product = new Product();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7115/api/products/");

                //Called Member default GET All records  
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7115/api/products/");
                string selectedProductId = product.Id.ToString();
                var responseTask = client.DeleteAsync(selectedProductId);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
