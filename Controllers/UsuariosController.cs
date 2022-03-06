using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TextToSpeechFrontEnd.Models;
using TextToSpeechFrontEnd.Repositorio.IRepositorio;
using TextToSpeechFrontEnd.Utilidades;

namespace TextToSpeechFrontEnd.Controllers
{
    public class UsuariosController : Controller
    {
        //instanciando Interfaz
        private readonly IUsuarioRepositorio _repoUsuario;
        //COnstructor
        public UsuariosController(IUsuarioRepositorio repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        public IActionResult Index()
        {
            return View(new UsuarioU() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosusuarios()//metodo que trae todos lo audios y se pasa en usuarios.js
        {
            return Json(new { data = await _repoUsuario.GetTodoAsync(CT.RutaUsuariosApi) });// obtenemos los usuarios en Json. variable data en await accedo a _repoUsuario y llamo al metodo GetTodoAsync y le paso la ruta que se creo en CT(constantes)
        }
    }
}
