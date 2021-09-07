using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class OrganizacijaController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        OrganizacijaRepository organizacijaRepository = new OrganizacijaRepository();
        // GET: organizacija
        public ActionResult Index()
        {
            //grazinamas markiu sarašas
            return View(organizacijaRepository.getOrganizacijos());
        }

        // GET: organizacija/Create
        public ActionResult Create()
        {
            Organizacija organizacija = new Organizacija();
            return View(organizacija);
        }

        // POST: organizacija/Create
        [HttpPost]
        public ActionResult Create(Organizacija collection)
        {
            try
            {
                // išsaugo nauja organizacija duomenų bazėje
                if (ModelState.IsValid)
                {
                    organizacijaRepository.addOrganizacija(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: organizacija/Edit/5
        public ActionResult Edit(string id)
        {
            return View(organizacijaRepository.getOrganizacija(id));
        }

        // POST: organizacija/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Organizacija collection)
        {
            try
            {
                // atnajina organizacijas informacija
                if (ModelState.IsValid)
                {
                    organizacijaRepository.updateOrganizacija(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: organizacija/Delete/5
        public ActionResult Delete(string id)
        {
            return View(organizacijaRepository.getOrganizacija(id));
        }

        // POST: organizacija/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;
                //if (organizacijaRepository.getOrganizacijaCount(salis)>0)
                //{
                //    naudojama = true;
                //    ViewBag.naudojama = "Negalima pašalinti yra sukurtų modelių su šia organizacija.";
                //    return View(organizacijaRepository.getOrganizacija(salis));
                //}

                if (!naudojama)
                {
                    organizacijaRepository.deleteOrganizacija(id);
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
