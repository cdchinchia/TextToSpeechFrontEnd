using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TextToSpeechFrontEnd.Models;
using TextToSpeechFrontEnd.Repositorio.IRepositorio;
using TextToSpeechFrontEnd.Utilidades;

namespace TextToSpeechFrontEnd.Controllers
{
    public class TextToSpeechController : Controller
    {
        //instanciando Interfaz
        private readonly IAudioInfoRepositorio _repoAudioInfo;
        //COnstructor
        public TextToSpeechController(IAudioInfoRepositorio repoAudioInfo)
        {
            _repoAudioInfo = repoAudioInfo;
        }
        [HttpGet]
        public IActionResult Index()//se crea vista Index por defecto
        {
            return View(new AudioInfo() { });//se instancia el modelo AudioInfo
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAudios()//metodo que trae todos lo audios y se pasa en audios.js
        {
            return Json(new { data = await _repoAudioInfo.GetTodoAsync(CT.RutaAudiosApi) });// obtenemos los audios en Json. variable data en await accedo a _repoAudioInfo y llamo al metodo GetTodoAsync y le paso la ruta que se creo en CT(constantes)
        }

        //Metodo HTTPGET para mostrar formulario de crear un audio
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//validacion para solo recibir información del formulario
        public async Task<IActionResult> Create(AudioInfo audioInfo)
        {
            if (ModelState.IsValid)
            {
                await _repoAudioInfo.CrearAsync(CT.RutaAudiosApi, audioInfo, HttpContext.Session.GetString("JWToken"));
                return RedirectToAction("Index");
            }
            return View();
        }

        //Metodo borrar AudioInfo
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _repoAudioInfo.BorrarAsync(CT.RutaAudiosApi, id, HttpContext.Session.GetString("JWToken"));
            //Valida que exista un AudioInfo con el Id que se pasa como parametro
            if (status)
            {
                return Json(new { success = true, message = "Borrado correctamente" });
            }

            return Json(new { success = false, message = "No se pudo borrar" });
        }



        //probando traer la ruta 
        //[HttpGet]
        //public async Task<IActionResult> GetRutaAudios(string rutaAudio)//metodo que trae todos lo audios y se pasa en audios.js
        //{
        //    return Json(new { data = await _repoAudioInfo.GetUrlAudiosAsync(CT.RutaAudiosApi, rutaAudio) });// obtenemos los audios en Json. variable data en await accedo a _repoAudioInfo y llamo al metodo GetTodoAsync y le paso la ruta que se creo en CT(constantes)
        //}



    }
}
