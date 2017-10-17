using Market.Models;
using Market.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Market.Controllers
{
    public class OrdersController : Controller
    {
        MarketContext db = new MarketContext(); 

        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Models.Customer();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;

            var list = db.Customers.ToList();
            list.Add(new Customer() { CustomerID = 0, FullName = "[Seleccione un cliente]" });

            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            var customerID = int.Parse(Request["CustomerID"]);
            if (customerID == 0)
            {
                var listc = db.Customers.ToList();
                listc.Add(new Customer() { CustomerID = 0, FullName = "[Seleccione un cliente]" });

                listc = listc.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(listc, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(orderView);
            }

            var customer = db.Customers.Find(customerID);
            if (customer==null)
            {
                var listc = db.Customers.ToList();
                listc.Add(new Customer() { CustomerID = 0, FullName = "[Seleccione un cliente]" });

                listc = listc.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(listc, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(orderView);
            }
            
            if (orderView.Products.Count==0)
            {
                var listc = db.Customers.ToList();
                listc.Add(new Customer() { CustomerID = 0, FullName = "[Seleccione un cliente]" });

                listc = listc.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(listc, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar detalle";
                return View(orderView);
            }

            var order = new Order
            {
                CustomerID = customerID,
                DateOrder = DateTime.Now,
                OrderStatus = OrderStatus.Crated
            };

            db.Orders.Add(order);
            db.SaveChanges();

            var orderID = order.OrderID;

            foreach (var item in orderView.Products)
            {
                var orderDatail = new OrderDetail {
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    OrderID = orderID
                };
                db.OrderDetails.Add(orderDatail);
            }

            db.SaveChanges();

            //return View(orderView);
            ViewBag.Message = $"La orden: {orderID}, grabada exitosamente";
            RedirectToAction("NewOrder");
        }


        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un producto]" });
            list = list.OrderBy(c => c.Description).ToList();
            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            if (!ModelState.IsValid)
            {

            }
            var orderView = Session["orderView"] as OrderView;

            var productID = int.Parse(Request["ProductID"]);

            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un producto]" });
            list = list.OrderBy(c => c.Description).ToList();
            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            if (productID==0)
            {
                ViewBag.Error = "Debe seleccionar un producto";
                return View(productOrder);
            }

            var product = db.Products.Find(productID);
            if (product==null)
            {
                ViewBag.Error = "Producto no existe";
                return View(productOrder);
            }

            productOrder = orderView.Products.Find(p => p.ProductID.Equals(productID));

            if (productOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])
                };
                orderView.Products.Add(productOrder);

            }
            else
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }


            var listCustomer = db.Customers.ToList();
            listCustomer.Add(new Customer() { CustomerID = 0, FirstName = "[Seleccione un cliente]" });

            listCustomer = listCustomer.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(listCustomer, "CustomerID", "FullName");

            return View("NewOrder", orderView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}