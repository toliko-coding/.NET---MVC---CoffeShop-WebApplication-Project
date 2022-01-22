using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CoffeShop.Dal;
using CoffeShop.ViewModels;


namespace CoffeShop.Controllers
{
    public class BaristasController : Controller
    {
        // GET: Baristas
        public ActionResult login()
        {
            return View(new BaristasClass());
        }

        [HttpPost]
        public ActionResult BaristasLogin(BaristasClass barista)
        {
            BaristasDal dal = new BaristasDal();
            BaristasClass user = dal.Baristases.Find(barista.ID);
            if (user != null)
            {
                if(user.Password == barista.Password)
                {
                    if (ModelState.IsValid)
                    {
                        OrderViewModel vm = new OrderViewModel();
                        vm.barista = user;

                        TableDal tDal = new TableDal();
                        CoffeDal cDal = new CoffeDal();


                        vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
                        if(vm.Tables == null)
                        {
                            vm.Tables = new List<TableClass>();
                        }

                        vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();

                        if (vm.Products == null)
                        {
                            vm.Tables = new List<TableClass>();

                        }

                        vm.client = new ClientClass();

                        vm.Table = new TableClass();



                        ClientOrderClass order = new ClientOrderClass();
                        order.ClientID = "";
                        order.items = "";
                        order.TotalPrice = "0";
                        order.CoffeList = new List<CoffeClass>();

                        vm.order = order;

                        return View("BaristasConected", vm);
                    }
                    else
                    {
                        return View("login", barista);
                    }
                }
                else
                {
                    ViewBag.error = "One of the inputs incorrect";
                    Session["IDerror"] = "Password is not match to the ID";

                    return View("login", barista);
                }
                
            }
            else
            {
                ViewBag.error = "One of the inputs incorrect";
                Session["IDerror"] = "ID in not exsict";
                return View("login", barista);
            }

        }

        


        [HttpPost]
        public ActionResult ClientLoginToOrder()
        {
            OrderViewModel vm = new OrderViewModel();
            ClientDal ClDal = new ClientDal();
            BaristasDal bDal = new BaristasDal();
            TableDal tDal = new TableDal();
            CoffeDal cDal = new CoffeDal();

            

            vm.client = ClDal.Clients.Find(Request.Form["PhoneNumber"]);
            if(vm.client == null)
            {
                vm.client = new ClientClass();
                Session["ClientError"] = "There is no Clinet with the number :" + Request.Form["ClientID"];
            }

            vm.barista = bDal.Baristases.Find(Request.Form["BaristaId"]);

            vm.coffe = new CoffeClass();
            vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();
            if(vm.Products == null)
            {
                vm.Products = new List<CoffeClass>();
            }

            vm.Table = tDal.Tables.Find(Request.Form["tableID"]);
            if(vm.Table == null)
            {
                vm.Table = new TableClass();
            }

            vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
            if(vm.Tables == null)
            {
                vm.Tables = new List<TableClass>();
            }

            ClientOrderClass order = new ClientOrderClass();
            order.ClientID = "";
            order.items = "";
            order.TotalPrice = "0";
            order.CoffeList = new List<CoffeClass>();

            vm.order = order;

            return View("BaristasConected", vm);

        }

