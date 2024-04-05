using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetApp.Models.TableViewModel
{
    public class PublicacionesEditViewModel
    {
        public int PublicacionId { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int UsuarioId { get; set; }
    }
}