using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio5_1.Controllers
{
    public class HomeController : Controller
    {
        ShoeInventoryDBEntities shoeInventoryDBEntities = new ShoeInventoryDBEntities();

        public ActionResult Index()
        {
            //var listOfData = shoeInventoryDBEntities.ShoeInventoryTables.ToList();
            //return View(listOfData);
            Models.ClassListView classListView = new Models.ClassListView();

            var selectedListOfData = shoeInventoryDBEntities.ShoeInventoryTables.
                Select(x => new Models.ClassListView()
                {
                   Id = x.Id,
                   ShoeName = x.ShoeName,
                   ShoePrice = x.ShoePrice,
                   ShoeSize = (int)x.ShoeSize,
                   ShoeQuantity = (int)x.Quantity


                }).ToList();

            return View(selectedListOfData);

        }

        [HttpGet]
        public ActionResult Create() 
        { 
            return View();
        }


        [HttpPost]
        public ActionResult Create(ShoeInventoryTable shoeInventoryTable) 
        {
            shoeInventoryDBEntities.ShoeInventoryTables.Add(shoeInventoryTable);
            shoeInventoryDBEntities.SaveChanges();
            ViewBag.Message = "Data insert successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id) 
        { 
            var data  = shoeInventoryDBEntities.ShoeInventoryTables.Where(x => x.Id == id).FirstOrDefault();    
            return View(data);
        }


        [HttpPost]
        public ActionResult Edit(ShoeInventoryTable shoeInventoryTable)
        {
            var data = shoeInventoryDBEntities.ShoeInventoryTables.Where(x => x.Id == shoeInventoryTable.Id).FirstOrDefault();
            if (data != null)
            {
                data.ShoeName = shoeInventoryTable.ShoeName;
                data.ShoeDescription = shoeInventoryTable.ShoeDescription;
                data.ShoePrice = shoeInventoryTable.ShoePrice;
                data.ShoeSize = shoeInventoryTable.ShoeSize;
                data.Quantity = shoeInventoryTable.Quantity;
                shoeInventoryDBEntities.SaveChanges();

            }

            return RedirectToAction("Index");
        
        }

        public ActionResult Details(int id)
        {
            var data = shoeInventoryDBEntities.ShoeInventoryTables.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = shoeInventoryDBEntities.ShoeInventoryTables.Where(x => x.Id == id).FirstOrDefault();
            shoeInventoryDBEntities.ShoeInventoryTables.Remove(data);
            shoeInventoryDBEntities.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            
            return View();
        }

        public ActionResult Contact()
        {
           //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}