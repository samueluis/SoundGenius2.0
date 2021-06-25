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
    public class MusicController : Controller
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

        /// <summary>
        /// recolher os dados de uma pessoa que está autenticada
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public MusicController(
                       SoundGeniusDB context,
                       IWebHostEnvironment caminho,
                       UserManager<IdentityUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: faixas
        /// <summary>
        /// Lista os dados dos faixas no 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Faixas.ToListAsync());
        }



        // GET: faixas/Details/5
        /// <summary>
        /// Mostra os detalhes de um faixas.
        /// Se houverem, mostra os detalhes das consultas associadas a ele
        /// Pesquisa feita em modo 'Lazy Loading'
        /// </summary>
        /// <param name="id">Identificador do Veterinário</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            // acesso aos dados em modo 'Lazy Loading'
            var faixa = await _context.Faixas.FirstOrDefaultAsync(v => v.ID == id);

            if (faixa == null)
            {
                return NotFound();
            }

            return View(faixa);
        }


        [Authorize(Roles = "Gerente")]
        public IActionResult Create()
        {


            return View();
        }



        // POST: faixa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ID,Titulo,Genero,FicheiroImg")] Faixas faixa, IFormFile fotoFaixa)
        {
            // var auxiliar
            string nomeImagem = "";

            if (fotoFaixa == null)
            {
                //não ha ficheiro
                ModelState.AddModelError("", "Adicione por favor a capa do video");
                ViewData["Id"] = new SelectList(_context.Albuns.OrderBy(v => v.Titulo), "id", "Titulo", faixa.Titulo);
                return View(faixa);
            }
            else
            {
                //ha ficheiro mas sera valido
                if (fotoFaixa.ContentType == "image/jpeg" || fotoFaixa.ContentType == "image/png")
                {

                    // definir o novo nome da fotografia
                    Guid g;
                    g = Guid.NewGuid();
                    nomeImagem = faixa.Titulo + "" + g.ToString(); // tb, poderia ser usado a formatação da data atual
                                                                   // determinar a extensão do nome da imagem
                    string extensao = Path.GetExtension(fotoFaixa.FileName).ToLower();
                    // agora, consigo ter o nome final do ficheiro
                    nomeImagem = nomeImagem + extensao;

                    // associar este ficheiro aos dados da Fotografia do cão
                    faixa.FicheiroImg = nomeImagem;

                    // localização do armazenamento da imagem
                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImagem = Path.Combine(localizacaoFicheiro, "Imagens\\Faixas", nomeImagem);
                }

                else
                {
                    //ficheiro não valido
                    ModelState.AddModelError("", "Apenas pode associar uma imagem a um video.");
                    return View(faixa);

                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //adicionar dados do novo video
                    _context.Add(faixa);
                    //
                    await _context.SaveChangesAsync();

                    //se cheguei, tudo correu bem
                    using var stream = new FileStream(nomeImagem, FileMode.Create);
                    await fotoFaixa.CopyToAsync(stream);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro...");
                }
            }
            return View(faixa);
        }

        // GET: faixa/Edit/5
        /// <summary>
        /// Edita os dados de um faixa
        ///   
        /// </summary>
        /// <param name="id">id do faixa</param>
        /// <returns></returns>
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //  return NotFound(); // este retorno é automático e deve ser personalizado
                return RedirectToAction("Index", "Home");
            }

            var faixas = await _context.Faixas.FindAsync(id);
            if (faixas == null)
            {
                return RedirectToAction("Index");
            }
            return View(faixas);
        }

        // POST: faixa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,Genero,FicheiroImg")] Faixas faixas)
        {
            if (id != faixas.ID)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faixas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaixasExists(faixas.ID))
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
            return View(faixas);
        }

        // GET: faixa/Delete/5
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faixas = await _context.Faixas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (faixas == null)
            {
                return NotFound();
            }

            return View(faixas);
        }

        // POST: faixa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faixas = await _context.Faixas.FindAsync(id);
            _context.Faixas.Remove(faixas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaixasExists(int id)
        {
            return _context.Faixas.Any(e => e.ID == id);
        }

    }
}
