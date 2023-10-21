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
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            IEnumerable<Sale> sales = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync("sales");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Sale>>();
                    readTask.Wait();

                    sales = readTask.Result;
                }
                else
                {
                    //Error response received   
                    sales = Enumerable.Empty<Sale>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(sales);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Sale sale = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/sales/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sale>();
                    readTask.Wait();

                    sale = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Sale sale)
        {
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/");

                //Making a HttpPost Request
                var responseTask = client.PostAsJsonAsync("sales", sale);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sale>();
                    readTask.Wait();

                    sale = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Sale sale = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/sales/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sale>();
                    readTask.Wait();

                    sale = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Sale sale)
        {
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/sales/");

                //Making a HttpPost Request
                var responseTask = client.PutAsJsonAsync(id.ToString(), sale);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sale>();
                    readTask.Wait();

                    sale = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index"); ;
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Sale sale = null;
            using (var client = new HttpClient())
            {
                //Setting the base address of the API
                client.BaseAddress = new Uri("https://localhost:7115/api/sales/");

                //Making a HttpGet Request
                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sale>();
                    readTask.Wait();

                    sale = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                Sale sale = new Sale();
                client.BaseAddress = new Uri("https://localhost:7115/api/sales/");
                string selectedSale = id.ToString();
                var responseTask = client.DeleteAsync(selectedSale);

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Sale>();
                    readTask.Wait();

                    sale = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Index");
        }

        private bool SaleExists(int id)
        {
          return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
