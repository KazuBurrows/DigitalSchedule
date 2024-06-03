using CDHB_Official.Models;
using CDHB_Official.sakila;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDHB_Official.Controllers
{
    public class SessionController : Controller
    {
        DbAa1c85CdhbContext context_db = new DbAa1c85CdhbContext();
        List<string> AnaestheticTypes = new List<string>() { "General", "Regional", "Local", "Other" };

        public async Task<IActionResult> Index()
        {
            IQueryable<Session> session_list = context_db.Sessions;

            var viewModel = new SessionViewModel
            {
                Sessions = await session_list.ToListAsync()
            };

            return View(viewModel);
        }



        // GET: Create
        public async Task<IActionResult> Create()
        {
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;

            var viewModel = new SessionCreateViewModel
            {
                StaffSelector = await staff_list.ToListAsync(),
                SubspecialtySelector = await subspecialties_list.ToListAsync(),
                AnaestheticTypeSelector = AnaestheticTypes
            };

            return View("Create", viewModel);
        }

        // POST: Create/Session
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Session session)
        {

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                var staff = await context_db.Staffs.FindAsync(session.StaffId);
                var subspecialty = await context_db.Subspecialties.FindAsync(session.SubspecialtyId);
                session.Staff = staff;
                session.Subspecialty = subspecialty;

                context_db.Add(session);
                await context_db.SaveChangesAsync();

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
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;

            var viewModel = new SessionCreateViewModel
            {
                StaffSelector = await staff_list.ToListAsync(),
                SubspecialtySelector = await subspecialties_list.ToListAsync(),
                AnaestheticTypeSelector = AnaestheticTypes
            };

            return View(viewModel);
        }






        // GET: Edit/Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context_db.Sessions == null)
            {
                return NotFound();
            }

            var session = await context_db.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            // If fail return to current view.
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;

            var viewModel = new SessionEditViewModel
            {
                session = session,
                StaffSelector = await staff_list.ToListAsync(),
                SubspecialtySelector = await subspecialties_list.ToListAsync(),
                AnaestheticTypeSelector = AnaestheticTypes
            };

            return View("Edit", viewModel);
        }

        // POST: Edit/theatre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Session session)
        {

            

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                var staff = await context_db.Staffs.FindAsync(session.StaffId);
                var subspecialty = await context_db.Subspecialties.FindAsync(session.SubspecialtyId);
                session.Staff = staff;
                session.Subspecialty = subspecialty;

                context_db.Update(session);
                await context_db.SaveChangesAsync();

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
            IQueryable<Staff> staff_list = context_db.Staffs;
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;

            var viewModel = new SessionEditViewModel
            {
                session = session,
                StaffSelector = await staff_list.ToListAsync(),
                SubspecialtySelector = await subspecialties_list.ToListAsync(),
                AnaestheticTypeSelector = AnaestheticTypes
            };

            return View(viewModel);
        }







        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context_db.Sessions == null)
            {
                return NotFound();
            }

            var session = await context_db.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return View("Delete", session);
        }

        // GET: Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var session = await context_db.Sessions.FindAsync(id);
            if (session != null)
            {
                context_db.Remove(session);
            }

            await context_db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
