using System.Net.Http;
using TextToSpeechFrontEnd.Models;
using TextToSpeechFrontEnd.Repositorio.IRepositorio;

namespace TextToSpeechFrontEnd.Repositorio
{
    public class UsuarioRepositorio : Repositorio<UsuarioU>, IUsuarioRepositorio
    {
        //Injección de dependencias se debe importar el IHttpClientFactory
        private readonly IHttpClientFactory _clientFactory;

        public UsuarioRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
