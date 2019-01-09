using RdplForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Data.Entity;


namespace RdplForm.Controllers
{
    public class FormController : Controller
    {
        [HttpGet]
        public ActionResult EmpDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveEmpDetail(OrderMst objOrder)
        {
           
            RahmanDbEntities objdb = new RahmanDbEntities();
            if (objOrder.CustomerName==null|| objOrder.OrderDetail==null)
            {
                ViewBag.msg= "Plz Fill all the details";
                return View("EmpDetail");
            }
            else
            {
                tblOrderMst objordmst = new tblOrderMst();
                // List<tblOrderDt objordDt = new tblOrderDt();
                {
                    objordmst.OrderNo = objOrder.No;
                    objordmst.OrdDate = objOrder.Date;
                    objordmst.CustName = objOrder.CustomerName;
                }
                objdb.tblOrderMsts.Add(objordmst);
                objdb.SaveChanges();

                var id = objdb.tblOrderMsts.OrderByDescending(x => x.OrderId).First().OrderId;
                foreach (var lstg in objOrder.OrderDetail)
                {
                    tblOrderDt objdt = new tblOrderDt();
                    objdt.OrderId = id;
                    objdt.Item = lstg.Item;
                    objdt.Quantity = lstg.Quantity;
                    objdt.Rate = lstg.Rate;


                    objdb.tblOrderDts.Add(objdt);
                    objdb.SaveChanges();
                }

            }
            return Json(" Data Successfuly Saved");
        }
        public ActionResult List()
        {
            RahmanDbEntities objdb = new RahmanDbEntities();
            {
                tblOrderMst objtblOrdMst = new tblOrderMst();
                {
                    List<tblOrderMst> result = objdb.tblOrderMsts.ToList();
                    return Json(result ,JsonRequestBehavior.AllowGet);
                }

            }
           
        }
	}
}