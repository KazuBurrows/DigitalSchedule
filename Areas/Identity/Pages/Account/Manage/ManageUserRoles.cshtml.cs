// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;


namespace CDHB_Official.Areas.Identity.Pages.Account.Manage
{
    public class ManageUserRolesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public ManageUserRolesModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<IdentityUser>? AllAdmins { get; set; }
        public IList<IdentityUser>? AllMembers { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            AllAdmins = await _userManager.GetUsersInRoleAsync("Admin");
            AllMembers = await _userManager.GetUsersInRoleAsync("Member");
            return Page();
        }



        // POST: Edit/Schedule
        [HttpPost]
        public async Task<IActionResult> OnPostEditAsync(string? Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "Member");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "Member");
                await _userManager.AddToRoleAsync(user, "Admin");
            }



            return RedirectToPage();
        }


        // POST: Edit/Schedule
        [HttpPost]
        public async Task<IActionResult> OnPostDeleteAsync(string? Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            try
            {
                await _userManager.DeleteAsync(user);
            }
            catch {
                Console.WriteLine("Failed to delete user.");
            }
            



            return RedirectToPage();
        }
    }
}
