using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class RenginiaiController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        RenginiuRepository renginiuRepository = new RenginiuRepository();
        // GET: Renginys
        public ActionResult Index()
        {
            return View(renginiuRepository.getRenginiai());
        }

        // GET: Renginys/Create
        public ActionResult Create()
        {
            RenginysEditViewModel renginys = new RenginysEditViewModel();
            return View(renginys);
        }

        // POST: Renginys/Create
        [HttpPost]
        public ActionResult Create(RenginysEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    renginiuRepository.addRenginys(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Renginys/Edit/5
        public ActionResult Edit(string id)
        {
            RenginysEditViewModel renginys = renginiuRepository.getRenginys(id);
            return View(renginys);
        }

        // POST: Renginys/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, RenginysEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    renginiuRepository.updateRenginys(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Renginys/Delete/5
        public ActionResult Delete(string id)
        {
            RenginysEditViewModel renginys = renginiuRepository.getRenginys(id);
            return View(renginys);
        }

        // POST: Renginys/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                RenginysEditViewModel renginys = renginiuRepository.getRenginys(id);
                bool naudojama = false;

                //if (renginiuRepository.getRenginysCount(id)>0)
                //{
                //    naudojama = true;
                //    ViewBag.naudojama = "Negalima pašalinti arenos, yra sukurtų automobilių su šiuo renginiu.";
                //    return View(renginys);
                //}

                if (!naudojama)
                {
                    renginiuRepository.deleteRenginys(id);
                }
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
