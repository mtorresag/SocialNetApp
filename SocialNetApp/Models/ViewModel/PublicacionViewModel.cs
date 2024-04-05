using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialNetApp.Models.ViewModel

{
    public class PublicacionViewModel
    {
        public int PublicacionId { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int UsuarioId { get; set; }
        public int EstadoPublicacion { get; set; }
    }


    public class EditPublicacionViewModel
    {
        public int PublicacionId { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int UsuarioId { get; set; }
        public int EstadoPublicacion { get; set; }
    }


    public class PublicacionNamesViewModel
    {
        public int PublicacionId { get; set; }
        [Required]
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public int EstadoPublicacion { get; set; }
    }
}