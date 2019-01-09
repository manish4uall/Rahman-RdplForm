using RdplForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RdplForm.Controllers
{
    public class ShoppingSiteController : Controller
    {
        //
        // GET: /ShoppingSite/
        public ActionResult ShoppingPage()
        {
            RahmanDbEntities db = new RahmanDbEntities();
            var result = db.tblProductDetails.ToList();


            return View(result);
        }

        [HttpGet]
        public ActionResult CartItems()
        {
            List<CartItemsViewModel> cdList = new List<CartItemsViewModel>();
            if (Session["cart"] != null)
            {
                List<CartItems> cartIds = (List<CartItems>)Session["cart"];
                using (RahmanDbEntities db = new RahmanDbEntities())
                {
                    foreach (var item in cartIds)
                    {
                        CartItemsViewModel cd = db.tblProductDetails.Where(c => c.Id == item.ProductId).Select(c => new CartItemsViewModel
                        {
                            CompanyName = c.CompanyName,
                            ProdDescription = c.ProdDescription,
                            Id = c.Id,
                            Images = c.Images,
                            Price = c.Price,
                            ProductId = c.ProductId,
                            Quantity = item.Quantity
                        }).FirstOrDefault();

                        cdList.Add(cd);
                    }
                }
            }
            return View(cdList);

        }
        [HttpPost]
        public ActionResult Addtocart(int id)
        {
            int currentProductCount = 1;

            List<CartItems> cartIds = new List<CartItems>();
            if (Session["cart"] == null)
            {
                CartItems cv = new CartItems()
                {
                    ProductId = id,
                    Quantity = 1
                };
                cartIds.Add(cv);
                Session["cart"] = cartIds;
            }
            else
            {
                cartIds = (List<CartItems>)Session["cart"];
                var prod = cartIds.Where(c => c.ProductId == id).FirstOrDefault();
                if (prod != null)
                {
                    if(prod.Quantity <4)
                    { 
                    prod.Quantity = prod.Quantity + 1;
                    currentProductCount = prod.Quantity;
                    }
                    currentProductCount = prod.Quantity;
                }  
                else
                {
                    CartItems cv = new CartItems()
                    {
                        ProductId = id,
                        Quantity = 1
                    };
                    cartIds.Add(cv);
                }
                Session["cart"] = cartIds;

            }
            return Json(new { totalCount = cartIds.Count, currentProductCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SubItemToCart(int id)
        {
            int currentProductCount = 1;
            List<CartItems> cartIds = new List<CartItems>();
            if (Session["cart"] != null)
            {
                cartIds = (List<CartItems>)Session["cart"];
                var prod = cartIds.Where(c => c.ProductId == id).FirstOrDefault();
                if (prod != null && prod.Quantity > 1)
                {
                    prod.Quantity = prod.Quantity - 1;
                    currentProductCount = prod.Quantity;
                }
                Session["cart"] = cartIds;
            }
            return Json(new { currentProductCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCartItems()
        {
            List<CartItems> ids = new List<CartItems>();
            if (Session["Cart"] != null)
            {
                ids = (List<CartItems>)Session["Cart"];
                return Json(new { cartItemsCount = ids.Count, cartItems = ids });
            }
            else
            {
                return Json(new { cartItemsCount = 0, cartItems = ids }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult RemoveFromCart(int id)
        {
            List<CartItems> ProductIds = (List<CartItems>)Session["Cart"];
            List<CartItems> result = ProductIds.Where(x => x.ProductId != id).ToList();
            Session["Cart"] = null;
            Session["Cart"] = result;
            return Json(new { cartItemsCount = result.Count() });
        }
        [HttpGet]
        public ActionResult ShippingDetails()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult Shipping()
        {
            // var result = List< CartDetailsViewModel > Session["cart"]
            List<CartItemsViewModel> cdList = new List<CartItemsViewModel>();
            if (Session["cart"] != null)
            {
                List<CartItems> cartIds = (List<CartItems>)Session["cart"];
                using (RahmanDbEntities db = new RahmanDbEntities())
                {
                    foreach (var item in cartIds)
                    {
                        CartItemsViewModel cd = db.tblProductDetails.Where(c => c.Id == item.ProductId).Select(c => new CartItemsViewModel
                        {
                            CompanyName = c.CompanyName,
                            ProdDescription = c.ProdDescription,
                            Id = c.Id,
                            Images = c.Images,
                            Price = c.Price,
                            ProductId = c.ProductId,
                            Quantity = item.Quantity,
                            Total = c.Price * item.Quantity
                        }).FirstOrDefault();

                        cdList.Add(cd);
                    }
                }
            }

            return Json(cdList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ShippingDetails(RdplForm.Models.CustDetail result )
        {
            RahmanDbEntities db = new RahmanDbEntities();
            {

                tblCustDetail objcustdt = new tblCustDetail();
                objcustdt.CustName = result.CustName;
                objcustdt.Address = result.Address;
                objcustdt.MobileNo = result.MobileNO;
               // For total Amount Calculation
                List<CartItems> cartId = (List<CartItems>)Session["cart"];
                var total_Amount = 0;
                foreach (var item in cartId)
                {
                    //tblProductDetail prodDetail = new tblProductDetail();
                    var Amount = db.tblProductDetails.Where(c => c.ProductId == item.ProductId).FirstOrDefault();
                    total_Amount = total_Amount + (Amount.Price) * (item.Quantity);
                   
                }
                objcustdt.Total_Amount = total_Amount;


                db.tblCustDetails.Add(objcustdt);
                db.SaveChanges();
                ViewBag.Success = " Order placed Successfully ";

                tblProductDetail prodDetails=new tblProductDetail();
                
                List<CartItems> cartIds = (List<CartItems>)Session["cart"];
                
                 foreach (var item in cartIds)
                 {
                      
                     CustOrderDetail orddt = new CustOrderDetail();
                     orddt.Order_Id = objcustdt.CustId;
                     orddt.Quntity = item.Quantity;
                     orddt.Product_Id = item.ProductId;
                     
                     db.CustOrderDetails.Add(orddt);


                 }
                 db.SaveChanges();
                 ModelState.Clear();
            }
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
    }

 }
