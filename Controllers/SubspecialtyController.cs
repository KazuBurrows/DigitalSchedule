using CDHB_Official.Models;
using CDHB_Official.sakila;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDHB_Official.Controllers
{
    public class SubspecialtyController : Controller
    {
        DbAa1c85CdhbContext context_db = new DbAa1c85CdhbContext();

        public async Task<IActionResult> Index()
        {
            IQueryable<Subspecialty> Subspecialty_list = context_db.Subspecialties;

            var viewModel = new SubspecialtyViewModel
            {
                Subspecialties = await Subspecialty_list.ToListAsync()
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
        public async Task<IActionResult> Create(Subspecialty subspecialty)
        {

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                context_db.Add(subspecialty);
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






        // GET: Edit/Id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context_db.Subspecialties == null)
            {
                return NotFound();
            }

            var subspecialty = await context_db.Subspecialties.FindAsync(id);
            if (subspecialty == null)
            {
                return NotFound();
            }

            return View("Edit", subspecialty);
        }

        // POST: Edit/theatre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Subspecialty subspecialty)
        {

            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                context_db.Update(subspecialty);
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

            return View(subspecialty);
        }







        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context_db.Subspecialties == null)
            {
                return NotFound();
            }

            var subspecialty = await context_db.Subspecialties.FindAsync(id);
            if (subspecialty == null)
            {
                return NotFound();
            }
            return View("Delete", subspecialty);
        }

        // GET: Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var subspecialty = await context_db.Subspecialties.FindAsync(id);
            if (subspecialty != null)
            {
                context_db.Remove(subspecialty);
            }

            await context_db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
