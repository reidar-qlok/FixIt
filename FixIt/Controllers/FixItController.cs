using FixIt.Models;
using FixIt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FixIt.Controllers
{
    [Authorize]
    public class FixItController : Controller
    {
        private readonly IFixItService fixitService;
        private readonly UserManager<IdentityUser> usermanager;
        public FixItController(IFixItService _fixitService, UserManager<IdentityUser> _usermanager)
        {
            fixitService = _fixitService;
            usermanager = _usermanager;
        }
        public async Task<IActionResult> Index()
        {
            // hämta påloggad user
            var currentuser = await usermanager.GetUserAsync(User);
            // hämta todo items
            var items = await fixitService.GetIncompleteAsync(currentuser);
            //Skicka items till en modell
            var model = new ToDoViewModel()
            {
                Items = items
        };
            //Skicka resultatet till en view
            return View(model);
        }
        public async Task<IActionResult> AddItem(ToDoItem newitem)
        {
            var currentuser = await usermanager.GetUserAsync(User);
            var successful = await fixitService.AddItemAsync(newitem, currentuser);
            if (!successful)
            {
                return BadRequest("Could not add this item");
            }
            return RedirectToAction("Index");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var currentuser = await usermanager.GetUserAsync(User);
            var successful = await fixitService.MarkDoneAsync(id, currentuser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }


    }
}
