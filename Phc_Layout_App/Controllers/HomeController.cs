using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Phc_Layout.Models;
using Phc_Layout.Services;

namespace Phc_Layout.Controllers
{
    public class HomeController : Controller
    {
        readonly SupplierService _supplierService;

        public HomeController(SupplierService supplierService)
        {
            this._supplierService = supplierService;
       }


        public IActionResult Index()
        {

            return View();
        }


        public JsonResult GetSuppliers()
        {
            List<SupplierOption> supplierOptions = this._supplierService.GetSupplierCollections("507981");

            return Json(supplierOptions);
        }

        public JsonResult GetSuppliersAddress(string masterId)
        {
            
            List<SupplierAddressModel> supplierAddressOptions = this._supplierService.GetSupplierAddressCollection("507981");

            return Json(supplierAddressOptions);
        }


        public JsonResult GetDivisionLocation(String id)
        {
            List<RootObject> rootObjects = new List<RootObject>();
            List<DivisionsLocationOption> divisionLocationOptions = this._supplierService.GetDivisionLocationCollections("507981", id);
            var data = (from m in divisionLocationOptions
                        select m.masterId).Distinct();


            foreach (var itemTop in data)
            {
                var data1 = divisionLocationOptions.FirstOrDefault(cond => cond.masterId == itemTop);

                RootObject rootObject = new RootObject
                {
                    text = data1.parkerDivName,
                    expanded = false
                };

                var data2 = divisionLocationOptions.Where(cond => cond.masterId == itemTop);
                List<Item> items = new List<Item>();
                foreach (var item in data2)
                {
                    items.Add(new Item
                    {
                        text = item.parkerLocName
                    });
                }

                rootObject.items = items;
                rootObjects.Add(rootObject);
            }

            return Json(rootObjects);
        }

    }
    
}

    
