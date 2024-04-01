using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoSimple.Models;

namespace ToDoSimple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Create(string name, string discription)
        {
            Note note = new Note();
            note.Name = name;
            note.Description = discription;
            return View(note);
        }
        public async Task<IActionResult> Delete(string id) //реализовать
        {
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edite(string id) //реализовать
        {
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
