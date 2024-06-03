using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using ToDoSimple.Models;
using ToDoSimple.Models.Home;
using ToDoSimple.Models.Pagination;

namespace ToDoSimple.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ToDoContext _context;
        protected int _userId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public HomeController(ILogger<HomeController> logger, ToDoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var notes = from n in _context.Notes select n;
            if (!string.IsNullOrEmpty(searchString))
            {
                notes = notes.Where(
                    n=>n.Name.Contains(searchString)||
                    n.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    notes = notes.OrderBy(n => n.Name);
                    break;
                default:
                    notes = notes.OrderBy(n => n.CreatedTimestamp);
                    break;
            }

            return View(await notes.AsNoTracking().ToListAsync());
        }

        public async Task<PageViewModel> Page(PageViewModel page)
        {
            IEnumerable<Note> source;
            if (string.IsNullOrEmpty(page.SearchString))
            {
                source = await _context.Notes.OrderBy(n => n.Name).ToListAsync();
            }
            else
            {
                source = await _context.Notes.Where(n => n.Name.Contains(page.SearchString) || n.Description.Contains(page.SearchString)).ToListAsync();
            }
            page.Notes = source
                .Skip(page.TotalItemsCount / page.PageSize * (page.PageNumber - 1))
                .Take(page.PageSize).ToList();
            return page;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
                return View(note);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string description, bool isCompleted, string expireDate)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            note.Name = name;
            note.Description = description;
            note.IsCompleted = isCompleted;
            note.ExpireDate = DateTime.Parse(expireDate);
            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditA(HomeViewModel model)
        {
            Note note = new Note();
            note.Name = model.NoteName;
            note.Description = model.NoteDescription;
            note.ExpireDate = model.ExpireDate;
            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {

                Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
                //return PartialView(note);
                return View(note);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(new Note
                {
                    Name = model.NoteName,
                    Description = model.NoteDescription,
                    ExpireDate = model.ExpireDate,
                    UserId = _userId
                });
                await _context.SaveChangesAsync();
                //var note = await _context.Notes.FirstOrDefaultAsync(n => n.Name.ToLower() == model.NoteName.ToLower());
                //return RedirectToAction("Details", new { id = (_context.Notes.FirstOrDefault(n => n.Name == model.NoteName).Id) });//wtf?? 
                return RedirectToAction("Index"); // previously variant
            }
            return View("Create");//??????
        }
    }
}
