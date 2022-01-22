using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Dal;
using CoffeShop.Models;


namespace CoffeShop.Controllers
{
    public class ClientSignInController : Controller
    {
        // GET: ClientSignIn
        public ActionResult SignIn()
        {
            return View(new ClientClass());
        }

        
        [HttpPost]
        public ActionResult ClientSignIn(ClientClass Client)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //check if numbers only
                    int n = Int32.Parse(Client.PhoneNumber);

                    if(n.ToString().Length >= 9)
                    {
                        //password check
                        

                        if (Client.Password.ToString().Length >=4)
                        {
                            //Create new Client
                            ClientDal clientDal = new ClientDal();
                            clientDal.Clients.Add(Client);
                            clientDal.SaveChanges();

                            return View("ThankYou", Client);

                        }
                        else
                        {
                            Session["SignInError"] = "Password Must be more then 4 charecters";
                            return View("SignIn", Client);
                        }


                    }
                    else
                    {
                        Session["SignInError"] = "Phone Number Must be with 10 digits";
                        return View("SignIn", Client);
                    }
                   
                }
                catch
                {
                    Session["SignInError"] = "Phone Number Must number only";
                    return View("SignIn", Client);

                }
                

                
               

                    
            }
            else
            {
                return View("SignIn", Client);
            }
        }
    }
}