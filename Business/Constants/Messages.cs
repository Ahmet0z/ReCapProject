using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string BrandAdded = "Model eklendi";
        public static string BrandDeleted = "Model silindi";
        public static string BrandUpdated = "Model güncellendi";
        public static string BrandsListed = "Modeller Listelendi";

        public static string CarAdded = "Araba eklendi";
        public static string CarDeleted = "Araba silindi";
        public static string CarUpdated = "Araba güncellendi";
        public static string CarsListed = "Arabalar Listelendi";
        public static string CarNameInvalid = "Araç ismi geçersiz";
        public static string CarDetailBrought = "Araç detayı getirildi";
        public static string CarNotFound = "Araç bulunamadı.";

        public static string ColorAdded = "Renk eklendi";
        public static string ColorDeleted = "Renk silindi";
        public static string ColorUpdated = "Renk güncellendi";
        public static string ColorsListed = "Renkler Listelendi";

        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string UserGetted = "Kullanıcı getirildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";

        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string CustomerUpdated = "Müşteri güncellendi";
        public static string CustomersListed = "Müşteriler Listelendi";

        public static string RentalAdded = "Kiralama bilgisi eklendi";
        public static string RentalDeleted = "Kiralama bilgisi silindi";
        public static string RentalUpdated = "Kiralama bilgisi güncellendi";
        public static string RentalsListed = "Kiralama bilgileri Listelendi";
        public static string RentalDetailsListed = "Kiralama Detayları Listelendi";
        public static string CarIsOnRent = "Araç şuan bir başkası tarafından kiralanmıştır";

        public static string ImageAdded = "Resim eklendi";
        public static string ImageDeleted = "Resim silindi";
        public static string ImageUpdated = "Resim güncellendi";
        public static string ImagessListed = "Resimler Listelendi";
        public static string CarImageLimitExceded = "Bir aracın en fazla 5 fotoğrafı olabilir.";
        public static string NotFoundImage = "Resim bulunamadı";

        public static string ClaimsListed = "Roller Listelendi";

        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access Token başarıyla oluşturuldu";

        public static string AuthorizationDenied = "Yetkiniz bulunmamaktadır.";

    }
}
