using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int Id { get; set; } // Kullanıcı Id
        public string Username { get; set; } // Kullanıcı adı
        public string Email { get; set; } // E-posta adresi
        public string Password { get; set; } // Şifre
        public DateTime CreatedAt { get; set; } // Hesap oluşturulma tarihi
       
        public List<Event> CreatedEvents { get; set; } // Kullanıcının oluşturduğu etkinlikler
        public List<Event> ParticipatedEvents { get; set; } // Kullanıcının katıldığı etkinlikler
        public List<SurveyResponse> SurveyResponses { get; set; } // Kullanıcının verdiği anket yanıtları
    }

}
