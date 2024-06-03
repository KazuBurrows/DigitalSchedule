using CDHB_Official.Models;
using CDHB_Official.sakila;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace CDHB_Official.Controllers
{
    public class TheatreController : Controller
    {
        DbAa1c85CdhbContext context_db = new DbAa1c85CdhbContext();

        public async Task<IActionResult> Index()
        {
            IQueryable<Theatre> theatres = context_db.Theatres;


            var viewModel = new TheatreViewModel
            {
                Theatres = await theatres.ToListAsync()
            };

            return View(viewModel);
        }



        // GET: Create
        public async Task<IActionResult> Create()
        {
            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;

            var viewModel = new TheatreCreateViewModel
            {
                SubspecialtySelector = await subspecialties_list.ToListAsync()
            };
            return View("Create", viewModel);
        }

        // POST: Create/Theatre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Theatre theatre, List<string> Selected)
        {
            theatre.Specialties = "";
            Subspecialty s;
            foreach (var opId in Selected) {
                s = context_db.Subspecialties.Find(int.Parse(opId));
                theatre.Specialties += s.Speciality + " ";
                
            }


            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                context_db.Add(theatre);
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
            if (id == null || context_db.Theatres == null)
            {
                return NotFound();
            }

            var theatre = await context_db.Theatres.FindAsync(id);
            if (theatre == null)
            {
                return NotFound();
            }


            IQueryable<Subspecialty> subspecialties_list = context_db.Subspecialties;
            List<string> selected = new List<string>();
            string[] specialties = theatre.Specialties.Split(" ");
            foreach ( var x in specialties)
            {
                selected.Add(x);
            }

            var viewModel = new TheatreEditViewModel
            {
                theatre = theatre,
                //Selected = selected,
                SubspecialtySelector = await subspecialties_list.ToListAsync()
            };
            return View("Edit", viewModel);
        }

        // POST: Edit/theatre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Theatre theatre, List<string> Selected)
        {
            theatre.Specialties = "";
            Subspecialty s;
            foreach (var opId in Selected)
            {
                s = context_db.Subspecialties.Find(int.Parse(opId));
                theatre.Specialties += s.Speciality + " ";

            }


            if (ModelState.IsValid)     // If a required attribute/field in model is empty;
            {
                context_db.Update(theatre);
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

            return View(theatre);
        }







        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context_db.Theatres == null)
            {
                return NotFound();
            }

            var theatre = await context_db.Theatres.FindAsync(id);
            if (theatre == null)
            {
                return NotFound();
            }
            return View("Delete", theatre);
        }

        // GET: Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var theatre = await context_db.Theatres.FindAsync(id);
            if (theatre != null)
            {
                context_db.Remove(theatre);
            }

            await context_db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
