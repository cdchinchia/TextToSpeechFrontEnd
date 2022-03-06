using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TextToSpeechFrontEnd.Models.ViewModels
{
    public class AudioInfoVM
    {
        public IEnumerable<SelectListItem> ListaAudios { get; set; }
        public AudioInfo AudioInfo { get; set; }
    }
}
