using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetApp.Models.ViewModel
{
    public class AmistadesViewModel
    {
        public int AmistadId { get; set; }
        public int UsuarioSolicitanteId { get; set; }
        public int UsuarioAcetadoId { get; set; }
        public int EstadoAmistad { get; set; }
    }
}