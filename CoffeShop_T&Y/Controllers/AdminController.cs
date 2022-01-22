using CoffeShop.Dal;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CoffeShop.ViewModels;

namespace CoffeShop.Controllers
{
    public class AdminController : Controller
    {
        // Admin page load actions
        public ActionResult Enter()
        {
            return View();
        }

        public ActionResult login()
        {
            return View(new AdminClass());
        }

        //Barista Index

        public ActionResult BaristaAdd()
        {
            return View(new BaristasClass());
        }

        public ActionResult BaristaEdit()
        {
            UsersViewModel vm = new UsersViewModel();
            
            BaristasDal bDal = new BaristasDal();


            vm.barista = new BaristasClass();

            vm.baristaS = bDal.Baristases.ToList();

            return View(vm);
        }

        public ActionResult BaristaDelete()
        {
            UsersViewModel vm = new UsersViewModel();
            BaristasDal bDal = new BaristasDal();

            vm.barista = new BaristasClass();
            vm.baristaS = bDal.Baristases.ToList();
            return View("BaristaEdit", vm);
        }

       


        //Table Index
        public ActionResult TableAdd()
        {
            TableDal dal = new TableDal();
            TableViewModel vm = new TableViewModel();
            List<TableClass> AllTables = dal.Tables.ToList();
            vm.Table = new TableClass();
            vm.Tables = AllTables;
            return View(vm);
           
        }

        public ActionResult TableEdit()
        {
            TableDal dal = new TableDal();
            TableViewModel vm = new TableViewModel();
            List<TableClass> AllTables = dal.Tables.ToList();
            vm.Table = new TableClass();
            vm.Tables = AllTables;
            return View(vm);


        }

        public ActionResult TableDelete()
        {
            TableDal dal = new TableDal();
            TableViewModel vm = new TableViewModel();
            List<TableClass> AllTables = dal.Tables.ToList();
            vm.Table = new TableClass();
            vm.Tables = AllTables;
            return View(vm);


        }


        //Menu Index
        public ActionResult MenuManage()
        {
            CoffeDal dal = new CoffeDal();
            CoffeViewModel vm = new CoffeViewModel();
            List<CoffeClass> products = dal.Products.ToList();
            vm.coffe = new CoffeClass();
            vm.Products = products;
            return View(vm);
        }

        public ActionResult MenuEdit()
        {
            CoffeDal dal = new CoffeDal();
            CoffeViewModel vm = new CoffeViewModel();
            List<CoffeClass> products = dal.Products.ToList();
            vm.coffe = new CoffeClass();
            vm.Products = products;
            return View(vm);
        }

        public ActionResult MenuDelete()
        {
            CoffeDal dal = new CoffeDal();
            CoffeViewModel vm = new CoffeViewModel();
            List<CoffeClass> products = dal.Products.ToList();
            vm.coffe = new CoffeClass();
            vm.Products = products;
            return View(vm);
        }


        // 'functions'
        public ActionResult AdminLogIn(AdminClass admin)
        {

            AdminDal dal = new AdminDal();
            AdminClass a = dal.Admins.Find(admin.ID);

            if (a != null)
            {
                if (ModelState.IsValid)
                {
                    if (a.Password == admin.Password)
                    {
                        return View("Enter");
                    }
                    else
                    {
                        ViewBag.error = "One of the inputs incorrect";
                        return View("login" , admin);
                    }

                }
                else
                {
                    
                    return View("login", admin);

                }

            }
            else
            {
                ViewBag.error = "One of the inputs incorrect";
                return View("login", admin);

            }



        }

