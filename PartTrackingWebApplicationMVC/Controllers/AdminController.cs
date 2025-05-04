using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartTrackingWebApplicationMVC.Models;

namespace PartTrackingWebApplicationMVC.Controllers
{
    public class AdminController : Controller
    {
        ParcaTakipDBEntities entity = new ParcaTakipDBEntities();
        // GET: Yonetici
        public ActionResult Index()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1002)
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
        public ActionResult Tanimla()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1002)
            {

                var birimler = (from b in entity.Birimler select b).ToList();
                ViewBag.birimler = birimler;

                var yetkiler = (from y in entity.YetkiTurler select y).ToList();
                ViewBag.yetkiler = yetkiler;
           

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        [HttpPost]
        public ActionResult Tanimla(FormCollection formCollection)
        {
            string personelAdSoyad = formCollection["personelAdSoyad"];
            string personelKullaniciAdi = formCollection["personelKullaniciAd"];
            string personelParola = formCollection["personelParola"];
            int personelBirimId = Convert.ToInt32(formCollection["personelBirimId"]);
            int personelYetki = Convert.ToInt16(formCollection["personelYetki"]);

            Personeller yeniPersonel = new Personeller();

            yeniPersonel.personelAdSoyad = personelAdSoyad;
            yeniPersonel.personelKullaniciAdi = personelKullaniciAdi;
            yeniPersonel.personelParola = personelParola;
            yeniPersonel.personelBirimId = personelBirimId;
            yeniPersonel.personelYetkiTurId = personelYetki;
         

            entity.Personeller.Add(yeniPersonel);
            entity.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Delete()
        {
            int iyetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);

            if (iyetkiTurId == 1002)
            {
                var birimler = (from b in entity.Birimler select b).ToList();
                ViewBag.birimler = birimler;

                var personeller = (from p in entity.Personeller select p).ToList();
                ViewBag.personeller = personeller;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Delete(FormCollection formCollection)
        {
          
            int birimId = Convert.ToInt32(formCollection["birimId"]);
            string secim = formCollection["personelSecimi"];
            string[] parcalar = secim.Split('-');

            int personelBirimId = Convert.ToInt32(parcalar[0]);
            int personelId = Convert.ToInt32(parcalar[1]);

            var silinecekPersonel = (from p in entity.Personeller
                                     where personelId == p.personelId
                                     select p).FirstOrDefault();


            if (birimId == personelBirimId && silinecekPersonel != null)
            {
                entity.Personeller.Remove(silinecekPersonel);
                entity.SaveChanges();
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı Adı veya Birimi Yanlış Seçtiniz";
                ViewBag.birimler = entity.Birimler.ToList();
                ViewBag.personeller = entity.Personeller.ToList();
                return View();
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}
