using CDHB_Official.sakila;
using Microsoft.Data.SqlClient;
using NuGet.Packaging.Signing;

namespace CDHB_Official.Services
{
    public interface ISearchService
    {

        IQueryable<Schedule> Filter(IQueryable<Schedule> schedule_list, int? searchWeek,
                                    string searchDay, string searchAMPM,
                                    int? searchTheatreId, string searchTheatre,
                                    int? searchSessionId, string searchSession,
                                    string searchFirstName, string searchLastName,
                                    string searchCode, string searchSpeciality,
                                    string searchSubSpeciality, string searchAnaestheticType,
                                    int? searchAcute, int? searchPediatric);


        IQueryable<Schedule> Sort(IQueryable<Schedule> schedule_list, string sortOrder);
    }

}
