using CDHB_Official.sakila;

namespace CDHB_Official.Models
{
    public class ScheduleViewModel
    {
        public PaginatedList<Schedule>? TheatreSchedule { get; set; }


        public string? SortOrder { get; set; }

        public int? SearchWeek { get; set; }
        public string? SearchDay { get; set; }
        public string? SearchAMPM { get; set; }
        public int? SearchTheatreId { get; set; }
        public string? SearchTheatre { get; set; }
        public int? SearchSessionId { get; set; }
        public string? SearchSession { get; set; }
        public string? SearchFirstName { get; set; }
        public string? SearchLastName { get; set; }
        public string? SearchCode { get; set; }
        public string? SearchSpeciality { get; set; }
        public string? SearchSubSpeciality { get; set; }
        public string? SearchAnaestheticType { get; set; }
        public int? SearchAcute { get; set; }
        public int? SearchPediatric { get; set; }



    }

    public class MatrixCreateViewModel
    {
        public Theatre TempTheatre { get; set; }
        public int TempWeek { get; set; }
        public string TempDay { get; set; }
        public bool TempIsAm { get; set; }


        public sakila.Schedule? schedule { get; set; }
        public List<Session>? SessionSelector { get; set; }
        public List<Staff>? StaffSelector { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
        public List<Theatre>? TheatreSelector { get; set; }

        public List<string> DaySelector = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };


    }

    public class ScheduleCreateViewModel
    {
        public sakila.Schedule? schedule { get; set; }
        public List<Session>? SessionSelector { get; set; }
        public List<Staff>? StaffSelector { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
        public List<Theatre>? TheatreSelector { get; set; }

        public List<string> DaySelector = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    }


    public class EditViewModel
    {
        public string SessionViewType { get; set; }
        public sakila.Schedule? schedule { get; set; }
        public List<Session>? SessionSelector { get; set; }
        public List<Staff>? StaffSelector { get; set; }
        public List<Subspecialty>? SubspecialtySelector { get; set; }
        public List<Theatre>? TheatreSelector { get; set; }

        public List<string> DaySelector = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    }
}
