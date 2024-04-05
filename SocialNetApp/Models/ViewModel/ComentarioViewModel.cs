using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetApp.Models.ViewModel
{
    public class ComentarioViewModel
    {
        public int Comentarioid { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; }
        public int UsuarioId { get; set; }
        public int PublicacionId { get; set; }

        public int EstadoComentario { get; set; }
    }


    public class EditComentarioViewModel
    {
        public int Comentarioid { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; }
        public int UsuarioId { get; set; }
        public int PublicacionId { get; set; }

        public int EstadoComentario { get; set; }
    }



    public class ComentarioNameViewModel
    {
        public int Comentarioid { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public int PublicacionId { get; set; }

        public int EstadoComentario { get; set; }
    }


}