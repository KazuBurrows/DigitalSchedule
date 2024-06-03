using CDHB_Official.sakila;

namespace CDHB_Official.Models
{
    public class SessionViewModel
    {
        public List<Session>? Sessions { get; set; }
    }


    public class SessionCreateViewModel
    {
        public Session? session { get; set; }
        public List<Staff>? StaffSelector { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
        public List<string>? AnaestheticTypeSelector { get; set; }
    }


    public class SessionEditViewModel
    {
        public Session? session { get; set; }
        public List<Staff>? StaffSelector { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
        public List<string>? AnaestheticTypeSelector { get; set; }
    }
}