        [HttpPost]
        public ActionResult AddTableToOrder()
        {
            //build the ViewModel
            OrderViewModel vm = new OrderViewModel();

            vm.client = new ClientClass();
            vm.order = new ClientOrderClass();
            vm.coffe = new CoffeClass();
            vm.Table = new TableClass();


            TableDal tDal = new TableDal();
            CoffeDal cDal = new CoffeDal();
            ClientDal clDal = new ClientDal();

            vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
            vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();


            vm.order.ClientID = Request.Form["ClientID"];

            vm.order.TableID = Request.Form["tableID"];

            vm.order.items = Request.Form["itemsList"];

            vm.Table = tDal.Tables.Find(vm.order.TableID);

            vm.client = clDal.Clients.Find(vm.order.ClientID);
            if (vm.client == null)
            {
                vm.client = new ClientClass();
            }

            vm.order.TotalPrice = Request.Form["TotalPrice"];

            vm.order.CoffeList = new List<CoffeClass>();

            Session["CartUpdate"] = "Table number " + vm.order.TableID + " Is updated";

            if (vm.order.items != "")
            {
                string[] itemsArray = vm.order.items.Split(',');

                foreach (string itemId in itemsArray)
                {
                    if (itemId != "")
                    {
                        vm.order.CoffeList.Add(cDal.Products.Find(itemId));
                    }
                }
            }

            BaristasDal barDal = new BaristasDal();

            vm.barista = barDal.Baristases.Find(Request.Form["BaristaId"]);




            return View("BaristasConected", vm);





        }


        [HttpPost]
        public ActionResult AddItemToOrder()
        {

            //build the ViewModel
            OrderViewModel vm = new OrderViewModel();

            vm.client = new ClientClass();
            vm.order = new ClientOrderClass();
            vm.coffe = new CoffeClass();
            vm.Table = new TableClass();


            TableDal tDal = new TableDal();
            CoffeDal cDal = new CoffeDal();
            ClientDal clDal = new ClientDal();

            vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
            vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();


            vm.order.ClientID = Request.Form["ClientID"];

            vm.order.TableID = Request.Form["tableID"];

            vm.order.items = Request.Form["itemsList"];

            vm.order.TotalPrice = Request.Form["TotalPrice"];

            if (vm.order.TableID != "")
            {
                vm.Table = tDal.Tables.Find(vm.order.TableID);
            }
            else
            {
                vm.Table = new TableClass();
                vm.Table.ID = "";
            }



            vm.client = clDal.Clients.Find(vm.order.ClientID);
            if (vm.client == null)
            {
                vm.client = new ClientClass();
            }

            vm.order.CoffeList = new List<CoffeClass>();

            var newitemId = Request.Form["itemId"];
            vm.order.items = vm.order.items + newitemId + ",";


            string[] itemsArray = vm.order.items.Split(',');

            foreach (string itemId in itemsArray)
            {
                if (itemId != "")
                {
                    vm.order.CoffeList.Add(cDal.Products.Find(itemId));
                }
            }

            int currentPrice = Int32.Parse(vm.order.TotalPrice);
            int itemPrice = Int32.Parse(Request.Form["ItemPrice"]);
            int newPrice = currentPrice + itemPrice;

            vm.order.TotalPrice = newPrice.ToString();






            Session["CartUpdate"] = "Item Added To the Cart";





            BaristasDal barDal = new BaristasDal();

            vm.barista = barDal.Baristases.Find(Request.Form["BaristaId"]);




            return View("BaristasConected", vm);
        }



