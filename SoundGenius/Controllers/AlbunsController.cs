using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoundGenius.Data;
using SoundGenius.Models;

namespace SoundGenius.Controllers
{

    [Authorize] // todos os métodos desta classe ficam protegidos. Só pessoas AUTORIZADAS têm acesso.
    public class AlbunsController : Controller
    {

        /// <summary>
        /// variável que identifica a BD do nosso projeto
        /// </summary>
        private readonly SoundGeniusDB _context;

        /// <summary>
        /// variável que contém os dados do 'ambiente' do servidor. 
        /// Em particular, onde estão os ficheiros guardados, no disco rígido do servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        public AlbunsController(
                     SoundGeniusDB context,
                     IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;
            
        }


        // GET: Albuns
        /// <summary>
        /// Lista os dados dos Albuns no Ecrã
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albuns.ToListAsync());
        }



        // GET: Albuns/Details/5
        /// <summary>
        /// Mostra os detalhes de um Albuns.
        /// Se houverem, mostra os detalhes das consultas associadas a ele
        /// Pesquisa feita em modo 'Lazy Loading'
        /// </summary>
        /// <param name="id">Identificador do Albuns</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            // acesso aos dados em modo 'Lazy Loading'
            var album = await _context.Albuns.FirstOrDefaultAsync(v => v.ID == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }




        // GET: Albuns/Create
        // este método deixa de ser necessário, pq há uma proteção 'de classe'

        //[Authorize(Roles = "Gerente")]  // apenas um utilizador autenticado e que pertença a este role, pode aceder ao conteúdo


        public IActionResult Create()
        {
            ViewData["Artista"] = new SelectList(_context.Artista.OrderBy(v => v.Nome), "ID", "Nome");
            return View();
        }



        // POST: Albuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ID,Titulo,Genero,Data,ArtistaFK,FicheiroImg")] Albuns album, IFormFile fotoAlbum)
        {
            // var auxiliar
            string nomeImagem = "";

            if (fotoAlbum == null)
            {
                //não ha ficheiro
                ModelState.AddModelError("", "Adicione por favor a capa do video");
                ViewData["Id"] = new SelectList(_context.Albuns.OrderBy(v => v.Titulo), "id", "Titulo", album.Titulo);
                return View(album);
            }
            else
            {
                //ha ficheiro mas sera valido
                if (fotoAlbum.ContentType == "image/jpeg" || fotoAlbum.ContentType == "image/png")
                {

                    // definir o novo nome da fotografia
                    Guid g;
                    g = Guid.NewGuid();
                    nomeImagem = album.Titulo + "" + g.ToString(); // tb, poderia ser usado a formatação da data atual
                                                                   // determinar a extensão do nome da imagem
                    string extensao = Path.GetExtension(fotoAlbum.FileName).ToLower();
                    // agora, consigo ter o nome final do ficheiro
                    nomeImagem = nomeImagem + extensao;

                    // associar este ficheiro aos dados da Fotografia do cão
                    album.FicheiroImg = nomeImagem;

                    // localização do armazenamento da imagem
                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImagem = Path.Combine(localizacaoFicheiro, "Imagens\\Albuns", nomeImagem);
                }

                else
                {
                    //ficheiro não valido
                    ModelState.AddModelError("", "Apenas pode associar uma imagem a um video.");
                    return View(album);

                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //adicionar dados do novo video
                    _context.Add(album);
                    //
                    await _context.SaveChangesAsync();

                    //se cheguei, tudo correu bem
                    using var stream = new FileStream(nomeImagem, FileMode.Create);
                    await fotoAlbum.CopyToAsync(stream);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro...");
                }
            }
            ViewData["Artista"] = new SelectList(_context.Artista.OrderBy(v => v.Nome), "ID", "Nome", album.ArtistaFK);
            return View(album);
        }

        // GET: Albuns/Edit/5
        /// <summary>
        /// Edita os dados de um faixa
        ///   
        /// </summary>
        /// <param name="id">id do faixa</param>
        /// <returns></returns>
        //[Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //  return NotFound(); // este retorno é automático e deve ser personalizado
                return RedirectToAction("Index", "Home");
            }

            var album = await _context.Albuns.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }



            return View(album);
        }

        // POST: Albuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,NumCedulaProf,Fotografia")] Albuns albuns)
        {
            if (id != albuns.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(albuns);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbunsExists(albuns.ID))
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
            return View(albuns);
        }

        //[Authorize(Roles = "Gerente")]
        // GET: Albuns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albuns = await _context.Albuns
                .FirstOrDefaultAsync(m => m.ID == id);
            if (albuns == null)
            {
                return NotFound();
            }

            return View(albuns);
        }

        // POST: Albuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albuns = await _context.Albuns.FindAsync(id);
            _context.Albuns.Remove(albuns);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbunsExists(int id)
        {
            return _context.Albuns.Any(e => e.ID == id);
        }
    }
}
