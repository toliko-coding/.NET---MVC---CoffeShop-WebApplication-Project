using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Models;
using System.ComponentModel.DataAnnotations;
using CoffeShop.Dal;
using CoffeShop.ViewModels;

namespace CoffeShop.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult ClientLogIn()
        {
            return View(new ClientClass());
        }

        
        
        [HttpPost]
        public ActionResult ClientLogin(ClientClass client)
        {
            ClientDal dal = new ClientDal();
            ClientClass user = dal.Clients.Find(client.PhoneNumber);
            

            if(user != null)
            {
                client.Name = user.Name;

                if (ModelState.IsValid)
                {


                    if (client.Password == user.Password)
                    {
                        //Client
                        OrderViewModel vm = new OrderViewModel();
                        vm.client = client;
                        //

                        
                        CoffeDal coffedal = new CoffeDal();
                        vm.coffe = new CoffeClass();
                        vm.Products = coffedal.Products.Where(I => I.avlilable.ToString() == "1").ToList();


                        //


                        TableDal tabledal = new TableDal();
                        vm.Table = new TableClass();
                        vm.Tables = tabledal.Tables.Where(I => I.Free == "1").ToList();

                        //
                        ClientOrderClass order = new ClientOrderClass();
                        order.ClientID = client.PhoneNumber;
                        order.items = "";
                        order.TotalPrice = "0";
                        order.CoffeList = new List<CoffeClass>();

                        vm.order = order;


                        return View("ClientConected", vm);

                    }
                    else
                    {
                        ViewBag.error = "One of the inputs incorrect";
                        return View("ClientLogIn", client);
                    }


                }
                else
                {
                    return View("ClientLogIn", client);
                }
            }

            else
            {
                ViewBag.error = "One of the inputs incorrect";
                return View("ClientLogIn", client);
            }

            

        }


        [HttpPost]
        public ActionResult ClientSignIn(ClientClass client)
        {

            if (ModelState.IsValid)
            {

                return View("ClientConected", client);

            }
            else
            {
                return View("guest", client);
            }



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

            vm.order.TotalPrice = Request.Form["TotalPrice"];

            vm.order.CoffeList = new List<CoffeClass>();

            Session["CartUpdate"] = "Table number " + vm.order.TableID + " Is updated";

            if(vm.order.items != "")
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

            


            return View("ClientConected",vm);

            
            


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

            if(vm.order.TableID != "")
            {
                vm.Table = tDal.Tables.Find(vm.order.TableID);
            }
            else
            {
                vm.Table = new TableClass();
                vm.Table.ID = "";
            }

            

            vm.client = clDal.Clients.Find(vm.order.ClientID);

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


            
            


            Session["CartUpdate"] =  "Item Added To the Cart";





            return View("ClientConected", vm);
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


            vm.order.CoffeList = new List<CoffeClass>();

            //string itemsss = vm.order.items;
            //itemsss.Remove(vm.order.items + ",");

            string[] itemsss = vm.order.items.Split(',');

            // {1 , , , 1 , 2 ,}

            
            for(int i = 0; i < itemsss.Length;i++)
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
                if(itemsss[i] != ",")
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





            return View("ClientConected", vm);
        }


        [HttpPost]
        public ActionResult CheckOut()
        {
            bool Payment = true;

            if(Payment)
            {
                

                //get relative data
                Order order = new Order();
                order.Barista = "";
                order.ClientID = Request.Form["ClientID"];
                order.TableID = Request.Form["tableID"];
                order.TotalPrice = Request.Form["TotalPrice"];
                order.items = Request.Form["itemsList"];

                //check Table
                if(order.ClientID != "" && order.TableID != "" && order.items != "")
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

                    return View("ClientOrderConfirm", order);

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


                    return View("ClientConected", vm);

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


                return View("ClientConected", vm);
            }
            
            
        }



    }
}