        [HttpPost]
        public ActionResult DeleteItemFromOrder()
        {

            //build the ViewModel
            OrderViewModel vm = new OrderViewModel();

            vm.client = new ClientClass();
            vm.order = new ClientOrderClass();
            vm.coffe = new CoffeClass();
            vm.Table = new TableClass();


            TableDal tDal = new TableDal();
            CoffeDal cDal = new CoffeDal();
            ClientDal clDal = new ClientDal();

            vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
            vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();


            vm.order.ClientID = Request.Form["ClientID"];

            vm.order.TableID = Request.Form["tableID"];

            vm.order.items = Request.Form["itemsList"];

            vm.order.TotalPrice = Request.Form["TotalPrice"];



            var newitemId = Request.Form["itemId"];

            //create Dals
            vm.Table = tDal.Tables.Find(vm.order.TableID);

            vm.client = clDal.Clients.Find(vm.order.ClientID);
            if (vm.client == null)
            {
                vm.client = new ClientClass();
            }


            vm.order.CoffeList = new List<CoffeClass>();

            //string itemsss = vm.order.items;
            //itemsss.Remove(vm.order.items + ",");

            string[] itemsss = vm.order.items.Split(',');

            // {1 , , , 1 , 2 ,}


            for (int i = 0; i < itemsss.Length; i++)
            {
                if (itemsss[i] == newitemId)
                {
                    itemsss[i] = ",";
                    break;
                }
            }

            vm.order.items = "";
            //rebuild
            for (int i = 0; i < itemsss.Length; i++)
            {
                if (itemsss[i] != ",")
                {
                    vm.order.items = vm.order.items + itemsss[i] + ",";
                }
            }




            //create new CoffeList of the Order
            string[] itemsArray = vm.order.items.Split(',');

            foreach (string itemId in itemsArray)
            {
                if (itemId != "")
                {
                    vm.order.CoffeList.Add(cDal.Products.Find(itemId));
                }
            }

            int currentPrice = Int32.Parse(vm.order.TotalPrice);
            int itemPrice = Int32.Parse(Request.Form["ItemPrice"]);
            int newPrice = currentPrice - itemPrice;

            vm.order.TotalPrice = newPrice.ToString();






            Session["CartUpdate"] = "Item Deleted From The Cart";



            BaristasDal barDal = new BaristasDal();

            vm.barista = barDal.Baristases.Find(Request.Form["BaristaId"]);




            return View("BaristasConected", vm);
        }


