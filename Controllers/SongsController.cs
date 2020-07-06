using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Songs
        public async Task<IActionResult> Index(string sortOrder)
        {
            //var applicationDbContext = _context.Songs.Include(s => s.SongCategory).Include(s => s.SongComposer);
            //return View(await applicationDbContext.ToListAsync());
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "song_length_desc" : "Date";
            var songs = from s in _context.Songs
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    songs = songs.OrderByDescending(s => s.SongName);
                    break;
                case "song_category":
                    songs = songs.OrderByDescending(s => s.SongCategory);
                    break;
                case "Date":
                    songs = songs.OrderBy(s => s.SongYear);
                    break;
                case "song_length_desc":
                    songs = songs.OrderByDescending(s => s.SongLength);
                    break;
                default:
                    songs = songs.OrderBy(s => s.SongId);
                    break;
            }
            return View(await songs.AsNoTracking().ToListAsync());
        }
        [AllowAnonymous]
        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.SongCategory)
                .Include(s => s.SongComposer)
                .Include(s=>s.SongPayments)
                    .ThenInclude(e=>e.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }
        [Authorize(Roles = "admin,composer")]
        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["ComposerId"] = new SelectList(_context.Composers, "ComposerId", "ComposerId");
            return View();
        }
        
        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin,composer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongId,SongName,SongLyrics,SongLength,SongYear,ComposerId,CategoryId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", song.CategoryId);
            ViewData["ComposerId"] = new SelectList(_context.Composers, "ComposerId", "ComposerId", song.ComposerId);
            return View(song);
        }
        [Authorize(Roles = "admin,composer")]
        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", song.CategoryId);
            ViewData["ComposerId"] = new SelectList(_context.Composers, "ComposerId", "ComposerId", song.ComposerId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin,composer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongId,SongName,SongLyrics,SongLength,SongYear,ComposerId,CategoryId")] Song song)
        {
            if (id != song.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", song.CategoryId);
            ViewData["ComposerId"] = new SelectList(_context.Composers, "ComposerId", "ComposerId", song.ComposerId);
            return View(song);
        }
        [Authorize(Roles = "admin,composer")]
        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.SongCategory)
                .Include(s => s.SongComposer)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin,composer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}
