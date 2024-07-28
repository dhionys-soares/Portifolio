using DesafioIbge.Views.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIbge.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Senha, false, false);
                return (!result.Succeeded) ? BadRequest("Não foi possível realizar o login.") : RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao realizar login.");
            }            
        }
        [HttpPost]
        public async Task<IActionResult> LoginWithDefaultData(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Senha, false, false);
                return (!result.Succeeded) ? BadRequest("Não foi possível realizar o login.") : RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao realizar login.");
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                var result = _signInManager.SignOutAsync();
                return (!result.IsCompletedSuccessfully) ? BadRequest("Não foi possível realizar a operação") : RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao realizar logout.");
            }
            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                Guid guid = Guid.NewGuid();
                var usuario = new IdentityUser();

                if (model.Name.Contains(" "))
                {
                    var fullName = model.Name;
                    int espaco = fullName.IndexOf(' ');
                    string name = fullName.Substring(0, espaco);

                    usuario.UserName = name;
                    usuario.Email = model.Email;
                    usuario.PasswordHash = model.Senha;
                    usuario.Id = guid.ToString();
                }
                else
                {
                    usuario.UserName = model.Name;
                    usuario.Email = model.Email;
                    usuario.PasswordHash = model.Senha;
                    usuario.Id = guid.ToString();
                }                

                var result = await _userManager.CreateAsync(usuario, model.Senha);
                var resultRole = await _userManager.AddToRoleAsync(usuario, "User");

                if (result.Succeeded && resultRole.Succeeded)
                {
                    await _signInManager.SignInAsync(usuario, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest("Não foi possível criar o usuário.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar usuário.");
            }

            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(UsuarioViewModel model)
        {
            if (model.Email == null || model.Role == null)
                return BadRequest("Por favor, informe email e perfil para prosseguir.");

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return NotFound("Usuário não encontrado.");

                var roleExist = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                var result = await _userManager.AddToRoleAsync(user, model.Role);

                return (!result.Succeeded) ? StatusCode(500, "Erro ao tentar completar o processo.") : RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao adicionar perfil.");
            }
            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(UsuarioViewModel model)
        {
            if (model.Email == null || model.Role == null)
                return BadRequest("Por favor, informe email e perfil para prosseguir.");

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return NotFound("Usuário não encontrado.");

                var roleExist = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleExist)
                    return NotFound("Perfil não encontrado.");

                var result = await _userManager.RemoveFromRoleAsync(user, model.Role);

                return (!result.Succeeded) ? StatusCode(500, "Erro ao tentar completar o processo.") : RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao remover perfil.");
            }            
        }
    }
}