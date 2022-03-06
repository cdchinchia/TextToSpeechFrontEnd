using System.Collections;
using System.Threading.Tasks;
using TextToSpeechFrontEnd.Models;

namespace TextToSpeechFrontEnd.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class// clase generica<T>
    {
        //Metodos
        Task<IEnumerable> GetTodoAsync(string url);//Obtener una lista
        Task<T> GetAsync(string url, int Id);//Obtener un elemento individual
        Task<AudioInfo> CrearAsync(string url, AudioInfo itemCrear, string token); //crear un recurso, metodo protegido    
        Task<bool> BorrarAsync(string url, int Id, string token);//Borrar un recurso, metodo protegido

        //Task<IEnumerable> BuscarAsync(string url, string nombre);//Obtener Audio por nombre
        //Task<T> GetUrlAudiosAsync(string url, string rutaAudio);

    }
}
//IRepositorio interfaz general que implementara los metodos. que seran llamados en los controladores
// con Task<> se accedera a cualquier de las listas sea de AudioInfo o de Users