        [HttpPost]
        public ActionResult CheckOut()
        {
            bool Payment = true;

            if (Payment)
            {
                BaristasDal BaristasDal = new BaristasDal();
                BaristasClass barista = BaristasDal.Baristases.Find(Request.Form["BaristaId"]);


                //get relative data
                Order order = new Order();
                order.Barista = barista.ID;
                order.ClientID = Request.Form["ClientID"];
                order.TableID = Request.Form["tableID"];
                order.TotalPrice = Request.Form["TotalPrice"];
                order.items = Request.Form["itemsList"];

                //check Table
                if (order.TableID != "" && order.items != "")
                {
                    // Table Status Change
                    TableDal tables = new TableDal();
                    TableClass table = tables.Tables.Find(order.TableID);
                    table.Free = "0";
                    tables.SaveChanges();

                    //write new order
                    OrderDal orderDal = new OrderDal();
                    orderDal.Orders.Add(order);
                    orderDal.SaveChanges();




                    //build next page ovject
                    

                    OrderViewModel vm = new OrderViewModel();
                    vm.barista = barista;
                    vm.client = new ClientClass();

                    
                    

                    ClientOrderClass Neworder = new ClientOrderClass();

                    Neworder.ClientID = order.ClientID;
                    Neworder.items = order.items;
                    Neworder.TableID = order.TableID;
                    Neworder.TotalPrice = order.TotalPrice;
                    Neworder.CoffeList = new List<CoffeClass>();

                    vm.order = Neworder;
                  

                    vm.Tables = tables.Tables.Where(I => I.Free == "1").ToList();
                    if(vm.Tables == null)
                    {
                        vm.Tables = new List<TableClass>();
                    }

                    vm.Table = new TableClass();

                    CoffeDal cDal = new CoffeDal();
                    vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();
                    if(vm.Products == null)
                    {
                        vm.Products = new List<CoffeClass>();
                    }

                    string[] itemsArray = vm.order.items.Split(',');

                    foreach (string itemId in itemsArray)
                    {
                        if (itemId != "")
                        {
                            vm.order.CoffeList.Add(cDal.Products.Find(itemId));
                        }
                    }






                    return View("BaristaOrderConfirm", vm);

                }

                else
                {

                    //build the ViewModel
                    OrderViewModel vm = new OrderViewModel();

                    vm.client = new ClientClass();
                    vm.order = new ClientOrderClass();
                    vm.coffe = new CoffeClass();
                    vm.Table = new TableClass();
                    vm.barista = barista;

                   


                    TableDal tDal = new TableDal();
                    CoffeDal cDal = new CoffeDal();
                    ClientDal clDal = new ClientDal();

                    vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
                    vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();


                    vm.order.ClientID = Request.Form["ClientID"];

                    vm.order.TableID = Request.Form["tableID"];

                    vm.order.items = Request.Form["itemsList"];

                    vm.order.TotalPrice = Request.Form["TotalPrice"];



                    if (vm.order.TableID != "")
                    {
                        vm.Table = tDal.Tables.Find(vm.order.TableID);
                    }
                    else
                    {
                        vm.Table = new TableClass();
                        vm.Table.ID = "";
                    }

                    vm.client = clDal.Clients.Find(vm.order.ClientID);
                    if(vm.client == null)
                    {
                        vm.client = new ClientClass();
                    }



                    vm.order.CoffeList = new List<CoffeClass>();

                    string[] itemsArray = vm.order.items.Split(',');

                    foreach (string itemId in itemsArray)
                    {
                        if (itemId != "")
                        {
                            vm.order.CoffeList.Add(cDal.Products.Find(itemId));
                        }
                    }



                  

                    Session["OrderError"] = "You Have yo choose Table + Item in Order to make an order";


                    return View("BaristasConected", vm);

                }


            }
            else
            {
                //build the ViewModel
                OrderViewModel vm = new OrderViewModel();

                vm.client = new ClientClass();
                vm.order = new ClientOrderClass();
                vm.coffe = new CoffeClass();
                vm.Table = new TableClass();


                TableDal tDal = new TableDal();
                CoffeDal cDal = new CoffeDal();
                ClientDal clDal = new ClientDal();
                BaristasDal bDal = new BaristasDal();

                vm.barista = bDal.Baristases.Find(Request.Form["BaristaId"]);

                vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
                vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();


                vm.order.ClientID = Request.Form["ClientID"];

                vm.order.TableID = Request.Form["tableID"];

                vm.order.items = Request.Form["itemsList"];

                vm.order.TotalPrice = Request.Form["TotalPrice"];

                vm.Table = tDal.Tables.Find(vm.order.TableID);

                vm.client = clDal.Clients.Find(vm.order.ClientID);

                vm.Products = cDal.Products.ToList();

                vm.order.CoffeList = new List<CoffeClass>();

                string[] itemsArray = vm.order.items.Split(',');

                foreach (string itemId in itemsArray)
                {
                    if (itemId != "")
                    {
                        vm.order.CoffeList.Add(cDal.Products.Find(itemId));
                    }
                }





                Session["Payment"] = "you cannot afford to buy our coffee because your broke..";


                return View("BaristasConected", vm);
            }


        }

        [HttpPost]
        public ActionResult ReconectBarista()
        {
            OrderViewModel vm = new OrderViewModel();

            //dals
            CoffeDal cDal = new CoffeDal();
            TableDal tDal = new TableDal();
            BaristasDal bDal = new BaristasDal();

            vm.client = new ClientClass();

            vm.barista = bDal.Baristases.Find(Request.Form["BaristaId"]);
            vm.Products = cDal.Products.Where(I => I.avlilable.ToString() == "1").ToList();

            vm.coffe = new CoffeClass();
            if (vm.Products == null)
            {
                vm.Products = new List<CoffeClass>();

            }

            vm.Table = new TableClass();
            vm.Tables = tDal.Tables.Where(I => I.Free == "1").ToList();
            if (vm.Tables == null)
            {
                vm.Tables = new List<TableClass>();
            }

            ClientOrderClass order = new ClientOrderClass();
            order.ClientID = "";
            order.items = "";
            order.TotalPrice = "0";
            order.CoffeList = new List<CoffeClass>();

            vm.order = order;

            return View("BaristasConected", vm);

        }
    }
}