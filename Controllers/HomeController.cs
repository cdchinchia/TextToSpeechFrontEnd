using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TextToSpeechFrontEnd.Models;
using TextToSpeechFrontEnd.Repositorio.IRepositorio;
using TextToSpeechFrontEnd.Utilidades;

namespace TextToSpeechFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //instanciando Interfaz
        private readonly IAccountRepositorio _accRepo;
        //Constructor

        public HomeController(ILogger<HomeController> logger, IAccountRepositorio accRepo)
        {
            _logger = logger;
            _accRepo = accRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Metodo HttpGetque muestra el formulario Login
        [HttpGet]
        public IActionResult Login()
        {
            UsuarioM usuario = new UsuarioM();//se crea objeto de la clase UsuaioM
            return View(usuario);
        }
        //Metodo HttpPost del login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioM obj)
        {
            if (ModelState.IsValid)
            {
                UsuarioM objUser = await _accRepo.LoginAsync(CT.RutaUsuariosApi + "Login", obj);//Accedo al metodo LoginAsync de _accRepo y le paso la ruta concadenando Login(parte de la ruta) y se pasa el obj tipo UsuarioM que se esta pasando como parametro
                if (objUser.Token == null)//Validacion del usuario
                {
                    TempData["alert"] = "Los datos son incorrectos";//si no se genera el token envia el mensaje 
                    return View();//Retorna la vista, vuelve y muestra el formulario
                }

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimValueTypes.String, ClaimTypes.Name, obj.Usuario));//guardando el nombre de usuario y el token en el navegador
                //identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Usuario));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("JWToken", objUser.Token);//crea la session
                TempData["alert"] = "Bienvenido/a " + objUser.Usuario;//si se genero correctamete envia mensaje de bienbenido concadenado con el usuario
                return RedirectToAction("Index");//una vez la autenticacion se generó. redirecciona a la vista index. puede redireccionar a la pagina que se desee
            }
            //si el objeto no es valido retorna al mismo formulario
            else
            {
                return View();
            }
        }

        //Metodo HttpGetque muestra el formulario Registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
        //Metodo HttpPost del Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(UsuarioM obj)
        {
            bool result = await _accRepo.RegisterAsync(CT.RutaUsuariosApi + "Register", obj);//Accedo al metodo RegisterAsync de _accRepo y le paso la ruta concadenando Registro(parte de la ruta) y se pasa el obj tipo UsuarioM que se esta pasando como parametro
            if (result == false)//Validacion
            {
                return View();
            }

            TempData["alert"] = "Registro correcto";//si se genero correctamete envia mensaje de registro correcto
            return RedirectToAction("Login");//Redirecciona al Login
        }

        //Logout 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