        [HttpPost]
        public ActionResult SignInBarista(BaristasClass barista)
        {
            if (ModelState.IsValid)
            {
                BaristasDal dal = new BaristasDal();
                dal.Baristases.Add(barista);
                dal.SaveChanges();

                return View("BaristaConfirm", barista);
            }
            else
            {
                return View("BaristaAdd", barista);
            }
        }
        [HttpPost]
        public ActionResult EditBarista()
        {
            UsersViewModel vm = new UsersViewModel();
            BaristasDal bDal = new BaristasDal();

            BaristasClass barista = bDal.Baristases.Find(Request.Form["ID"]);

            if (barista != null)
            {

                string newName = Request.Form["Name"];
                string newPassword = Request.Form["Password"];

                if (newName != "")
                {
                    barista.Name = newName;
                }

                if (newPassword != "")
                {
                    barista.Password = newPassword;
                }

                bDal.SaveChanges();

                vm.baristaS = bDal.Baristases.ToList();
                vm.barista = new BaristasClass();

                return View("BaristaEdit", vm);
            }
            else
            {
                Session["IDERROR"] = "This is not a valid ID";
                vm.barista = new BaristasClass();
                vm.baristaS = bDal.Baristases.ToList();
                return View("BaristaEdit", vm);

            }


        }

        [HttpPost]
        public ActionResult DeleteBarista()
        {
            UsersViewModel vm = new UsersViewModel();
            BaristasDal bDal = new BaristasDal();

            string id = Request.Form["ID"];

            BaristasClass dBarista = bDal.Baristases.Find(Request.Form["ID"]);
            if(dBarista != null)
            {
                bDal.Baristases.Remove(dBarista);
                bDal.SaveChanges();

            }
           
            vm.baristaS = bDal.Baristases.ToList();
            vm.barista = new BaristasClass();

            Session["Error"] = "Barista havent found";
            return View("BaristaEdit", vm);
        }



        [HttpPost]
        public ActionResult AddCoffe()
        {
            CoffeViewModel vm = new CoffeViewModel();
            CoffeClass coffe = new CoffeClass()
            {
                ID = Request.Form["ID"],
                CoffeName = Request.Form["CoffeName"],
                Price = Int32.Parse(Request.Form["Price"]),
                BPrice = Int32.Parse(Request.Form["BPrice"]),
                AgeLimit = Int32.Parse(Request.Form["AgeLimit"]),
                avlilable = Int32.Parse(Request.Form["avlilable"]),
                imgName = Request.Form["imgName"],
                isBusiness = Int32.Parse(Request.Form["isBusiness"]),
            };

            
                CoffeDal dal = new CoffeDal();

                dal.Products.Add(coffe);
                dal.SaveChanges();
                vm.coffe = new CoffeClass();
                vm.Products = dal.Products.ToList<CoffeClass>();
                return RedirectToAction("MenuManage", "Admin");
            
           
        }
        [HttpPost]
        public ActionResult EditCoffe()
        {
            CoffeViewModel vm = new CoffeViewModel();
            CoffeDal dal = new CoffeDal();
            vm.coffe = dal.Products.Find(Request.Form["ID"]);
           
            if(vm.coffe != null)
            {
                if (Request.Form["CoffeName"] != "")
                {
                    vm.coffe.CoffeName = Request.Form["CoffeName"];
                }

                if (Request.Form["Price"] != "")
                {
                    vm.coffe.Price = Int32.Parse(Request.Form["Price"]);
                }

                if (Request.Form["BPrice"] != "")
                {
                    vm.coffe.BPrice = Int32.Parse(Request.Form["BPrice"]);
                }

                if (Request.Form["AgeLimit"] != "")
                {
                    vm.coffe.AgeLimit = Int32.Parse(Request.Form["AgeLimit"]);
                }

                if (Request.Form["avlilable"] != "")
                {
                    vm.coffe.avlilable = Int32.Parse(Request.Form["avlilable"]);
                }

                if (Request.Form["imgName"] != "")
                {
                    vm.coffe.imgName = Request.Form["imgName"];
                }

                if (Request.Form["isBusiness"] != "")
                {
                    vm.coffe.isBusiness = Int32.Parse(Request.Form["isBusiness"]);
                }



                dal.SaveChanges();

                vm.Products = dal.Products.ToList<CoffeClass>();
                return RedirectToAction("MenuEdit", "Admin");
            }
            else
            {
                return RedirectToAction("MenuEdit", "Admin");
            }
            

            
            


        }

