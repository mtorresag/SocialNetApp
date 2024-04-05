using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetApp.Models;
using SocialNetApp.Models.ViewModel;

namespace SocialNetApp.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso


        //public ActionResult Enter(string user, string password)
        //{
        //    try
        //    {
        //        using (SOCIALEntities1 db = new SOCIALEntities1())
        //        {
        //            var lst = from d in db.Usuarios
        //                      where d.CorreoElectronico == user && d.Password == password && d.EstadoUsuario==1
        //                      select d;
        //            if (lst.Count()>0)
        //            {
        //                Usuarios oUser = lst.First();
        //                Session["User"] = oUser;
        //                Session["UserId"] = oUser.UsuarioId.ToString();
        //                return Content("1");
        //            }
        //            else
        //            {
        //                return Content("Usuario Invalido");
        //            }
        //        }

                    
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("Ocurrio un error :(" + ex.Message);
        //    }
        //}



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios oUsuario)
        {
            try
            {
                using (SOCIALEntities1 db = new SOCIALEntities1())
                {
                    var lst = from d in db.Usuarios
                              where d.CorreoElectronico == oUsuario.CorreoElectronico && d.Password == oUsuario.Password && d.EstadoUsuario == 1
                              select d;
                    if (lst.Count() > 0)
                    {
                        Usuarios oUser = lst.First();
                        Session["User"] = oUser;
                        Session["UserId"] = oUser.UsuarioId.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Mensaje"] = oUsuario;
                        //return View();
                        return Content("Usuario Invalido");
                    }
                }


            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error :(" + ex.Message);
            }
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


        public ActionResult Logoff()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Acceso");
        }


        //public ActionResult Registrar()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Registrar(Usuarios oUsuario)
        //{
        //    bool registrado;
        //    string mensaje;

        //    //if (oUsuario.Password = oUsuario.Con)
        //    return View();
        //}
    }
}