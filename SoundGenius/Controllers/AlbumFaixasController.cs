using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoundGenius.Data;
using SoundGenius.Models;

namespace SoundGenius.Controllers
{
    [Authorize(Roles = "gerente")]
    public class AlbumFaixasController : Controller
    {
        private readonly SoundGeniusDB _context;

        public AlbumFaixasController(SoundGeniusDB context)
        {
            _context = context;
        }

        // GET: CreateMusic
        public async Task<IActionResult> Index()
        {
            var soundGeniusDB = _context.AlbumFaixas.Include(a => a.Album).Include(a => a.Faixa);
            return View(await soundGeniusDB.ToListAsync());
        }

        // GET: CreateMusic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumFaixas = await _context.AlbumFaixas
                .Include(a => a.Album)
                .Include(a => a.Faixa)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (albumFaixas == null)
            {
                return NotFound();
            }

            return View(albumFaixas);
        }

        // GET: CreateMusic/Create
        public IActionResult Create()
        {
            ViewData["AlbumFK"] = new SelectList(_context.Albuns, "ID", "Titulo");
            ViewData["FaixaFK"] = new SelectList(_context.Faixas, "ID", "Titulo");
            return View();
        }

        // POST: CreateMusic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AlbumFK,FaixaFK")] AlbumFaixas albumFaixas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(albumFaixas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumFK"] = new SelectList(_context.Albuns, "ID", "Titulo", albumFaixas.AlbumFK);
            ViewData["FaixaFK"] = new SelectList(_context.Faixas, "ID", "Titulo", albumFaixas.FaixaFK);
            return View(albumFaixas);
        }

       

        private bool AlbumFaixasExists(int id)
        {
            return _context.AlbumFaixas.Any(e => e.ID == id);
        }
    }
}
