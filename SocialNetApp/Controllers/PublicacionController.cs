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
    public class PublicacionController : Controller
    {
        // GET: Publicacion
        //public ActionResult Index()
        //{
        //    List<PublicacionesEditViewModel> lst = null;
        //    using (SOCIALEntities1 db = new SOCIALEntities1())
        //    {
        //        lst = (from d in db.Publicaciones
        //               select new PublicacionesEditViewModel
        //               {
        //                   PublicacionId = d.PublicacionId,
        //                   Contenido = d.Contenido,
        //                   FechaPublicacion = d.FechaHoraPublicacion,
        //                   UsuarioId = d.UsuarioId,
        //               }).ToList();
        //    }
        //    return View(lst);
        //}

        public ActionResult Index()
        {
            //List<PublicacionesEditViewModel> lst = null;
            List<PublicacionNamesViewModel> publicacionesAmigos = null;
            //List<Publicaciones> publicacionesAmigos = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                var amigos = (from a in db.Amistades
                              where (a.UsuarioAceptadoId == userid && a.EstadoAmistad == 2) || (a.UsuarioSolicitanteId == userid && a.EstadoAmistad == 2)
                              select new { UsuarioId = (a.UsuarioAceptadoId == userid ? a.UsuarioSolicitanteId : a.UsuarioAceptadoId) }).Distinct();

                publicacionesAmigos = (from d in db.Publicaciones
                                       join amigo in amigos on d.UsuarioId equals amigo.UsuarioId
                                       join u in db.Usuarios on d.UsuarioId equals u.UsuarioId
                                       where d.EstadoPublicacion == 1
                                       select new PublicacionNamesViewModel
                                       {
                           PublicacionId = d.PublicacionId,
                           Contenido = d.Contenido,
                           FechaPublicacion = d.FechaHoraPublicacion,
                           UsuarioId = d.UsuarioId,
                           NombreUsuario = u.NombreUsuario,
                           ApellidoUsuario = u.ApellidoUsuario
                           
                       }).Distinct().ToList();
            }
            return View(publicacionesAmigos);
        }


        public ActionResult MisPublicaciones()
        {
            List<PublicacionNamesViewModel> lst = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                lst = (from d in db.Publicaciones
                       join u in db.Usuarios on d.UsuarioId equals u.UsuarioId
                       where d.UsuarioId == userid &&d.EstadoPublicacion == 1
                       select new PublicacionNamesViewModel
                       {
                           PublicacionId = d.PublicacionId,
                           Contenido = d.Contenido,
                           FechaPublicacion = d.FechaHoraPublicacion,
                           UsuarioId = d.UsuarioId,
                           NombreUsuario = u.NombreUsuario,
                           ApellidoUsuario = u.ApellidoUsuario
                       }).ToList();
            }
            return View(lst);
        }


        [HttpGet]
        public ActionResult PublicacionAmigo(int Id)
        {
            List<PublicacionViewModel> lst = null;            
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                lst = (from d in db.Publicaciones
                       where d.UsuarioId == Id
                       select new PublicacionViewModel
                       {
                           PublicacionId = d.PublicacionId,
                           Contenido = d.Contenido,
                           FechaPublicacion = d.FechaHoraPublicacion,
                           UsuarioId = d.UsuarioId,
                       }).ToList();
            }
            //return View("~/Views/Publicacion/Index.cshtml", lst);
            return View(lst);
        }


        [HttpPost]
        public ActionResult Add(string ContenidoPublicacion)
        {
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            using (var db = new SOCIALEntities1())
            {
                Publicaciones oPublicacion = new Publicaciones();

                oPublicacion.Contenido = ContenidoPublicacion;
                oPublicacion.UsuarioId = userid;
                oPublicacion.EstadoPublicacion = 1;
                oPublicacion.FechaHoraPublicacion = DateTime.Now;


                db.Publicaciones.Add(oPublicacion);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Publicacion/MisPublicaciones"));
        }



        [HttpGet]
        public ActionResult DetallePublicacion(int Id)
        {
            ViewData["PublicacionId"] = Id;


            List<PublicacionNamesViewModel> lst = null;
            List <ComentarioNameViewModel> lstComment = null;
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                lst = (from d in db.Publicaciones
                       where d.PublicacionId == Id
                       join u in db.Usuarios on d.UsuarioId equals u.UsuarioId
                       select new PublicacionNamesViewModel
                       {
                           PublicacionId = d.PublicacionId,
                           Contenido = d.Contenido,
                           FechaPublicacion = d.FechaHoraPublicacion,
                           UsuarioId = d.UsuarioId,
                           NombreUsuario = u.NombreUsuario,
                           ApellidoUsuario = u.ApellidoUsuario
                       }).ToList();

                ViewData["UsuarioId"] = lst[0].UsuarioId;
                ViewData["Contenido"] = lst[0].Contenido;
                ViewData["FechaPublicacion"] = lst[0].FechaPublicacion;
                ViewData["NombreUsuario"] = lst[0].NombreUsuario;
                ViewData["ApellidoUsuario"] = lst[0].ApellidoUsuario;

                lstComment = (from d in db.Comentarios
                              where d.PublicacionId == Id
                              join u in db.Usuarios on d.UsuarioId equals u.UsuarioId
                              select new ComentarioNameViewModel
                              {
                                  Comentarioid = d.ComentarioId,
                                  Contenido = d.Contenido,
                                  FechaComentario = d.FechaHoraComentario,
                                  UsuarioId = d.UsuarioId,
                                  PublicacionId = d.PublicacionId,
                                  EstadoComentario = d.EstadoComentario,
                                  NombreUsuario = u.NombreUsuario,
                                  ApellidoUsuario = u.ApellidoUsuario

                              }).ToList();

            }
            //return View("~/Views/Publicacion/Index.cshtml", lst);
            return View(lstComment);
            //return Content("1");
        }


        [HttpPost]
        public ActionResult DetallePublicacion(int Id, int UsuarioId, string Contenido, DateTime FechaPublicacion)
        {
            ViewData["PublicacionId"] = Id;
            ViewData["UsuarioId"] = UsuarioId;
            ViewData["Contenido"] = Contenido;
            ViewData["FechaPublicacion"] = FechaPublicacion;

            List<PublicacionViewModel> lst = null;
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                lst = (from d in db.Publicaciones
                       where d.PublicacionId == Id
                       select new PublicacionViewModel
                       {
                           PublicacionId = d.PublicacionId,
                           Contenido = d.Contenido,
                           FechaPublicacion = d.FechaHoraPublicacion,
                           UsuarioId = d.UsuarioId,
                       }).ToList();
            }
            //return View("~/Views/Publicacion/Index.cshtml", lst);
            //return View(lst);
            //return Redirect(Url.Content("~/Publicacion/MisPublicaciones/"));
            return Content("1");

            //return Content();
        }


        public ActionResult ComentariosPublicacion(int PublicacionId)
        {
            EditComentarioViewModel model = new EditComentarioViewModel();
            using (var db = new SOCIALEntities1())
            {
                var oComentario = db.Comentarios.Find(PublicacionId);
                model.PublicacionId = oComentario.PublicacionId;
                model.Comentarioid = oComentario.ComentarioId;
                model.EstadoComentario = oComentario.EstadoComentario;
                model.Contenido = oComentario.Contenido;
                model.UsuarioId = oComentario.UsuarioId;
                model.FechaComentario = oComentario.FechaHoraComentario;
            }
            return View(model);
        }




        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new SOCIALEntities1())
            {
                var oUser = db.Usuarios.Find(Id);
                oUser.EstadoUsuario = 2; //ELIMINAR
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }


    }
}