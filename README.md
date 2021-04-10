# ReCapProject Bir Araç Kiralama Projesidir

## Başlamadan Önce

---

<br>

## Bilgilendirme

<b>
- Bu proje, İsmail KAYGISIZ tarafından Engin DEMİROĞ'un ücretsiz olarak yaptığı Yazılım Geliştirici Yetiştirme Kampı'nda verilen ödevler sonucunda geliştirilmiştir.
</b>

<br>
<br>

[Yazılım Geliştirici Yetiştirme Kampı](https://www.youtube.com/playlist?list=PLqG356ExoxZVN7rC0KmMo0lvECK97VRZg)

[Kodlama.io](https://www.kodlama.io/)

---

<br>

## Gereklilikler

---

<br>

- ### Sürümler

  - Angular CLI 11.2.3.
  - .Net Core 3.1
  - .Net Standart 2.0

    Sürümlerle ilgili ayrıntıyı bilgiye projelerin içinde bulunan .csproj dosyalarından ve [Dependencies](https://github.com/ismailkaygisiz/ReCapProject/network/dependencies) üzerinden erişebilirsiniz.

- ### Uygulamalar

  - <b>BackEnd</b>

    .Net Core 3.1 ve .Net Core 2.0 derleyebilecek herhangi bir geliştirme ortamı.

  - <b>FrontEnd</b>

    Angular CLI 11.2.3 derleyebilecek herhangi bir geliştirme ortamı.

---

<br>

## BackEnd için Yapılması Gerekenler

---

<br>

- ### Veritabanı Etkinleştirme

  DbScriptAndInfos klasörünün altında bulunan script.sql mevcut veritabanı üzerinde çalıştırılmalıdır. (SQLServer)
  Eğer aynı isimde veritabanı varsa script içinde değişiklik yapılmalıdır. Veritabanı ismi veya server ismi farklıysa `DataAccess/Concrete/ReCapProjectContext` dosyasındaki   veriler buna göre değiştirilmelidir.

  <br>

- ### Projeyi Çalıştırma

  Klasör içinde bulunan .sln uzantılı dosya açılmalıdır daha sonra WebAPI başlangıç projesi
  olarak işaretlenmeli ve proje build edildikten sonra çalıştırılmalıdır. Eğer önünüzde kullanıcılar varsa tebrikler
  artık API çalışıyor. Açılan sekmedeki port adresini kopyalayın.

---

<br>

## FrontEnd için Yapılması Gerekenler

<br>

- ### Angular Projesini API' ye Entegre Etme

  Dosya yolu üzerinden AngularUI dizini içinde bulunan ReCapProject projesini Angular çalıştırabileceğiniz bir editörle açın. Daha sonra `src/api.ts` dosyasına giderek kopyaladığınız port adresini gerekli yerlere yapıştırın. Bu işlemi yaparken apiUrl değişkeninde `/api/` ifadesinin olmasına dikkat edin.
  örn : `http://localhost:44311/api/`

  <br>

- ### Angular Projesi için Gerekli Modülleri Kurma

  Angular dosya dizini içinde yeni bir terminal oluşturup `npm install` komutunu yazmanız yeterlidir Angular proje için gerekli paketleri kuracaktır.

  <br>

- ### Angular Projesini Çalıştırma

  Kurulum işlemi bittikten sonra yapmanız gereken terminale `ng serve --open --port 4200` komutunu yazmak olacaktır. Eğer bu port meşgulse veya çalışmıyorsa port adresini değiştirebilirsiniz. Port adresini değiştirdiğinizde `WebAPI/Startup.cs` içindeki

  `app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());`

  kısmını düzenlemeniz gerekir sadece url değiştirmeniz yeterli olacaktır bu işlemden sonra WebAPI'yi durdurup tekrar çalıştırmanız gerekecektir.

---

<br>

### Eğer Bütün Bu Adımları Başarıyla Tamamladıysanız ve Proje Çalışıyorsa Tebrikler Artık Diğer Adımlara Geçebilirsiniz

<br>

---

<br>

## Ana Sayfa

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Home%20Page.png"></center>

<br>

Bütün işlemleri doğru tamamladığınızda önünüze yukarıdaki sayfa gelmelidir.
Sol tarafta bulunan ilk listeden herhangi bir marka seçtiğinizde o markaya ait araçlar listelenecektir.

Sol tarafta bulunan ikinci listeden herhangi bir renk seçtiğinizde o renge ait araçlar listelenecektir.

Bunların dışında arama çubuğunun yanında bulunan filtrelerden marka ve renk seçerek hem marka hem de renk için filtre uygulayabilirsiniz.

Arama çubuğu marka, renk, günlük ücret ve model yılı için olan aramaları desteklemektedir. Arama çubuğuna herhangi bir şey yazdığınızda filtrelemeyi otomatik yapacaktır.

<br>

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Search%20for%20Brand.png"></center>

<br>

---

## Mevcut Markayı ve Rengi Güncelleme / Silme

<br>

Bu işlemleri yapmadan önce admin yetkisiyle sisteme giriş yapılmalıdır bunun için `DbScriptAndInfos` klasörü içinde bulunan `Informations.txt` dosyasından faydalanabilirsiniz. Eğer herhangi bir hesapla giriş yapmazsanız sizi otomatik giriş yap ekranına yönlendirecektir.

Üst tarafta yer alan Listele butonuna tıkladığınızda önünüze 4 seçenek çıkacaktır.

- Markalar
- Renkler
- Kiralamalar
- Müşteriler

Markalar veya Renkler butonlarından herhangi birine basarak listelendikleri sayfaya ulaşabilirsiniz. Açılan bu sayfa üzerinden seçtiğiniz herhangi bir markanın veya rengin detay sayfasına yönlendirilirsiniz. Sonrasında ise eğer yetkiniz varsa rengi veya markayı güncelleyebilir ya da silebilirsiniz. Aynı isimde birden fazla renk ya da marka varsa veya yetkiniz yoksa sistem hata verip sizi ana sayfaya yönlendirecektir.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Brand%20Update.png"></center>

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Color%20Update.png"></center>

---

<br>

## Yeni Marka, Renk ve Araba Ekleme

Üst tarafta yer alan Ekle butonuna bastığınızda karşınıza 3 seçenecek çıkacaktır.

- Marka
- Renk
- Araba

Eğer giriş yaptıysanız bunlardan herhangi birini seçtiğinizde seçtiğiniz nesnenin ekleme sayfasına yönlendirilirsiniz

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Car%20Add.png"></center>

<br>

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Brand%20Add.png"></center>

<br>

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Color%20Add.png"></center>

<br>

Ekle butonuna bastığınızda yetkiniz yoksa hata alarak ana sayfaya yönlendirilirsiniz. Yetkiniz varsa ve gerekli kurallara uyarsanız ekleme işlemi başarıyla gerçekleşir. Aracı ilk eklediğinizde herhangi bir resmi olmaz.

---

<br>

## Araba Kiralama, Güncelleme ve Silme

Ana sayfa üzerinden herhangi bir aracın detaylarını görmek için Detayları Gör butonuna basmanız yeterlidir.

<br>

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Car%20Details.png"></center>

<br>

Aracı silmek için Sil butonuna basmanız yeterlidir. Butona bastığınızda karşınıza aşağıdaki gibi bir ekran çıkacaktır.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Car%20Delete.png"></center>

Sil butonuna bastığınızda eğer yetkiniz varsa aracı siler yetkiniz yoksa hata verir ve sizi ana sayfaya yönlendirir.

---

<br>

Aracın detay sayfasındayken Güncelle butonuna bastığınızda güncelleme ekranına yönlendirilirsiniz.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Car%20Update.png"></center>

Sadece yetkiniz varken araçları güncelleyebilirsiniz.
Eğer aracın mevcut resmi yoksa ve resim eklemeden Güncelle butonuna basarsanız araca ön tanımlı olan resmi atayacaktır. Her resim eklemek istediğinizde aracı güncellemeniz gerekmektedir. Her aracın en fazla 5 resmi bulunabilir. Daha sonra bu resmi değiştirmek istediğinizde güncelle sayfasındayken resmin üzerine tıklamanız yeterlidir. Bu işlemden sonra resmin detay sayfasına yönlendirileceksiniz.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Image%20Update%20and%20Delete.png"></center>

Eğer aracın 1 adet resmi varsa resim silme işlemi başarısız olacaktır. Dosya seç kısmından yeni bir resim seçerek güncelleye bastığınızda mevcut resmin güncellenmiş olduğunu göreceksiniz. Bu işlemi bütün resimler için yapabilirsiniz.

---

<br>

Arabayı kiralamak istediğinizde aracın detayları sayfasında bulunan Kirala butonuna basmanız gerekmektedir.

Eğer giriş yapmışsanız bu butona bastıktan sonra tarih seçme sayfasına yönlendirileceksiniz.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Car%20Rental.png"></center>

Burada tarih seçerken dikkat etmeniz gereken geri dönüş tarihinin kiralama tarihinden sonrası olmasıdır ve geri kiralama tarihiyle geri dönüş tarihinin aynı olmamasıdır.
Bu aşamayı da başarıyla geçtikten sonra ödeme sayfasına yönlendirileceksiniz.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Payment%20Page.png"></center>

Bu kısımda gerekli alanları doldurabilir ya da daha önceden kayıtlı olan kredi kartınız varsa onu kullanabilirsiniz.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/Saved%20Cards.png"></center>

Kayıtlı kartlardan herhangi birini seçtiğinizde otomatik olarak alanları dolduracaktır.

Eğer kayıtlı kartınız yoksa ve kartı kaydetmek istiyorsanız Ödeme Yap butonuna basmadan önce Kart bilgilerimi kaydet kutucuğunu işaretlemeniz gerekmektedir.

Ödeme yap butonuna bastıktan sonra bütün işlemler yolunda giderse aracı kiralamış olacaksınız.

Eğer findeks puanınız yetersiz ise aracı kiralayamadan ana sayfaya yönlendirilirsiniz.

---

<br>

## Kullanıcı Güncelleme ve Silme

Eğer giriş yapmışsanız üst tarafta sağda yer alan butonda sisteme kayıtlı olduğunuz ismi göreceksiniz.

Eğer giriş yapmadıysanız Merhaba Kullanıcı yazısını göreceksiniz.

İsminizin yazdığı butona bastığınızda karşınıza Profil ve Çıkış Yap seçenekleri çıkacaktır.

Profil butonuna bastığınızda Profil sayfasına yönlendirilirsiniz.

<center><img src="https://raw.githubusercontent.com/ismailkaygisiz/ReCapProject/master/Images/User%20Update%20and%20Delete.png"></center>

Buradan kullanıcıyı doğrudan silebilirsiniz. Bu işlemi yaptıktan sonra tekrar giriş yapmanız eğer mevcut hesabınız yoksa tekrar kayıt olmanız gerekmektedir (Admin kullanıcısını silmemelisiniz eğer silerseniz yetkiyi el ile atamak durumunda kalırsınız).

Güncellemek istediğinizde ise değerleri değiştirip Güncelle butonuna basmanız gerekmektedir.

Findeks Puanı kullanıcı tarafından değiştirilemez. Her kullanıcı kayıt olduğunda sistem tarafından 200 ile 350 arasında olacak şekilde rastgele bir findeks puanı verilir. Kullanıcı her araç kiraladığında bu puan aracın fiyatına göre belirli bir oranda artar minimum değeri 0 maksimum değeri 1900'dür.
