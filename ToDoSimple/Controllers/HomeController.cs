using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using ToDoSimple.Models;
using ToDoSimple.Models.Home;

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

        //public async Task<IActionResult> AddNoteAsync(string name, string description, bool isConpleted = false)
        //{
        //    Note note = new Note();
        //    note.Name = name;
        //    note.Description = description;
        //    note.IsCompleted = isConpleted;
        //    note.UserId = _accountId;
        //    _context.Notes.Add(note);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
        public ActionResult Edit([Bind("Name, Description, IsCompleted")] Note note)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(note).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(note);
        }
        //public ActionResult Edit([Bind("Name, Description, IsCompleted")] Note note)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _context.Entry(note).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(note);
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {

                Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
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
                UserId = _userId
            }) ;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = (_context.Notes.FirstOrDefault(n=>n.Name == model.NoteName).Id) });//wtf?? 
                //return View("Index", model); // previously variant
            }
            var note = await _context.Notes.FirstOrDefaultAsync(n=>n.Name.ToLower() == model.NoteName.ToLower());
            return View(note);//??????

        }
    }
}
