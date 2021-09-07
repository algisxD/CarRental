using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class ZaidimasController : Controller
    {
        //Apibrežiamos saugyklos kurios naudojamos šiame valdiklyje
        // GET: Zaidimas
        ZaidimasRepository zaidimasRepository = new ZaidimasRepository();
        public ActionResult Index()
        {
            //gražinamas darbuotoju sarašo vaizdas
            return View(zaidimasRepository.getZaidimai());
        }

        // GET: Zaidimas/CreategetZaidimas
        public ActionResult Create()
        {
            //Grazinama darbuotojo kūrimo forma
            Zaidimas zaidimas = new Zaidimas();
            return View(zaidimas);
        }

        // POST: Zaidimas/Create
        [HttpPost]
        public ActionResult Create(Zaidimas collection)
        {
            try
            {
                // Patikrinama ar tokiod arbuotojo nėra duomenų bazėje
                Zaidimas tmpZaidimas = zaidimasRepository.getZaidimas(collection.pavadinimas);

                if (tmpZaidimas.id!=null)
                {
                    ModelState.AddModelError("tabelis", "Zaidimas su tokiu tabelio numeriu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                //Jei darbuotojo su tabelio nr neranda prideda naują
                if (ModelState.IsValid)
                {
                    zaidimasRepository.addZaidimas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Zaidimas/Edit/5
        public ActionResult Edit(string id)
        {
            //grazinama darbuotojo redagavimo forma
            return View(zaidimasRepository.getZaidimas(id));
        }

        // POST: Zaidimas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Zaidimas collection)
        {
            try
            {
                // Atnaujina darbuotojo informacija
                if (ModelState.IsValid)
                {
                    zaidimasRepository.updateZaidimas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Zaidimas/Delete/5
        public ActionResult Delete(string id)
        {
            return View(zaidimasRepository.getZaidimas(id));
        }

        // POST: Zaidimas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                //if (zaidimasRepository.getZaidimasSutarciuCount(id)>0)
                //{
                //    naudojama = true;
                //    ViewBag.naudojama = "Zaidimas turi sudarytų sutarčių, pašalinti negalima.";
                //    return View(zaidimasRepository.getZaidimas(id));
                //}

                if (!naudojama)
                {
                    zaidimasRepository.deleteZaidimas(id);
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
