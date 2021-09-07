using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class RemejasController : Controller
    {
        //Apibrežiamos saugyklos kurios naudojamos šiame valdiklyje
        // GET: Remejas
        RemejasRepository remejasRepository = new RemejasRepository();
        public ActionResult Index()
        {
            //gražinamas darbuotoju sarašo vaizdas
            return View(remejasRepository.getRemejai());
        }

        // GET: Remejas/CreategetRemejas
        public ActionResult Create()
        {
            //Grazinama darbuotojo kūrimo forma
            Remejas remejas = new Remejas();
            return View(remejas);
        }

        // POST: Remejas/Create
        [HttpPost]
        public ActionResult Create(Remejas collection)
        {
            try
            {
                // Patikrinama ar tokiod arbuotojo nėra duomenų bazėje
                Remejas tmpRemejas = remejasRepository.getRemejas(collection.pavadinimas);

                if (tmpRemejas.pavadinimas!=null)
                {
                    ModelState.AddModelError("tabelis", "Remejas su tokiu tabelio numeriu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                //Jei darbuotojo su tabelio nr neranda prsaliseda naują
                if (ModelState.IsValid)
                {
                    remejasRepository.addRemejas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Remejas/Edit/5
        public ActionResult Edit(string id)
        {
            //grazinama darbuotojo redagavimo forma
            return View(remejasRepository.getRemejas(id));
        }

        // POST: Remejas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Remejas collection)
        {
            try
            {
                // Atnaujina darbuotojo informacija
                if (ModelState.IsValid)
                {
                    remejasRepository.updateRemejas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Remejas/Delete/5
        public ActionResult Delete(string id)
        {
            return View(remejasRepository.getRemejas(id));
        }

        // POST: Remejas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                //if (remejasRepository.getRemejasSutarciuCount(salis)>0)
                //{
                //    naudojama = true;
                //    ViewBag.naudojama = "Remejas turi sudarytų sutarčių, pašalinti negalima.";
                //    return View(remejasRepository.getRemejas(salis));
                //}

                if (!naudojama)
                {
                    remejasRepository.deleteRemejas(id);
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
