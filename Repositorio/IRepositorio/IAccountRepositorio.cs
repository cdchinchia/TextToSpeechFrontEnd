using System.Threading.Tasks;
using TextToSpeechFrontEnd.Models;

namespace TextToSpeechFrontEnd.Repositorio.IRepositorio
{
    public interface IAccountRepositorio : IRepositorio<UsuarioM>
    {

        Task<UsuarioM> LoginAsync(string url, UsuarioM itemCrear);
        Task<bool> RegisterAsync(string url, UsuarioM itemCrear);
    }
}
//Interfaz de Repositorio para autenticación 