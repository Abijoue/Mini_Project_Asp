using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using MiniProjet_Core.Model;
using MiniProjet_Core.Data;

namespace MiniProjet_Core.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
    //    private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
           // _emailSender = emailSender;
            _roleManager = roleManager ;
            _db = db ;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Nom { get; set; }
            [Required]
            public string Prenom { get; set; }
            [Required]
            public string CIN { get; set; }

            [DataType(DataType.Date)]
            [Display (Name = "Date de naissance" )]
            public string Date { get; set; }

            [Required]
            public char Status{ get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Utilisateur { 
                    UserName = Input.Email,
                    Email = Input.Email ,
                    Nom = Input.Nom,
                    Prenom = Input.Prenom,
                    CIN = Input.CIN,
                    Date = Input.Date,
                    Status = Input.Status,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                _logger.LogInformation("User created a new account with password : "+user.Status+" --- ");

                if (result.Succeeded)
                {
                    if(!await _roleManager.RoleExistsAsync(GestionRole.AdminUser)){
                        await _roleManager.CreateAsync(new IdentityRole(GestionRole.AdminUser));
                    }
                    if(!await _roleManager.RoleExistsAsync(GestionRole.Professeur)){
                        await _roleManager.CreateAsync(new IdentityRole(GestionRole.Professeur));
                    }
                    if(!await _roleManager.RoleExistsAsync(GestionRole.Terminal)){
                        await _roleManager.CreateAsync(new IdentityRole(GestionRole.Terminal));
                    }

                    if(user.Status == 'A'){
                        await _userManager.AddToRoleAsync(user,GestionRole.AdminUser);
                    }
                    if(user.Status == 'P'){
                        await _userManager.AddToRoleAsync(user,GestionRole.Professeur);
                    }
                    if(user.Status == 'T'){
                        await _userManager.AddToRoleAsync(user,GestionRole.Terminal);
                    }

/*
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {*/
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                //    }
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
