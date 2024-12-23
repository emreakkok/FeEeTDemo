using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DateTimeOption
    {
        [Key]
        public int Id { get; set; } // Seçenek Id
        public DateTime DateTime { get; set; } // Seçilen tarih ve saat
        public int EventId { get; set; } // Seçeneğin ait olduğu etkinlik
        public Event Event { get; set; } // Etkinlik
        public List<SurveyResponse> SurveyResponses { get; set; } // Katılımcıların bu seçeneğe verdiği yanıtlar
    }
}
