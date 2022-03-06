using System;
using System.ComponentModel.DataAnnotations;

namespace TextToSpeechFrontEnd.Models
{
    public class AudioInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(10, ErrorMessage = "El texto debe tener maximo 10 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La fecha de creacion es obligatoria")]
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "El Texto es obligatorio")]
        [StringLength(5000, ErrorMessage = "El texto debe tener maximo 5000 caracteres")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Expresar el lenguaje como una etiqueta de idioma BCP-47 es obligatorio")]
        [StringLength(5, ErrorMessage = "La etiqueta de idioma debe ser expresada por ejemplo Español(es-ES), Ingles(en-US)")]
        public string Language { get; set; }
        [Required(ErrorMessage = "El Genero es obligatorio: 0(UNSPECIFIED), 1(MALE), 2(fEMALE), 3(NEUTRAL)")]
        public Gender gender { get; set; }
        public string RutaAudio { get; set; }
        [Required(ErrorMessage = "La Descripción es obligatoria")]
        public string Descripcion { get; set; }
    }
    public enum Gender { SSML_VOICE_GENDER_UNSPECIFIED, MALE, FEMALE, NEUTRAL }
}
//mapeando los campos a traves del modelo