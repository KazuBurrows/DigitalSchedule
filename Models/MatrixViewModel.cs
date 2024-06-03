using CDHB_Official.sakila;
using System.Collections.Generic;
using System.Globalization;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace CDHB_Official.Models
{
    public class MatrixViewModel
    {
        private DbAa1c85CdhbContext context_db = new DbAa1c85CdhbContext();



        public int ViewWeek { get; set; }
        public List<Location>? Matrix = new List<Location>();

        private int CalculateWeekOffset(int? week) {
            DateTime today = DateTime.Now;
            Calendar cal = new CultureInfo("en-US").Calendar;
            week = (week == null) ? cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, DayOfWeek.Monday) : week;       // If null return today's view week (e.g 08 Jan == week 2)

            return (int)week;
        }

        public MatrixViewModel(int? week)
        {
            ViewWeek = CalculateWeekOffset(week);
            IQueryable<Session> session_list = context_db.Sessions;
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;
            IQueryable<Theatre> theatre_list = context_db.Theatres;


            // POPULATE List<Location>? Matrix WITH UNIQUE FACILITY FROM THEATREs.
            List<string> locations = context_db.Theatres.Select(t => t.Facility).Distinct().ToList();
            locations.ForEach(l => Matrix.Add(new Location(l)));


            Location location;
            Room room;
            // POPULATE A Location IN List<Location>? Matrix WITH UNIQUE THEATRE FROM THEATREs WHERE THEATRE.FACILITY MATCHES A Location IN List<Location>? Matrix.
            List<sakila.Theatre> rooms = context_db.Theatres.Select(r => r).Distinct().ToList();
            foreach (var r in rooms)
            {
                room = new Room(r);
                // MATCH Location NAME FROM List<Location>? Matrix WITH THEATRE.FACILITY
                location = Matrix.Find(f => f.Name == r.Facility);
                location.LocationTable.Add(room);
            }


            // GET ALL SCHEDULES WHERE S.WEEK MATCHES VIEWWEEK.
            // POPULATE Room BY FINDING FACILITY THEN,
            // FINDING Room THEN,
            // ADDING SCHEDULE TO Room
            List<sakila.Schedule> weekSchedules = context_db.Schedules.Where(s => s.Week == ViewWeek).ToList();
            foreach (var schedule in weekSchedules)
            {
                location = Matrix.Find(f => f.Name == schedule.Theatre.Facility);
                room = location.LocationTable.Find(t => t.Theatre.Name == schedule.Theatre.Name);

                room.Add(schedule);
            }
        }
    }


    public class Location
    {
        public string Name { get; set; }
        public List<Room> LocationTable { get; set; }

        public Location(string name)
        {
            Name = name;
            LocationTable = new List<Room>();
        }
    }


    public class Room
    {
        public Theatre Theatre { get; set; }
        public sakila.Schedule[,] TheatreSchedule { get; set; }

        public Room(Theatre t)
        {
            Theatre = t;
            TheatreSchedule = new sakila.Schedule[,] { { null, null, null, null, null, null, null },
                                                       { null, null, null, null, null, null, null } };
        }

        public void Add(sakila.Schedule schedule)
        {
            int i = (schedule.IsAm) ? 0 : 1;
            int j = schedule.Day.ToLower() switch
            {
                "monday" => 0,
                "tuesday" => 1,
                "wednesday" => 2,
                "thursday" => 3,
                "friday" => 4,
                "saturday" => 5,
                "sunday" => 6,
                _ => 0
            };

            TheatreSchedule[i, j] = schedule;
        }
    }




}