        public ActionResult DeleteCoffe()
        {
            CoffeViewModel vm = new CoffeViewModel();
            CoffeDal dal = new CoffeDal();
            
            vm.coffe = dal.Products.Find(Request.Form["ID"]);
            if(vm.coffe != null)
            {
                dal.Products.Remove(vm.coffe);
                dal.SaveChanges();
                Session["DeleteError"] = "Item " + vm.coffe.CoffeName + " is Deleted From The Menu";
                return RedirectToAction("MenuDelete", "Admin");
            }
            else
            {
                Session["DeleteError"] = "There is not coffe with ID : " + Request.Form["ID"] + " in the Menu";
                return RedirectToAction("MenuDelete", "Admin");
            }

        }

        

        [HttpPost]
        public ActionResult AddTable()
        {
            TableViewModel vm = new TableViewModel();
            try
            {
                TableClass table = new TableClass()
                {
                    ID = Request.Form["ID"],
                    Seats = Request.Form["Seats"],
                    Dicription = Request.Form["Dicription"],
                    Inside = Request.Form["Inside"],
                    Free = "1",
                };

                if ( (table.ID != "" ) && (table.Seats != "") && (table.Dicription != "") && (table.Inside != ""))
                {
                    TableDal dal = new TableDal();
                    dal.Tables.Add(table);
                    dal.SaveChanges();
                    vm.Table = table;
                    vm.Tables = dal.Tables.ToList<TableClass>();
                    return RedirectToAction("TableAdd", "Admin");
                }
                else
                {
                    Session["TableError"] = "One of The fields is not correct";
                    return RedirectToAction("TableAdd", "Admin");
                }
            }


            catch
            {
                Session["TableError"] = "EROOR (Throw) ! catch => One of The fields is not correct ";
                return RedirectToAction("TableAdd", "Admin");
            }

            

            
        }

        [HttpPost]
        public ActionResult EditTable()
        {
            TableViewModel vm = new TableViewModel();
            TableDal dal = new TableDal();
            vm.Table = dal.Tables.Find(Request.Form["ID"]);

            if (vm.Table != null)
            {
                if (Request.Form["Seats"] != "")
                {
                    vm.Table.Seats = Request.Form["Seats"];
                }

                if (Request.Form["Dicription"] != "")
                {
                    vm.Table.Dicription = Request.Form["Dicription"];
                }

                if (Request.Form["Free"] != "")
                {
                    vm.Table.Free = Request.Form["Free"];
                }

                if (Request.Form["Inside"] != "")
                {
                    vm.Table.Inside = Request.Form["Inside"];
                }



                dal.SaveChanges();
                vm.Tables = dal.Tables.ToList<TableClass>();
                return RedirectToAction("TableEdit", "Admin");
            }
            else
            {
                return RedirectToAction("TableEdit", "Admin");
            }

        }

        [HttpPost]
        public ActionResult DeleteTable()
        {
            TableViewModel vm = new TableViewModel();
            TableDal dal = new TableDal();
            vm.Table = dal.Tables.Find(Request.Form["ID"]);

            if (vm.Table != null)
            {
               



                dal.Tables.Remove(vm.Table);
                dal.SaveChanges();
                Session["DeleteConfirm"] = "The Table " + vm.Table.ID.ToString() + " is Deleted";



                vm.Tables = dal.Tables.ToList<TableClass>();
                return RedirectToAction("TableDelete", "Admin");
            }
            else
            {
                Session["DeleteConfirm"] = "This Table ID in not exict";
                return RedirectToAction("TableDelete", "Admin");
            }
        }

        

    }

   
}




