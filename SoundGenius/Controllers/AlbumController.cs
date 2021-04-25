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
    public class AlbumController : Controller
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
        private readonly UserManager<ApplicationUser> _userManager;

        public AlbumController(
                       SoundGeniusDB context,
                       IWebHostEnvironment caminho,
                       UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }





        // GET: faixas
        /// <summary>
        /// Lista os dados dos faixas no Ecrã
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albuns.ToListAsync());
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
            var album = await _context.Albuns.FirstOrDefaultAsync(v => v.ID == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }




        // GET: faixa/Create
        //     [Authorize] // anotador que força a Autenticação para dar acesso ao recurso
        // este método deixa de ser necessário, pq há uma proteção 'de classe'

        [Authorize]  // apenas um utilizador autenticado e que pertença a este role, pode aceder ao conteúdo


        public IActionResult Create()
        {
            return View();
        }



        // POST: faixa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ID,Titulo,Genero,FicheiroImg")] Albuns Album, IFormFile fotoFaixa)
        {
            // **************************************
            // processar a fotografia
            // **************************************
            // vars. auxiliares
            string caminhoCompleto = "";
            bool haImagem = false;

            // será q há fotografia?
            //    - uma hipótese possível, seria reenviar os dados para a View e solicitar a adição da imagem
            //    - outra hipótese, será associar ao faixa uma fotografia 'por defeito'
            if (fotoFaixa == null) { Album.FicheiroImg = "No Album.png"; }
            else
            {
                // há ficheiro
                // será o ficheiro uma imagem?
                if (fotoFaixa.ContentType == "image/jpeg" ||
                    fotoFaixa.ContentType == "image/png")
                {
                    // o ficheiro é uma imagem válida
                    // preparar a imagem para ser guardada no disco rígido
                    // e o seu nome associado ao faixa
                    Guid g;
                    g = Guid.NewGuid();
                    string extensao = Path.GetExtension(fotoFaixa.FileName).ToLower();
                    string nome = g.ToString() + extensao;
                    // onde guardar o ficheiro
                    caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens\\Faixas", nome);
                    // associar o nome do ficheiro ao faixa
                    Album.FicheiroImg = nome;
                    // assinalar que existe imagem e é preciso guardá-la no disco rígido
                    haImagem = true;
                }
                else
                {
                    // há imagem, mas não é do tipo correto
                    Album.FicheiroImg = "NoFaixa.png";
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(Album);
                    await _context.SaveChangesAsync();
                    // se há imagem, vou guardá-la no disco rígido
                    if (haImagem)
                    {
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await fotoFaixa.CopyToAsync(stream);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    // se chegar aqui, é pq alguma coisa correu mesmo mal...
                    // o que fazer?
                    // opções a realizar (todas, ou apenas uma...):
                    //   - escrever, no disco do servidor, um log com o erro
                    //   - escrever numa tabela de Erros, na BD, o log do erro
                    //   - enviar o modelo de volta para a View
                    //   - se o erro for corrigível, corrigir o erro
                }
            }
            return View(Album);
        }

        // GET: faixa/Edit/5
        /// <summary>
        /// Edita os dados de um faixa
        ///   
        /// </summary>
        /// <param name="id">id do faixa</param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //  return NotFound(); // este retorno é automático e deve ser personalizado
                return RedirectToAction("Index", "Home");
            }

            var Album = await _context.Albuns.FindAsync(id);
            if (Album == null)
            {
                return NotFound();
            }



            return View(Album);
        }

        // POST: faixa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,NumCedulaProf,Fotografia")] Albuns Album)
        {
            if (id != Album.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaixasExists(Album.ID))
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
            return View(Album);
        }

        // GET: faixa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Album = await _context.Albuns
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Album == null)
            {
                return NotFound();
            }

            return View(Album);
        }

        // POST: faixa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Album = await _context.Albuns.FindAsync(id);
            _context.Albuns.Remove(Album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaixasExists(int id)
        {
            return _context.Albuns.Any(e => e.ID == id);
        }
    }
}
