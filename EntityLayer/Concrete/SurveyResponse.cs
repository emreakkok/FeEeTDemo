using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SurveyResponse
    {
        [Key]
        public int Id { get; set; } // Yanıt Id
        public int UserId { get; set; } // Yanıt veren kullanıcı
        public User User { get; set; } // Yanıt veren kullanıcı
        public int DateTimeOptionId { get; set; } // Seçilen tarih ve saat seçeneği
        public DateTimeOption DateTimeOption { get; set; } // Seçilen tarih ve saat
    }
}
