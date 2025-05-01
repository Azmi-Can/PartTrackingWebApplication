using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartTrackingWebApplicationMVC.Models;

namespace PartTrackingWebApplicationMVC.Controllers
{
    public class LoginController : Controller
    {
        ParcaTakipDBEntities entity = new ParcaTakipDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            //hata mmesaji basarken kullanılı
            ViewBag.messaj = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kullaniciAd, string parola)
        {
            var personel = (from p in entity.Personeller where p.personelKullaniciAdi == kullaniciAd 
                            && p.personelParola == parola select p).FirstOrDefault();
            if (personel != null)
            {
                // kullaniciya özel verileri sucu tarafında geçici olarak saklamak 
                // tarayıcı kapatılana ya da oturum süresi dolana kadar açık kalana kadar bu verileri saklayacağız
                // Bu sayayede, her sayfada istediğinde tekrar tekrar veritabanından çekilmek zorunda kalmaz
                Session["PersonelAdSoyad"] = personel.personelAdSoyad;
                Session["PersonelId"] = personel.personelId;
                Session["PersonelBirimId"] = personel.personelBirimId;
                Session["PersonelYetkiTürId"] = personel.personelYetkiTurId;

                // iki farklı yetki türü olduğu için farklı sayfalara yönlendirme yapacağız

                switch(personel.personelYetkiTurId)
                {
                    case 1:
                        return RedirectToAction("Index", "Yonetici");
                    case 2:
                        return RedirectToAction("Index", "Calisan");
                    default:
                        return View();
                }
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı Adınız veya Şifreniz HATALI!";
                return View();
            }

           

        }
    }
}