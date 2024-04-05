using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetApp.Models;
using SocialNetApp.Models.TableViewModel;
using SocialNetApp.Models.ViewModel;

namespace SocialNetApp.Controllers
{
    public class ComentarioController : Controller
    {
        // GET: Comentario
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Add(string ContenidoComentario, string IdPublicacion)
        {
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            var PublicacionId = Convert.ToInt32(IdPublicacion);
            using (var db = new SOCIALEntities1())
            {
                Comentarios oComentario = new Comentarios();

                oComentario.Contenido = ContenidoComentario;
                oComentario.UsuarioId = userid;
                oComentario.PublicacionId = PublicacionId;
                oComentario.EstadoComentario = 1;
                oComentario.FechaHoraComentario = DateTime.Now;

                db.Comentarios.Add(oComentario);
                db.SaveChanges();
            }
            //return Redirect(Url.Content("~/Publicacion/DetallePublicacion"));

            return Redirect(Url.Content("~/Publicacion/DetallePublicacion") + "/" +PublicacionId);


        }
    }
}