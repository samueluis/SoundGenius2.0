using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SoundGenius.Data;
using SoundGenius.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace SoundGenius.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SoundGeniusDB db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            SoundGeniusDB context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            db = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "A {0} deve ter ao menos {2} e no máximo {1} caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme password")]
            [Compare("Password", ErrorMessage = "A password e a confirmação não corresponde.")]
            public string ConfirmPassword { get; set; }

            ///Pode ser interessante
            public Utilizadores Utilizadore { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = Input.Email,
                    Email = Input.Email
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var claim = new System.Security.Claims.Claim("Nome", Input.Utilizadore.Nome);
                    await _userManager.AddClaimAsync(user, claim);
                    await _userManager.AddToRoleAsync(user, "Utilizadore");
                    _logger.LogInformation("User created a new account with password.");

                    try
                    {
                        Utilizadores novoUtilizadore = new Utilizadores
                        {
                            Nome = Input.Utilizadore.Nome,
                            Email = Input.Email,
                            UserId = user.Id,
                            Telefone = Input.Utilizadore.Telefone,
                        };

                        db.Add(novoUtilizadore);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        await _userManager.RemoveFromRoleAsync(user, "Utilizadore");
                        await _userManager.RemoveClaimAsync(user, claim);
                        await _userManager.DeleteAsync(user);

                        return RedirectToAction("Index", "Home");
                    }

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
