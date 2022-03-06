using System.Net.Http;
using TextToSpeechFrontEnd.Models;
using TextToSpeechFrontEnd.Repositorio.IRepositorio;

namespace TextToSpeechFrontEnd.Repositorio
{
    public class AudioInfoRepositorio : Repositorio<AudioInfo>, IAudioInfoRepositorio
    {
        //Injección de dependencias se debe importar el IHttpClientFactory
        private readonly IHttpClientFactory _clientFactory;

        public AudioInfoRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
