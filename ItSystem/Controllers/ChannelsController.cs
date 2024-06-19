using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItSystem.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ItSystem.Controllers
{
    public class ChannelsController : Controller
    {

        public class ChannelContentViewModel
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public int UserCount { get; set; }

            public ICollection<Message> Messages { get; set; } = new List<Message>();
        }

        private readonly ItSystemContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChannelsController(ItSystemContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Channels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Channels.Where(x => x.IsDelete == false).ToListAsync());
        }

        // GET: Channels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels
                .FirstOrDefaultAsync(m => m.Id == id); 
            
            if (channel == null)
            {
                return NotFound();
            }

            var messages = _context.Messages
                .Include(x => x.IdTaskNavigation)
                .Include(x => x.IdUserNavigation)
                .Include(x => x.IdBranchMessageNavigation)
                .Where(message => message.IdChannel == id)
                .OrderBy(message => message.DateCreate)
                .ToList();

            var viewModel = new ChannelContentViewModel
            {
                Id = channel.Id,
                Name = channel.Name,
                Description = channel.Description,
                Messages = messages,
                UserCount = channel.UserCount
            };

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> SendComment(string Comment, Guid? id)
        {
            try
            {
                if (User.Identity.Name == null) return NotFound();

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user == null) return NotFound();

                _context.Messages.Add(new Message
                {
                    Id = Guid.NewGuid(),
                    Text = Comment,
                    DateCreate = DateTime.UtcNow,
                    IdChannel = id,
                    IdUser = Guid.Parse(user.Id),
                    IsDelete = false,
                    HasBranch = false,
                    HasTask = false,
                    HasProject = false,
                    IdBranchMessage = null,
                    IdTask = null,
                    IdChat = null
                });

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", new { id = id });
            }
        }

        // GET: Channels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Channel channel)
        {
            if (ModelState.IsValid)
            {
                channel.Id = Guid.NewGuid();
                channel.UserCount = 0;
                channel.DateCreate = DateTime.UtcNow;
                _context.Add(channel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(channel);
        }

        // GET: Channels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }
            return View(channel);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Channel channel)
        {
            if (id != channel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(channel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelExists(channel.Id))
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
            return View(channel);
        }

        // GET: Channels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            return View(channel);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel != null)
            {
                channel.IsDelete = true;
                _context.Channels.Update(channel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChannelExists(Guid id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }
    }
}
