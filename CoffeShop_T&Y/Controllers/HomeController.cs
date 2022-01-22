using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Models;
using System.ComponentModel.DataAnnotations;
using CoffeShop.Dal;

namespace CoffeShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //delete me !
        public ActionResult AdminLogIn(AdminClass admin)
        {

            AdminDal dal = new AdminDal();
            AdminClass a = dal.Admins.Find(admin.ID);

            if(a != null)
            {
                if (ModelState.IsValid)
                {
                    if (a.Password == admin.Password)
                    {
                        return RedirectToAction("Enter", "Admin");
                    }
                    else
                    {
                        return View("index");
                    }

                }
                else
                {
                    
                    return View("index");

                }

            }
            else
            {
                return View("index");

            }



        }

        
        


    }
}