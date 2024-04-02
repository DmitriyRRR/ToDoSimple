using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoSimple.Models;

namespace ToDoSimple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ToDoContext _context;
        public HomeController(ILogger<HomeController> logger, ToDoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Note> notes = await _context.Notes.ToListAsync();

            return View(notes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Create() => View();

        public async Task<IActionResult> AddAsync(string name, string description, bool isConpleted = false)
        {
            Note note = new Note();
            note.Name = name;
            note.Description = description;
            note.IsCompleted = isConpleted;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit() => View();

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, string name, string description, bool isConpleted = false) //реализовать
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note != null)
            {
                note.Name = name;
                note.Description = description;
                note.IsCompleted = isConpleted;
            }
            return RedirectToAction("Index");
        }
    }
}
