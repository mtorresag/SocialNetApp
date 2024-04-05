﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetApp.Models.TableViewModel
{
    public class UserTableViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
    }

}