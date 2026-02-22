# FeeeT - Etkinlik ve Toplantı Planlama Platformu

FeeeT, kullanıcıların etkinliklerini ve toplantılarını hızlıca planlamalarını sağlayan, katılımcıların en uygun zamanı oylayarak belirleyebildiği web tabanlı bir planlama uygulamasıdır. 

Proje, **N-Katmanlı Mimari (N-Tier Architecture)** prensiplerine uygun olarak geliştirilmiş olup, sürdürülebilir ve genişletilebilir bir kod yapısına sahiptir.

## 🚀 Özellikler

* **Kullanıcı Kimlik Doğrulaması:** Güvenli kayıt olma ve giriş yapma (Cookie Authentication).
* **Etkinlik Oluşturma:** Etkinlikler için başlık, açıklama, konum ve birden fazla tarih/saat seçeneği belirleyebilme.
* **Dinamik Oylama Sistemi:** Katılımcıların etkinlik linki üzerinden kendilerine en uygun tarih ve saatleri oylayabilmesi (AJAX ile sayfa yenilenmeden anlık güncelleme).
* **Kişisel Kontrol Paneli:** Kullanıcıların oluşturdukları etkinlikleri listeleyebileceği, düzenleyebileceği ve silebileceği özel profil sayfası.
* **Bağlantı Paylaşımı:** Oluşturulan etkinliklerin detay sayfalarının tek tıkla kopyalanıp katılımcılarla paylaşılabilmesi.

## 💻 Kullanılan Teknolojiler

**Backend:**
* C# & .NET 8.0
* ASP.NET Core MVC
* Entity Framework Core
* FluentValidation (Sunucu taraflı doğrulama)

**Frontend:**
* HTML5, CSS3, JavaScript
* Bootstrap 5
* jQuery & AJAX

**Veritabanı:**
* Microsoft SQL Server

**Mimari (Architecture):**
* Entity Layer (Varlık Katmanı)
* Data Access Layer (Veri Erişim Katmanı / Repository Pattern)
* Business Layer (İş Katmanı / Service Pattern)
* Core Layer (Çekirdek Katman)
* Presentation Layer (Sunum Katmanı - MVC)
