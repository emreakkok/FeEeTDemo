using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Event
    {
        [Key]
        public int Id { get; set; } // Etkinlik Id
        public string Title { get; set; } // Etkinlik başlığı
        public string Description { get; set; } // Etkinlik açıklaması
        public DateTime CreatedAt { get; set; } // Etkinlik oluşturulma tarihi
        public string Location { get; set; } // Etkinlik konumu
        public int CreatedById { get; set; } // Etkinliği oluşturan kullanıcı
        public User CreatedBy { get; set; } // Etkinliği oluşturan kullanıcı
        
        public List<DateTimeOption> DateTimeOptions { get; set; } // Etkinlikle ilişkili tarih ve saat seçenekleri
        public List<User> Participants { get; set; } // Katılımcılar
    }
}
