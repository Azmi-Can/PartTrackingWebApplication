Boş bir **MVC** şablonu oluşturdum ve db bağlantısını entity data model EF designer ile bağlantısını yaptım. 

* Farklı table'lar oluşturarak, __id iler ilişkilendirdim. Personeller(yönetici, calisan tipinde iki calışan bulunmaktadır)
Yönetici görevi --> parça atama, iş listeleme, takip yapabilmektedir.
Çalışan --> iş tamamlama, işlerini takip etme 
![image](https://github.com/user-attachments/assets/148947f5-159a-4a83-a381-b5cdae387165)

* Controller kısmı; login kontrol etme, logout  kontrol etme, yönetici ve çalışan kontrolleri vardır.
* view kısmında bootsrapt kütüphaneisni kullandım
* ![image](https://github.com/user-attachments/assets/75af280e-a929-451b-8805-99a40bec7b0b)
  
![image](https://github.com/user-attachments/assets/cdd75284-5d16-49a5-8817-5abe62e41472)


![image](https://github.com/user-attachments/assets/1b0e1e87-9552-48b2-9f60-8487cc6d98ef)

İş ata tıklayınca alttaki ekran gelmektedir ve burada yöneticiye bağlı olan kişleri seçerek parça ismi, açıklaması yazılarak DB de bulunan 
parçalar table güncellenmektedir. (entity.Parcalar.Add(yeniParca) entity.SaveChanges())
![image](https://github.com/user-attachments/assets/66d4a025-46a6-4df7-9062-869ca8d85b16)

Parça takip'e basınca görevlendirdiğimiz kişiler çıkıyor ve işleri lisetele tuşunu basınca
![image](https://github.com/user-attachments/assets/6a021a1b-cac5-4860-810d-2b861844adda)

![image](https://github.com/user-attachments/assets/787ca609-f3ab-4348-b53e-28919141d8d0)

Çalışan kullanıcı adıyla giriş yapınca, alttaki ekran gelmektedir.
![image](https://github.com/user-attachments/assets/d5569bd2-75df-4eec-80f9-67ef2d8208ff)

iş tamamla tıkladıktan sonra şuanda iş durumu 1 (yapılıyor) olanları göstermektedir.Tamamla tuşunu basılırsa iş durumu 2 (yapıldı)
![image](https://github.com/user-attachments/assets/289cb342-5f9a-4c4c-80ab-fe19060df0ef)

iş takip basılırsa eğer şuana kadar üzerindeki işleri datetimenow sıralanır.

![image](https://github.com/user-attachments/assets/6a417171-c322-406f-8152-6173932a3da5)


Son güncelleme ile Admin Sayfası oluşturuldu.

![image](https://github.com/user-attachments/assets/5af349c4-d9c0-4fa3-9711-16f010376621)

Kişi Atama sayfasındaki boşluklar ve Select'ler seçilerek form doldurulu. Yeni kişi db aktarılır.
![image](https://github.com/user-attachments/assets/d95e2c6a-d129-429f-877d-4d87e51ed3ed)

Kişi Sil sayfasında ise seçilen birim ve kullanıcı seçilir.
![image](https://github.com/user-attachments/assets/d1d4a257-abaa-4d37-8f6c-ef6b39a2f31e)

Eğer kullanıcı ve birim uyuşmazsa hata vermektedir.
![image](https://github.com/user-attachments/assets/93ea74de-62fb-46e2-bd7b-84d90ee8dffd)

Doğru kullanıcı ve birim seçilirse, db'den kişi kaldırılır.





