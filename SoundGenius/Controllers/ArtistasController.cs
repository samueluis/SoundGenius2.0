using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoundGenius.Data;
using SoundGenius.Models;

namespace SoundGenius.Controllers

{
    [Authorize]
    public class ArtistasController : Controller
    {
        private readonly SoundGeniusDB db;
        private readonly IWebHostEnvironment _caminho;

        public ArtistasController(SoundGeniusDB context, IWebHostEnvironment caminho)
        {
            db = context;
            _caminho = caminho;
        }

        // GET: Faixas
        public async Task<IActionResult> Index()
        {
            // em SQL, db.music.ToListAsync() sigGeneroica:
            // SELECT * FROM Faixas

            return View(await db.Artista.ToListAsync());
        }




        // GET: Faixas/Details/5
        /// <summary>
        /// Mostra os detalhes de um Dono
        /// </summary>
        /// <param name="id">identificador do Dono a detalhar</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // em SQL, db.Faixas.FirstOrDefaultAsync(m => m.ID == id) sigGeneroica
            // SELECT * FROM Faixas d WHERE d.ID = id
            var artista = await db.Artista.FirstOrDefaultAsync(d => d.ID == id);
            if (artista == null)
            {
                return RedirectToAction("Index");
            }

            return View(artista);
        }


        // GET: Faixas/Add
        [Authorize(Roles = "Gerente")]
        public IActionResult Create()
        {
            return View();
        }



        // POST: Faixas/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Sexo,FicheiroImg")]Artista artista, IFormFile fotoArtista)
        {
            // var auxiliar
            string nomeImagem = "";

            if (fotoArtista == null)
            {
                //não ha ficheiro
                ModelState.AddModelError("", "Adicione por favor a capa do video");
                ViewData["Id"] = new SelectList(db.Artista.OrderBy(v => v.Nome), "id", "Nome", artista.Nome);
                return View(artista);
            }
            else
            {
                //ha ficheiro mas sera valido
                if (fotoArtista.ContentType == "image/jpeg" || fotoArtista.ContentType == "image/png")
                {

                    // definir o novo nome da fotografia
                    Guid g;
                    g = Guid.NewGuid();
                    nomeImagem = artista.Nome + "" + g.ToString(); // tb, poderia ser usado a formatação da data atual
                                                                     // determinar a extensão do nome da imagem
                    string extensao = Path.GetExtension(fotoArtista.FileName).ToLower();
                    // agora, consigo ter o nome final do ficheiro
                    nomeImagem = nomeImagem + extensao;

                    // associar este ficheiro aos dados da Fotografia do cão
                    artista.FicheiroImg = nomeImagem;

                    // localização do armazenamento da imagem
                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImagem = Path.Combine(localizacaoFicheiro, "Imagens\\Artistas", nomeImagem);
                }

                else
                {
                    //ficheiro não valido
                    ModelState.AddModelError("", "Apenas pode associar uma imagem a um video.");
                    return View(artista);

                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //adicionar dados do novo video
                    db.Add(artista);
                    //
                    await db.SaveChangesAsync();

                    //se cheguei, tudo correu bem
                    using var stream = new FileStream(nomeImagem, FileMode.Create);
                    await fotoArtista.CopyToAsync(stream);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro...");
                }
            }

            // alguma coisa correu mal.
            // devolve-se o controlo da aplicação à View
            return View(artista);
        }




        // GET: Faixas/Edit/5
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var artista = await db.Artista.FindAsync(id);
            if (artista == null)
            {
                return RedirectToAction("Index");
            }
            return View(artista);
        }


        // POST: Faixas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Sexo,FicheiroImg")] Artista artistas)
        {
            if (id != artistas.ID)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(artistas);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistasExists(artistas.ID))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artistas);
        }



        // GET: Faixas/Delete/5
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var artista = await db.Artista
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artista == null)
            {
                return RedirectToAction("Index");
            }

            return View(artista);
        }



        // POST: Faixas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistas = await db.Artista.FindAsync(id);
            db.Artista.Remove(artistas);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool ArtistasExists(int id)
        {
            return db.Artista.Any(e => e.ID == id);
        }
    }
}
