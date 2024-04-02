using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetApp.Models;

namespace SocialNetApp.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (SOCIALEntities db = new SOCIALEntities())
                {
                    var lst = from d in db.Usuarios
                              where d.CorreoElectronico == user && d.Password == password
                              select d;
                    if (lst.Count()>0)
                    {
                        Usuarios oUser = lst.First();
                        Session["User"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario Invalido");
                    }
                }

                    
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error :(" + ex.Message);
            }
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios oUsuario)
        {
            try
            {
                using (SOCIALEntities db = new SOCIALEntities())
                {
                    var lst = from d in db.Usuarios
                              where d.CorreoElectronico == oUsuario.CorreoElectronico && d.Password == oUsuario.Password
                              select d;
                    if (lst.Count() > 0)
                    {
                        Usuarios oUser = lst.First();
                        Session["User"] = oUser.NombreUsuario +" "+ oUser.ApellidoUsuario;
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


        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuarios oUsuario)
        {
            bool registrado;
            string mensaje;

            //if (oUsuario.Password = oUsuario.Con)
            return View();
        }
    }
}