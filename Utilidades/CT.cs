namespace TextToSpeechFrontEnd.Utilidades
{
    public static class CT
    {
        public static string UrlBaseApi = "https://localhost:44364/";// aca corresponde la ruta base, en donde iria el dominio raiz
        public static string RutaAudiosApi = UrlBaseApi + "api/Audios/";//Url Audios        
        public static string RutaUsuariosApi = UrlBaseApi + "api/Users/";//url usuarios
        //public static string RutaBuscarAudioApi = UrlBaseApi + "api/Audios/Buscar?nombre=";//url Buscar Audio por nombre
    }
}
// clase estatica para poder ser accedida de otras parted en donde tendre alojadas las rutas