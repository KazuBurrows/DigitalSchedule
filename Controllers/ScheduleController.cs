using CDHB_Official.sakila;
using CDHB_Official.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Linq;
using CDHB_Official.Services;
using System.Collections.Generic;



namespace CDHB_Official.Controllers
{
    public class ScheduleController : Controller
    {
        DbAa1c85CdhbContext context_db = new DbAa1c85CdhbContext();
        public const string SessionViewType = "_ViewType";
        SearchService Search = new SearchService();
        int pageSize = 100;


        public async Task<IActionResult> Index(
            string sortOrder, int? searchWeek,
            string searchDay, string searchAMPM,
            int? searchTheatreId, string searchTheatre,
            int? searchSessionId, string searchSession,
            string searchFirstName, string searchLastName,
            string searchCode, string searchSpeciality,
            string searchSubSpeciality, string searchAnaestheticType,
            int? searchAcute, int? searchPediatric, int? pageNumber=1
            )
        {
            ViewBag.WeekSortParm = String.IsNullOrEmpty(sortOrder) ? "week_desc" : "";
            ViewBag.DaySortParm = sortOrder == "Day" ? "day_desc" : "Day";
            ViewBag.IsAmSortParm = sortOrder == "IsAm" ? "isAm_desc" : "IsAm";
            ViewBag.TimeStartSortParm = sortOrder == "TimeStart" ? "startTime_desc" : "TimeStart";
            ViewBag.TimeEndSortParm = sortOrder == "EndStart" ? "endTime_desc" : "EndStart";
            ViewBag.TheatreIdSortParm = sortOrder == "TheatreId" ? "theatreId_desc" : "TheatreId";
            ViewBag.TheatreNameSortParm = sortOrder == "TheatreName" ? "theatreName_desc" : "TheatreName";
            ViewBag.SessionIdSortParm = sortOrder == "SessionId" ? "sessionId_desc" : "SessionId";
            ViewBag.SessionNameSortParm = sortOrder == "SessionName" ? "sessionName_desc" : "SessionName";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "firstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastName_desc" : "LastName";
            ViewBag.CodeSortParm = sortOrder == "Code" ? "code_desc" : "Code";
            ViewBag.SpecialitySortParm = sortOrder == "Speciality" ? "speciality_desc" : "Speciality";
            ViewBag.SubSpecialitySortParm = sortOrder == "SubSpeciality" ? "subSpeciality_desc" : "SubSpeciality";
            ViewBag.AnaestheticTypeSortParm = sortOrder == "AnaestheticType" ? "anaestheticType_desc" : "AnaestheticType";
            ViewBag.IsAcuteSortParm = sortOrder == "IsAcute" ? "isAcute_desc" : "IsAcute";
            ViewBag.IsPediatricSortParm = sortOrder == "IsPediatric" ? "isPediatric_desc" : "IsPediatric";

            IQueryable<Schedule> schedule_list = context_db.Schedules;

            schedule_list = Search.Filter(schedule_list, searchWeek,
                                            searchDay, searchAMPM,
                                            searchTheatreId, searchTheatre,
                                            searchSessionId, searchSession,
                                            searchFirstName, searchLastName,
                                            searchCode, searchSpeciality,
                                            searchSubSpeciality, searchAnaestheticType,
                                            searchAcute, searchPediatric);

            schedule_list = Search.Sort(schedule_list, sortOrder);


            var operationVM = new ScheduleViewModel
            {
                //TheatreSchedule = await schedule_list.ToListAsync(),
                TheatreSchedule = await PaginatedList<Schedule>.CreateAsync(schedule_list.AsTracking(), pageNumber ?? 1, pageSize),
                SearchWeek = searchWeek,
                SearchDay = searchDay,
                SearchAMPM = searchAMPM,
                SearchTheatreId = searchTheatreId,
                SearchTheatre = searchTheatre,
                SearchSessionId = searchSessionId,
                SearchSession = searchSession,
                SearchFirstName = searchFirstName,
                SearchLastName = searchLastName,
                SearchCode = searchCode,
                SearchSpeciality = searchSpeciality,
                SearchSubSpeciality = searchSubSpeciality,
                SearchAnaestheticType = searchAnaestheticType,
                SearchAcute = searchAcute,
                SearchPediatric = searchPediatric,

                SortOrder = sortOrder,

            };

            HttpContext.Session.SetString(SessionViewType, "Schedule");
            return View(operationVM);
        }



        [HttpGet]
        public async Task<IActionResult> Matrix(int? week = 1)
        {
            MatrixViewModel MatrixView = new MatrixViewModel(week);

            HttpContext.Session.SetString(SessionViewType, "Matrix");
            return View(MatrixView);
        }





