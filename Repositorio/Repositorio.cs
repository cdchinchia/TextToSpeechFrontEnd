using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechFrontEnd.Models;

namespace TextToSpeechFrontEnd.Repositorio.IRepositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        //inyeccion de dependencias se debe importar el IHttpClientFactory
        private readonly IHttpClientFactory _clientFactory;//IHttpClientFactory para poder utilizar HTTP en las llamadas
        //Contructor
        public Repositorio(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<bool> BorrarAsync(string url, int Id, string token = "")
        {
            var peticion = new HttpRequestMessage(HttpMethod.Delete, url + Id);// se concadena la url y el id. para ejecutar el delete


            //Se crea el cliente
            var cliente = _clientFactory.CreateClient();

            //Aquí valida token
            if (token != null && token.Length != 0)
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // se envia la peticion y queda guardada en respuesta
            HttpResponseMessage repuesta = await cliente.SendAsync(peticion); // el await hace parte de async
            //Validar si se actualizó la respuesta del servidor  y retorna boleano
            if (repuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }
        //Metodo Buscar Audio por nombre
        //public async Task<IEnumerable> BuscarAsync(string url, string nombre)
        //{
        //    var peticion = new HttpRequestMessage(HttpMethod.Get, url + nombre);// se concadena la url y el id. para obtener un elemento por id


        //    //Se crea el cliente
        //    var cliente = _clientFactory.CreateClient();
        //    // se envia la peticion y queda guardada en respuesta
        //    HttpResponseMessage repuesta = await cliente.SendAsync(peticion); // el await hace parte de async
        //    //Validar si se actualizó la respuesta del servidor  y retorna los datos
        //    if (repuesta.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var jsonString = await repuesta.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);//<IEnumerable> porque regresa una lista
        //    }
        //    else
        //        return null;
        //}

        //Metodo para crear metodo lleva parametro string y de modeloAudioInfo 
        public async Task<AudioInfo> CrearAsync(string url, AudioInfo itemCrear, string token = "")
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            //validacion que itemCrear no sea nulo, si no es nulo se crea el objeto y se envie el itemCrear
            if (itemCrear != null)
            {
                peticion.Content = new StringContent(
                    JsonConvert.SerializeObject(itemCrear), Encoding.UTF8, "application/json");

            }
            else
                //return false;
                return new AudioInfo();

            //Se crea el cliente
            var cliente = _clientFactory.CreateClient();

            //Aquí valida token
            if (token != null && token.Length != 0)
            {
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // se envia la peticion y queda guardada en respuesta
            HttpResponseMessage repuesta = await cliente.SendAsync(peticion);
            //Validar si se actualizó la respuesta del servidor  y retorna string
            if (repuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var jsonString = await repuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AudioInfo>(jsonString);
            }
                //return true;                
            else
                return new AudioInfo();
            //return false;
        }
        //Obteneres individual
        public async Task<T> GetAsync(string url, int Id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + Id);// se concadena la url y el id. para obtener un elemento por id


            //Se crea el cliente
            var cliente = _clientFactory.CreateClient();
            // se envia la peticion y queda guardada en respuesta
            HttpResponseMessage repuesta = await cliente.SendAsync(peticion); // el await hace parte de async
            //Validar si se actualizó la respuesta del servidor y retorna los datos
            if (repuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await repuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);//<T> regresa un solo generico
            }
            else
                return null;
        }
        //Obtener lista de todos
        public async Task<IEnumerable> GetTodoAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url);// se concadena la url y el id. para obtener un elemento por id


            //Se crea el cliente
            var cliente = _clientFactory.CreateClient();
            // se envia la peticion y queda guardada en respuesta
            HttpResponseMessage repuesta = await cliente.SendAsync(peticion); // el await hace parte de async
            //Validar si se actualizó la respuesta del servidor  y retorna los datos
            if (repuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await repuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);//<IEnumerable> porque regresa una lista
            }
            else
                return null;
        }

        //probando devolviendo ruta de audios
        //public async Task<T> GetUrlAudiosAsync(string url, string rutaAudio)
        //{
        //    var peticion = new HttpRequestMessage(HttpMethod.Get, url + rutaAudio);// se concadena la url y el id. para obtener un elemento por id


        //    //Se crea el cliente
        //    var cliente = _clientFactory.CreateClient();
        //    // se envia la peticion y queda guardada en respuesta
        //    HttpResponseMessage repuesta = await cliente.SendAsync(peticion); // el await hace parte de async
        //    //Validar si se actualizó la respuesta del servidor y retorna los datos
        //    if (repuesta.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var jsonString = await repuesta.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<T>(jsonString);//<T> regresa un solo generico
        //    }
        //    else
        //        return null;
        //}
    }
}
//Repositorio clase general que implementara los metodos. que seran llamados en los controladores
