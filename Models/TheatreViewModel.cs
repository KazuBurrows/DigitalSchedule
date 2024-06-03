using CDHB_Official.sakila;
using System.ComponentModel;

namespace CDHB_Official.Models
{
    public class TheatreViewModel
    {
        public List<Theatre>? Theatres { get; set; }
    }


    public class TheatreCreateViewModel
    {
        public Theatre? theatre { get; set; }
        [DisplayName("Specialties")]
        public List<string> Selected { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
    }

    public class TheatreEditViewModel
    {
        public Theatre? theatre { get; set; }
        [DisplayName("Specialties")]
        public List<string> Selected { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
    }
}
