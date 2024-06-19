using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItSystem.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace ItSystem.Controllers
{
    public class BoardsController : Controller
    {

        public class BoardViewModel
        {
            public Guid Id { get; set; }

            [Required]
            public string Name { get; set; } = null!;

            [Required]
            public string Description { get; set; } = null!;

            [Required]
            public Guid IdProject { get; set; }

            [Required]
            public string ShortName { get; set; } = null!;

            public List<ItSystem.Models.DbModels.Task> Tasks { get; set; } = null!;

        }

        private readonly ItSystemContext _context;

        public BoardsController(ItSystemContext context)
        {
            _context = context;
        }

        // GET: Boards
        public async Task<IActionResult> Index()
        {
            var itSystemContext = _context.Boards.Include(b => b.IdProjectNavigation);


            return View(await itSystemContext.ToListAsync());
        }

        // GET: Boards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
                .Include(b => b.IdProjectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            var tasks = await _context.Tasks
                .Include(t => t.IdAuthorNavigation)
                .Include(t => t.IdExecutorNavigation)
                .Where(m => m.IdBoard == id)
                .ToListAsync();

            if (board == null)
            {
                return NotFound();
            }

            var boardViewModel = new BoardViewModel
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                IdProject = board.IdProject,
                ShortName = board.ShortName,
                Tasks = tasks
            };

            return View(boardViewModel);
        }

        // GET: Boards/Create
        public IActionResult Create()
        {
            ViewData["IdProject"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IdProject,ShortName")] BoardViewModel board)
        {
            if (ModelState.IsValid)
            {
                board.Id = Guid.NewGuid();
                _context.Add(new Board {
                    Id = board.Id,
                    Name = board.Name,
                    Description = board.Description,
                    DateCreate = DateTime.UtcNow,
                    IdProject = board.IdProject,
                    ShortName = board.ShortName
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProject"] = new SelectList(_context.Projects, "Id", "Name", board.IdProject);
            return View(board);
        }

        // GET: Boards/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            ViewData["IdProject"] = new SelectList(_context.Projects, "Id", "Name", board.IdProject);
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,IdProject,ShortName")] BoardViewModel board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Board
                    {
                        Id = board.Id,
                        Name = board.Name,
                        Description = board.Description,
                        IdProject = board.Id
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
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
            ViewData["IdProject"] = new SelectList(_context.Projects, "Id", "Id", board.IdProject);
            return View(board);
        }

        // GET: Boards/Delete/5

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
                .Include(b => b.IdProjectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board != null)
            {
                _context.Boards.Remove(board);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(Guid id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
    }
}
