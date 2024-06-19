using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItSystem.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using ItSystem.Models;

namespace ItSystem.Controllers
{
    public class TasksController : Controller
    {
        public class TaskViewModel
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public DateTime DateCreate { get; set; }

            public DateTime? DateChange { get; set; }

            public Guid IdExecutor { get; set; }

            public Guid IdAuthor { get; set; }

            public Guid IdBoard { get; set; }

            public bool IsDelete { get; set; }

            public string ShortName { get; set; } = "";

            public int Status { get; set; }

            public int Priority { get; set; }

        }

        private readonly ItSystemContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ItSystemContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var itSystemContext = _context.Tasks.Include(t => t.IdAuthorNavigation).Include(t => t.IdBoardNavigation).Include(t => t.IdExecutorNavigation);
            return View(await itSystemContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.IdAuthorNavigation)
                .Include(t => t.IdBoardNavigation)
                .Include(t => t.IdExecutorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["IdBoard"] = new SelectList(_context.Boards, "Id", "Name");
            ViewData["IdExecutor"] = new SelectList(_context.Users, "Id", "ShortName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IdExecutor,IdBoard,Priority")] TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == task.IdBoard);
                var taskCount = _context.Tasks.Where(x => x.IdBoard == task.IdBoard).Count();

                var newTask = new ItSystem.Models.DbModels.Task
                {
                    Id = Guid.NewGuid(),
                    DateCreate = DateTime.UtcNow,
                    IsDelete = false,
                    IdAuthor = Guid.Parse(user.Id),
                    DateChange = DateTime.UtcNow,
                    Status = (int)StatusEnum.BACKLOG,
                    ShortName = $"{board.ShortName.Remove(' ')}-{(taskCount + 1)}",
                    Name = task.Name,
                    Description = task.Description,
                    IdExecutor = task.IdExecutor,
                    IdBoard = task.IdBoard,
                    Priority = task.Priority
                };


                _context.Add(newTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBoard"] = new SelectList(_context.Boards, "Id", "Name", task.IdBoard);
            ViewData["IdExecutor"] = new SelectList(_context.Users, "Id", "ShortName", task.IdExecutor);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["IdBoard"] = new SelectList(_context.Boards, "Id", "Name", task.IdBoard);
            ViewData["IdExecutor"] = new SelectList(_context.Users, "Id", "ShortName", task.IdExecutor);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,DateCreate,DateChange,IdExecutor,IdAuthor,IsDelete,IdBoard,ShortName,Status,Priority")] ItSystem.Models.DbModels.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAuthor"] = new SelectList(_context.Users, "Id", "Id", task.IdAuthor);
            ViewData["IdBoard"] = new SelectList(_context.Boards, "Id", "Id", task.IdBoard);
            ViewData["IdExecutor"] = new SelectList(_context.Users, "Id", "Id", task.IdExecutor);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.IdAuthorNavigation)
                .Include(t => t.IdBoardNavigation)
                .Include(t => t.IdExecutorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(Guid id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
