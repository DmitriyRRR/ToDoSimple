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

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 4;
            int totalItemsCount = _context.Notes.Count();
            PageViewModel page = new PageViewModel();
            
            return View(await Page(page , pageNumber, pageSize, totalItemsCount));
        }

        public async Task<PageViewModel> Page(PageViewModel page, int pageNumber, int pageSize, int totalItemsCount)
        {
            page.TotalItemsCount = await _context.Notes.CountAsync();
            page.PageNumber = pageNumber;
            page.PageSize = pageSize;
            page.TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);

            page.Notes = await _context.Notes
                .Skip(page.TotalItemsCount / page.PageSize * (page.PageNumber - 1))
                .Take(page.PageSize).ToListAsync();
            return page;
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

        //public async Task<IActionResult> EditA(int? id)
        //{
        //    if (id != null)
        //    {
        //        Note note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
        //        HomeViewModel model = new HomeViewModel();

        //        model.NoteName = note.Name;
        //        model.NoteDescription = note.Description;
        //        model.ExpireDate = note.ExpireDate;

        //        return View(model);
        //    }
        //    return NotFound();
        //}

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
