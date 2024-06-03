using CDHB_Official.Data;
using CDHB_Official.Models;
using CDHB_Official.sakila;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Data;

namespace CDHB_Official.Controllers
{
    public class StaffController : Controller
    {
        DbAa1c85CdhbContext context_db = new DbAa1c85CdhbContext();

        public async Task<IActionResult> Index()
        {
            IQueryable<Staff> Staff_list = context_db.Staffs;

            var viewModel = new StaffViewModel
            {
                Staffs = await Staff_list.ToListAsync()
            };
            return View(viewModel);
        }


        // GET: Create
        public async Task<IActionResult> Create()
        {

            return View("Create");
        }

        // POST: Create/Subspecialty
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staff staff)
        {

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                context_db.Add(staff);
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
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || context_db.Staffs == null)
            {
                return NotFound();
            }

            var staff = await context_db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }


            return View("Edit", staff);
        }

        // POST: Edit/Schedule
        [HttpPost]
        public async Task<IActionResult> Edit(Staff Staff)
        {
            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                context_db.Update(Staff);
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

            return View(Staff);
        }





        public async Task<IActionResult> Delete(int id)
        {

            if (id == null || context_db.Staffs == null)
            {
                return NotFound();
            }

            var staff = await context_db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View("Delete", staff);
        }

        // GET: Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var staff = await context_db.Staffs.FindAsync(id);
            if (staff != null)
            {
                context_db.Remove(staff);
            }

            await context_db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
