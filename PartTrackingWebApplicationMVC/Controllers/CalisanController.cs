using PartTrackingWebApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartTrackingWebApplicationMVC.Controllers
{
    public class IsDurum
    {
        public string isBaslik { get; set; }
        public string isAciklama { get; set; }
        public DateTime? iletilenTarih { get; set; }
        public DateTime? yapilanTarih { get; set; }
        public string durumAd { get; set; }
    }
    public class CalisanController : Controller
    {
        ParcaTakipDBEntities entity = new ParcaTakipDBEntities();
        // GET: Calisan
        public ActionResult Index()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);
            if (yetkiTurId == 2)
            {
                int birimId = Convert.ToInt32(Session["PersonelBirimId"]);
                var birim = (from b in entity.Birimler where b.birimId == birimId select b).FirstOrDefault();
                ViewBag.birimAd = birim.birimAd;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            } 
        }

        public ActionResult Yap()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);
            if (yetkiTurId == 2)
            {
                int personelId = Convert.ToInt32(Session["PersonelId"]);
                var isler = (from i in entity.Parcalar where i.isPersonelId == personelId && i.parcaDurumId == 1 select i)
                    .ToList().OrderByDescending(i=>i.iletilenTarih);
                ViewBag.isler = isler;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Yap(int parcaId)
        {
            var tekIs = (from i in entity.Parcalar where i.parcaId == parcaId select i).FirstOrDefault();
            tekIs.yapilanTarih = DateTime.Now;
            tekIs.parcaDurumId = 2;
            entity.SaveChanges();
            return RedirectToAction("Index", "Calisan");
        }

        public ActionResult Takip()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTürId"]);
            if (yetkiTurId == 2)
            {
                int personelId = Convert.ToInt32(Session["PersonelId"]);
                var isler = (from i in entity.Parcalar join d in entity.Durumlar on
                             i.parcaDurumId equals d.durumId where
                             i.isPersonelId == personelId select i).ToList().OrderByDescending(i =>i.iletilenTarih);
                
                IsDurumModel model = new IsDurumModel();
                List<IsDurum> list = new List<IsDurum>();

                foreach (var i in isler)
                {
                    IsDurum isDurum = new IsDurum();

                    isDurum.isBaslik = i.parcaIsmi;
                    isDurum.isAciklama = i.parcaDurumu;
                    isDurum.iletilenTarih = i.iletilenTarih;
                    isDurum.yapilanTarih = i.yapilanTarih;
                    isDurum.durumAd = i.Durumlar.durumAd;

                    list.Add(isDurum);
                }
                model.isDurumlar = list;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}