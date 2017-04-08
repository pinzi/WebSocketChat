using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Server.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public JsonResult Reg()
        {
            return View();
        }
    }
}