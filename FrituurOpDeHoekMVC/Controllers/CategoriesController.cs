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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Category> categories = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync("categories");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Category>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else
                {
                    //Error response received   
                    categories = Enumerable.Empty<Category>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(categories);
        }
    

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Category category = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/categories/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/");

                //Making a HttpPost Request
                var responseTask = client.PostAsJsonAsync("categories", category);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Category category = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/categories/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/categories/");

                //Making a HttpPost Request
                var responseTask = client.PutAsJsonAsync(id.ToString(), category);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index"); ;
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Category category = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/categories/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            using (var client = new HttpClient())
            {
                Category category = new Category();
                client.BaseAddress = new Uri("https://localhost:7115/api/categories/");
                string selectedCategory = id.ToString();
                var responseTask = client.DeleteAsync(selectedCategory);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Category>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
