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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<UserTableViewModel> lst = null;
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                lst = (from d in db.Usuarios
                       where d.EstadoUsuario==1
                       select new UserTableViewModel
                       {
                           Id = d.UsuarioId,
                           Nombre = d.NombreUsuario,
                           Apellido = d.ApellidoUsuario,
                           Correo = d.CorreoElectronico,
                           Fecha_Nacimiento = d.FechaNacimiento
                       }).ToList();
            }
                return View(lst);
        }


        [HttpGet]
        public ActionResult Perfil()
        {
            EditUserViewModel model = new EditUserViewModel();
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                var userid = Convert.ToInt32(Session["UserId"].ToString());
                var oUser = db.Usuarios.Find(userid);

                model.Id = oUser.UsuarioId;
                model.Nombre = oUser.NombreUsuario;
                model.Apellido = oUser.ApellidoUsuario;
                model.Email = oUser.CorreoElectronico;
                model.Fecha_Nacimiento = oUser.FechaNacimiento;               
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult PerfilAmigo(int Id)
        {
            EditUserViewModel model = new EditUserViewModel();
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                var oUser = db.Usuarios.Find(Id);

                model.Id = oUser.UsuarioId;
                model.Nombre = oUser.NombreUsuario;
                model.Apellido = oUser.ApellidoUsuario;
                model.Email = oUser.CorreoElectronico;
                model.Fecha_Nacimiento = oUser.FechaNacimiento;
            }
            //return View("~/Views/Usuario/Perfil.cshtml",  model);
            return View(model);
        }



        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Add(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new SOCIALEntities1())
            {
                Usuarios oUser = new Usuarios();
                oUser.NombreUsuario = model.Nombre;
                oUser.ApellidoUsuario = model.Apellido;
                oUser.CorreoElectronico = model.Email;
                oUser.Password = model.Password;
                oUser.FechaNacimiento = model.Fecha_Nacimiento;
                oUser.EstadoUsuario = 1;

                db.Usuarios.Add(oUser);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
            //return Redirect(Url.Content("~/Usuario/"));
        }


        [HttpPost]
        public ActionResult AddLogin(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Acceso/Login.cshtml", model);
            }
            using (var db = new SOCIALEntities1())
            {
                Usuarios oUser = new Usuarios();
                oUser.NombreUsuario = model.Nombre;
                oUser.ApellidoUsuario = model.Apellido;
                oUser.CorreoElectronico = model.Email;
                oUser.Password = model.Password;
                oUser.FechaNacimiento = model.Fecha_Nacimiento;
                oUser.EstadoUsuario = 1;

                db.Usuarios.Add(oUser);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
            //return Redirect(Url.Content("~/Usuario/"));
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditUserViewModel model = new EditUserViewModel();
            using (var db = new SOCIALEntities1())
            {
                var oUser = db.Usuarios.Find(Id);
                model.Id = oUser.UsuarioId;
                model.Nombre = oUser.NombreUsuario;
                model.Apellido = oUser.ApellidoUsuario;
                model.Email = oUser.CorreoElectronico;
                model.Fecha_Nacimiento = oUser.FechaNacimiento;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new SOCIALEntities1())
            {
                var oUser = db.Usuarios.Find(model.Id);
                oUser.NombreUsuario = model.Nombre;
                oUser.ApellidoUsuario = model.Apellido;
                oUser.CorreoElectronico = model.Email;
                oUser.FechaNacimiento = model.Fecha_Nacimiento;

                if(model.Password != null && model.Password.Trim() != "")
                {
                    oUser.Password = model.Password;
                }
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Usuario/"));
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

        [HttpGet]
        public ActionResult Amistad()
        {
            List<UserTableViewModel> lst = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                lst = (from a in db.Amistades
                       join u1 in db.Usuarios on a.UsuarioSolicitanteId equals u1.UsuarioId
                       join u2 in db.Usuarios on a.UsuarioAceptadoId equals u2.UsuarioId
                       where a.EstadoAmistad == 2 && // Estado de amistad Aceptada
                             (u1.UsuarioId == userid || u2.UsuarioId == userid) && // ID del usuario que deseas consultar
                             u1.EstadoUsuario == 1 && // Usuario solicitante activo
                             u2.EstadoUsuario == 1 // Usuario aceptado activo
                       select new UserTableViewModel
                       {
                           Id = u2.UsuarioId,
                           Nombre = u2.NombreUsuario,
                           Apellido = u2.ApellidoUsuario,
                           Correo = u2.CorreoElectronico,
                           Fecha_Nacimiento = u2.FechaNacimiento
                       }).ToList();
            }
            return View(lst);
        }


        [HttpGet]
        public ActionResult SolicitudAmistad()
        {
            List<UserTableViewModel> usuariosConSolicitudes = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());

            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                usuariosConSolicitudes = (from u in db.Usuarios
                                          where db.Amistades.Any(a => a.UsuarioSolicitanteId == u.UsuarioId && a.UsuarioAceptadoId == userid && a.EstadoAmistad == 1) // Cambiar a 1 si el estado "Pendiente" es representado por este valor
                                          select new UserTableViewModel
                                          {
                                              Id = u.UsuarioId,
                                              Nombre = u.NombreUsuario,
                                              Apellido = u.ApellidoUsuario,
                                              Correo = u.CorreoElectronico,
                                              Fecha_Nacimiento = u.FechaNacimiento
                                          }).ToList();
            }
            return View(usuariosConSolicitudes);
        }


        [HttpGet]
        public ActionResult AmistadEnviada()
        {
            List<UserTableViewModel> usuariosConSolicitudes = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());

            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                usuariosConSolicitudes = (from u in db.Usuarios
                                          where db.Amistades.Any(a => a.UsuarioSolicitanteId == userid && a.UsuarioAceptadoId ==  u.UsuarioId && a.EstadoAmistad == 1) // Cambiar a 1 si el estado "Pendiente" es representado por este valor
                                          select new UserTableViewModel
                                          {
                                              Id = u.UsuarioId,
                                              Nombre = u.NombreUsuario,
                                              Apellido = u.ApellidoUsuario,
                                              Correo = u.CorreoElectronico,
                                              Fecha_Nacimiento = u.FechaNacimiento
                                          }).ToList();
            }
            return View(usuariosConSolicitudes);
        }



        public ActionResult Usuarios()
        {
            List<UserTableViewModel> lst = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            using (SOCIALEntities1 db = new SOCIALEntities1())
            {
                var amigos = (from a in db.Amistades
                              where (a.UsuarioAceptadoId == userid && a.EstadoAmistad == 2) || (a.UsuarioSolicitanteId == userid && a.EstadoAmistad == 2)
                              select new { UsuarioId = (a.UsuarioAceptadoId == userid ? a.UsuarioSolicitanteId : a.UsuarioAceptadoId) }).Distinct();

                lst = (from u in db.Usuarios
                                    where !amigos.Any(amigo => amigo.UsuarioId == u.UsuarioId) && u.UsuarioId != userid && u.EstadoUsuario == 1
                                    select new UserTableViewModel
                                    {
                                        Id = u.UsuarioId,
                                        Nombre = u.NombreUsuario,
                                        Apellido = u.ApellidoUsuario,
                                        Correo = u.CorreoElectronico,
                                        Fecha_Nacimiento = u.FechaNacimiento
                                    }).ToList();
            }
            return View(lst);
        }


        [HttpPost]
        public ActionResult AddAmistad(int Id)
        {
            var userid = Convert.ToInt32(Session["UserId"].ToString());
            bool tieneAmistad = false;
            using (var db = new SOCIALEntities1())
            {
                tieneAmistad = db.Amistades.Any(a =>
                    ((a.UsuarioSolicitanteId == userid && a.UsuarioAceptadoId == Id) ||
                    (a.UsuarioSolicitanteId == Id && a.UsuarioAceptadoId == userid)) &&
                    a.EstadoAmistad == 1); // Cambiar a 2 si el estado "Aceptada" es representado por este valor

                if (tieneAmistad)
                {
                    return Content("1");
                } 
                else
                {
                    Amistades oAmistad = new Amistades();
                    oAmistad.UsuarioSolicitanteId = userid;
                    oAmistad.UsuarioAceptadoId = Id;
                    oAmistad.EstadoAmistad = 1;

                    db.Amistades.Add(oAmistad);
                    db.SaveChanges();

                    return Content("2");
                }
            }
        }


        [HttpPost]
        public ActionResult DeleteSolicAmistadEnviada(int Id)
        {
            //var solicitudEnviada = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());

            using (var db = new SOCIALEntities1())
            {
                var oSolicitudEnviada = (from a in db.Amistades
                                    where a.UsuarioSolicitanteId == userid &&
                                          a.UsuarioAceptadoId == Id
                                    select a).FirstOrDefault() ;

                db.Amistades.Remove(oSolicitudEnviada);
                db.SaveChanges();
            }
            return Content("1");
        }



        public ActionResult RechazarSolicAmistadRecibida(int Id)
        {
            //var solicitudEnviada = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());

            using (var db = new SOCIALEntities1())
            {
                var oSolicitudEnviada = (from a in db.Amistades
                                         where a.UsuarioSolicitanteId == Id &&
                                               a.UsuarioAceptadoId == userid
                                         select a).FirstOrDefault();

                oSolicitudEnviada.EstadoAmistad = 3; //RECHAZAR
                db.Entry(oSolicitudEnviada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            return Content("1");
        }


        public ActionResult AceptarSolicAmistadRecibida(int Id)
        {
            //var solicitudEnviada = null;
            var userid = Convert.ToInt32(Session["UserId"].ToString());

            using (var db = new SOCIALEntities1())
            {
                var oSolicitudEnviada = (from a in db.Amistades
                                         where a.UsuarioSolicitanteId == Id &&
                                               a.UsuarioAceptadoId == userid
                                         select a).FirstOrDefault();

                oSolicitudEnviada.EstadoAmistad = 2; //ACEPTAR
                db.Entry(oSolicitudEnviada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            return Content("1");
        }



    }
}