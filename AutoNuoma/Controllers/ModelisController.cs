using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class ModeliaiController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        ModeliuRepository modeliuRepository = new ModeliuRepository();
        // GET: Modelis
        public ActionResult Index()
        {
            return View(modeliuRepository.getModeliai());
        }

        // GET: Modelis/Create
        public ActionResult Create()
        {
            ModelisEditViewModel modelis = new ModelisEditViewModel();
            return View(modelis);
        }

        // POST: Modelis/Create
        [HttpPost]
        public ActionResult Create(ModelisEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    modeliuRepository.addModelis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Modelis/Edit/5
        public ActionResult Edit(int id)
        {
            ModelisEditViewModel modelis = modeliuRepository.getModelis(id);
            return View(modelis);
        }

        // POST: Modelis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ModelisEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    modeliuRepository.updateModelis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Modelis/Delete/5
        public ActionResult Delete(int id)
        {
            ModelisEditViewModel modelis = modeliuRepository.getModelis(id);
            return View(modelis);
        }

        // POST: Modelis/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ModelisEditViewModel modelis = modeliuRepository.getModelis(id);
                bool naudojama = false;

                if (modeliuRepository.getModelisCount(id)>0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Negalima pašalinti modelio, yra sukurtų automobilių su šiuo modeliu.";
                    return View(modelis);
                }

                if (!naudojama)
                {
                    modeliuRepository.deleteModelis(id);
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
