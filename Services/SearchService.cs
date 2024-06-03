using CDHB_Official.sakila;

namespace CDHB_Official.Services
{
    public class SearchService : ISearchService
    {
        public SearchService()
        {
        }

        public IQueryable<Schedule> Filter(IQueryable<Schedule> schedule_list, int? searchWeek, string searchDay, string searchAMPM, int? searchTheatreId, string searchTheatre, int? searchSessionId, string searchSession, string searchFirstName, string searchLastName, string searchCode, string searchSpeciality, string searchSubSpeciality, string searchAnaestheticType, int? searchAcute, int? searchPediatric)
        {
            if (searchWeek != null)
            {
                schedule_list = schedule_list.Where(s => s.Week.Equals(searchWeek));
            }

            if (searchDay != null)
            {
                schedule_list = schedule_list.Where(s => s.Day.Contains(searchDay));
            }

            if (searchAMPM != null)
            {
                var isAM = (searchAMPM.ToLower() == "am") ? true : false;
                schedule_list = schedule_list.Where(s => s.IsAm.Equals(isAM));
            }

            if (searchTheatreId != null)
            {
                schedule_list = schedule_list.Where(s => s.TheatreId.Equals(searchTheatreId));
            }

            if (!String.IsNullOrEmpty(searchTheatre))
            {
                schedule_list = schedule_list.Where(s => s.Theatre.Name.Contains(searchTheatre));
            }

            if (searchSessionId != null)
            {
                schedule_list = schedule_list.Where(s => s.SessionId.Equals(searchSessionId));
            }

            if (!String.IsNullOrEmpty(searchSession))
            {
                schedule_list = schedule_list.Where(s => s.Session.Name.Contains(searchSession));
            }

            if (!String.IsNullOrEmpty(searchFirstName))
            {
                schedule_list = schedule_list.Where(s => s.Session.Staff.FirstName.Contains(searchFirstName));
            }

            if (!String.IsNullOrEmpty(searchLastName))
            {
                schedule_list = schedule_list.Where(s => s.Session.Staff.LastName.Contains(searchLastName));
            }

            if (!String.IsNullOrEmpty(searchCode))
            {
                schedule_list = schedule_list.Where(s => s.Session.Subspecialty.Code.Contains(searchCode));
            }

            if (!String.IsNullOrEmpty(searchSpeciality))
            {
                schedule_list = schedule_list.Where(s => s.Session.Subspecialty.Speciality.Contains(searchSpeciality));
            }

            if (!String.IsNullOrEmpty(searchSubSpeciality))
            {
                schedule_list = schedule_list.Where(s => s.Session.Subspecialty.SubSpeciality.Contains(searchSubSpeciality));
            }

            if (!String.IsNullOrEmpty(searchAnaestheticType))
            {
                schedule_list = schedule_list.Where(s => s.Session.AnaestheticType.Contains(searchAnaestheticType));
            }

            if (searchAcute != null)
            {
                var isAM = (searchAMPM.ToLower() == "acute") ? true : false;
                schedule_list = schedule_list.Where(s => s.Session.IsAcute.Equals(searchAcute));
            }

            if (searchPediatric != null)
            {
                var isAM = (searchAMPM.ToLower() == "peds") ? true : false;
                schedule_list = schedule_list.Where(s => s.Session.IsPediatric.Equals(searchPediatric));
            }


            return schedule_list;
        }



        public IQueryable<Schedule> Sort(IQueryable<Schedule> schedule_list, string sortOrder)
        {
            switch (sortOrder)
            {
                case "week_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Week);
                    break;
                case "day_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Day);
                    break;
                case "Day":
                    schedule_list = schedule_list.OrderBy(s => s.Day);
                    break;
                case "isAm_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.IsAm);
                    break;
                case "IsAm":
                    schedule_list = schedule_list.OrderBy(s => s.IsAm);
                    break;
                case "theatreId_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Theatre.Id);
                    break;
                case "TheatreId":
                    schedule_list = schedule_list.OrderBy(s => s.Theatre.Id);
                    break;
                case "theatreName_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Theatre.Name);
                    break;
                case "TheatreName":
                    schedule_list = schedule_list.OrderBy(s => s.Theatre.Name);
                    break;
                case "sessionId_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.Id);
                    break;
                case "SessionId":
                    schedule_list = schedule_list.OrderBy(s => s.Session.Id);
                    break;
                case "sessionName_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.Name);
                    break;
                case "SessionName":
                    schedule_list = schedule_list.OrderBy(s => s.Session.Name);
                    break;
                case "startTime_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.TimeStart);
                    break;
                case "TimeStart":
                    schedule_list = schedule_list.OrderBy(s => s.TimeStart);
                    break;
                case "endTime_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.TimeEnd);
                    break;
                case "EndStart":
                    schedule_list = schedule_list.OrderBy(s => s.TimeEnd);
                    break;
                case "firstName_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.Staff.FirstName);
                    break;
                case "FirstName":
                    schedule_list = schedule_list.OrderBy(s => s.Session.Staff.FirstName);
                    break;
                case "lastName_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.Staff.LastName);
                    break;
                case "LastName":
                    schedule_list = schedule_list.OrderBy(s => s.Session.Staff.LastName);
                    break;
                case "code_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.Subspecialty.Code);
                    break;
                case "Code":
                    schedule_list = schedule_list.OrderBy(s => s.Session.Subspecialty.Code);
                    break;
                case "subSpeciality_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.Subspecialty.SubSpeciality);
                    break;
                case "SubSpeciality":
                    schedule_list = schedule_list.OrderBy(s => s.Session.Subspecialty.SubSpeciality);
                    break;
                case "anaestheticType_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.AnaestheticType);
                    break;
                case "AnaestheticType":
                    schedule_list = schedule_list.OrderBy(s => s.Session.AnaestheticType);
                    break;
                case "isAcute_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.IsAcute);
                    break;
                case "IsAcute":
                    schedule_list = schedule_list.OrderBy(s => s.Session.IsAcute);
                    break;
                case "isPediatric_desc":
                    schedule_list = schedule_list.OrderByDescending(s => s.Session.IsPediatric);
                    break;
                case "IsPediatric":
                    schedule_list = schedule_list.OrderBy(s => s.Session.IsPediatric);
                    break;

                default:
                    schedule_list = schedule_list.OrderBy(s => s.Week);
                    break;
            }

            return schedule_list;
        }
    }
}
