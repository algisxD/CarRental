using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class ArenaController : Controller
    {
        //Apibrežiamos saugyklos kurios naudojamos šiame valdiklyje
        // GET: Arena
        ArenaRepository arenaRepository = new ArenaRepository();
        public ActionResult Index()
        {
            //gražinamas darbuotoju sarašo vaizdas
            return View(arenaRepository.getArenos());
        }

        // GET: Arena/CreategetArena
        public ActionResult Create()
        {
            //Grazinama darbuotojo kūrimo forma
            Arena arena = new Arena();
            return View(arena);
        }

        // POST: Arena/Create
        [HttpPost]
        public ActionResult Create(Arena collection)
        {
            try
            {
                // Patikrinama ar tokiod arbuotojo nėra duomenų bazėje
                Arena tmpArena = arenaRepository.getArena(collection.pavadinimas);

                if (tmpArena.pavadinimas!=null)
                {
                    ModelState.AddModelError("tabelis", "Arena su tokiu tabelio numeriu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                //Jei darbuotojo su tabelio nr neranda prideda naują
                if (ModelState.IsValid)
                {
                    arenaRepository.addArena(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Arena/Edit/5
        public ActionResult Edit(string id)
        {
            //grazinama darbuotojo redagavimo forma
            return View(arenaRepository.getArena(id));
        }

        // POST: Arena/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Arena collection)
        {
            try
            {
                // Atnaujina darbuotojo informacija
                if (ModelState.IsValid)
                {
                    arenaRepository.updateArena(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Arena/Delete/5
        public ActionResult Delete(string id)
        {
            return View(arenaRepository.getArena(id));
        }

        // POST: Arena/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                //if (arenaRepository.getArenaSutarciuCount(id)>0)
                //{
                //    naudojama = true;
                //    ViewBag.naudojama = "Arena turi sudarytų sutarčių, pašalinti negalima.";
                //    return View(arenaRepository.getArena(id));
                //}

                if (!naudojama)
                {
                    arenaRepository.deleteArena(id);
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