        // GET: Create
        public async Task<IActionResult> Create(int theatreId, int week, string day, bool isAm)
        {

            Console.WriteLine("CREATE: " + theatreId + " . " + week + " . " + day + " . " + isAm);

            Theatre theatre = context_db.Theatres.Find(theatreId);

            IQueryable<Session> session_list = context_db.Sessions;
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;
            IQueryable<Theatre> theatre_list = context_db.Theatres;

            

            if (HttpContext.Session.GetString(SessionViewType) == "Matrix")
            {
                var viewModel = new MatrixCreateViewModel
                {
                    TempTheatre = theatre,
                    TempWeek = week,
                    TempDay = day,
                    TempIsAm = isAm,

                    schedule = new sakila.Schedule() { Theatre = theatre, Week = week, Day = day, IsAm = isAm },
                    SessionSelector = await session_list.ToListAsync(),
                    StaffSelector = await staff_list.ToListAsync(),
                    SubspecialtySelector = await subspecialties_list.ToListAsync(),
                    TheatreSelector = await theatre_list.ToListAsync()
                };
                return View("~/Views/Schedule/Create.cshtml", viewModel);
            }
            else {
                var viewModel = new ScheduleCreateViewModel
                {
                    SessionSelector = await session_list.ToListAsync(),
                    StaffSelector = await staff_list.ToListAsync(),
                    SubspecialtySelector = await subspecialties_list.ToListAsync(),
                    TheatreSelector = await theatre_list.ToListAsync()
                };
                return View("~/Views/Schedule/ScheduleCreate.cshtml", viewModel);
            }

            
        }

        // POST: Create/Schedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule)
        {

            Console.WriteLine(schedule.TheatreId);
            Console.WriteLine(schedule.Week);
            Console.WriteLine(schedule.Day);

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                TimeSpan interval = new TimeSpan(12, 0, 0);
                schedule.IsAm = (schedule.TimeStart < interval) ? true : false;

                var session = await context_db.Sessions.FindAsync(schedule.SessionId);
                var theatre = await context_db.Theatres.FindAsync(schedule.TheatreId);
                schedule.Session = session;
                schedule.Theatre = theatre;

                context_db.Add(schedule);
                await context_db.SaveChangesAsync();

                if (HttpContext.Session.GetString(SessionViewType) == "Matrix")
                {
                    return RedirectToAction(nameof(Matrix));
                }
                return RedirectToAction(nameof(Index));
            }


            // If (ModelState.IsValid) fails print errors.
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            
            return RedirectToAction(nameof(Matrix));
        }



        // GET: Edit/Schedule.Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context_db.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await context_db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }


            // If fail return to current view.
            IQueryable<Session> session_list = context_db.Sessions;
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;
            IQueryable<Theatre> theatre_list = context_db.Theatres;

            var viewModel = new EditViewModel
            {
                SessionViewType = HttpContext.Session.GetString(SessionViewType),
                schedule = schedule,
                SessionSelector = await session_list.ToListAsync(),
                StaffSelector = await staff_list.ToListAsync(),
                SubspecialtySelector = await subspecialties_list.ToListAsync(),
                TheatreSelector = await theatre_list.ToListAsync()
            };
            return PartialView("~/Views/Schedule/Edit.cshtml", viewModel);
        }

        // POST: Edit/Schedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Schedule schedule)
        {

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                var session = await context_db.Sessions.FindAsync(schedule.SessionId);
                var theatre = await context_db.Theatres.FindAsync(schedule.TheatreId);
                schedule.Session = session;
                schedule.Theatre = theatre;

                context_db.Update(schedule);
                await context_db.SaveChangesAsync();

                if (HttpContext.Session.GetString(SessionViewType) == "Matrix")
                {
                    return RedirectToAction(nameof(Matrix));
                }
                return RedirectToAction(nameof(Index));
            }


            // If (ModelState.IsValid) fails print errors.
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // If fail return to current view.
            IQueryable<Session> session_list = context_db.Sessions;
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;
            IQueryable<Theatre> theatre_list = context_db.Theatres;

            var viewModel = new EditViewModel
            {
                schedule = schedule,
                SessionSelector = await session_list.ToListAsync(),
                StaffSelector = await staff_list.ToListAsync(),
                SubspecialtySelector = await subspecialties_list.ToListAsync(),
                TheatreSelector = await theatre_list.ToListAsync()
            };
            return View(viewModel);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context_db.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await context_db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/Schedule/Delete.cshtml", schedule);
        }

        // GET: Flights/Delete/Schedule.Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var schedule = await context_db.Schedules.FindAsync(id);
            if (schedule != null)
            {
                context_db.Remove(schedule);
            }

            await context_db.SaveChangesAsync();
            if (HttpContext.Session.GetString(SessionViewType) == "Matrix")
            {
                return RedirectToAction(nameof(Matrix));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
