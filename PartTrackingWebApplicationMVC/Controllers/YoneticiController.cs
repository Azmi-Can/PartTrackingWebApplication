using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartTrackingWebApplicationMVC.Models;

namespace PartTrackingWebApplicationMVC.Controllers
{
    public class YoneticiController : Controller
    {

        ParcaTakipDBEntities entity = new ParcaTakipDBEntities();
        // GET: Yonetici
        public ActionResult Index()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1)
            {
                int iPersonelBirimId = Convert.ToInt32(Session["PersonelBirimId"]);

                var birim = (from b in entity.Birimler where b.birimId == iPersonelBirimId select b).FirstOrDefault();

                ViewBag.birimAd = birim.birimAd;
                return View();
            }
            else
            {
                return View();
            }


        }

        public ActionResult Ata()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1)
            {

                int iPersonelBirimId = Convert.ToInt32(Session["PersonelBirimId"]);

                var personeller = (from p in entity.Personeller where p.personelBirimId == iPersonelBirimId && p.personelYetkiTurId == 2 select p).ToList();
                ViewBag.personeller = personeller;

                var birim = (from b in entity.Birimler where b.birimId == iPersonelBirimId select b).FirstOrDefault();

                ViewBag.birimAd = birim.birimAd;
                return View();
            }
            else
            {
                return RedirectToAction("ındex", "Login");
            }

        }
        [HttpPost]
        public ActionResult Ata(FormCollection formCollection)
        {
            string parcaBaslik = formCollection["parcaBaslik"];
            string parcaAciklama = formCollection["parcaAciklama"];
            int selectPer = Convert.ToInt16(formCollection["selectPer"]);

            Parcalar yeniParca = new Parcalar();

            yeniParca.parcaIsmi = parcaBaslik;
            yeniParca.parcaDurumu = parcaAciklama;
            yeniParca.isPersonelId = selectPer;
            yeniParca.iletilenTarih = DateTime.Now;
            yeniParca.parcaDurumId = 1;

            entity.Parcalar.Add(yeniParca);
            entity.SaveChanges();

            return RedirectToAction("Takip", "Yonetici");
        }

        public ActionResult Takip()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1)
            {

                int iPersonelBirimId = Convert.ToInt32(Session["PersonelBirimId"]);

                var personeller = (from p in entity.Personeller where p.personelBirimId == iPersonelBirimId && p.personelYetkiTurId == 2 select p).ToList();
                ViewBag.personeller = personeller;

                var birim = (from b in entity.Birimler where b.birimId == iPersonelBirimId select b).FirstOrDefault();

                ViewBag.birimAd = birim.birimAd;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Takip(int selectPer)
        {
            var secilenPersonel = (from p in entity.Personeller where p.personelId == selectPer select p).FirstOrDefault();
            TempData["secilen"] = secilenPersonel;  
            return RedirectToAction("Listele", "Yonetici");
        }

        [HttpGet]
        public ActionResult Listele()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1)
            {
                Personeller secilenPersonel = (Personeller)TempData["secilen"];
                var isler = (from i in entity.Parcalar where i.isPersonelId == secilenPersonel.personelId select 
                             i). ToList().OrderByDescending(i=>i.iletilenTarih);

                ViewBag.isler = isler;
                ViewBag.personel = secilenPersonel;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


    }
}