using FixIt.Models;
using FixIt.Services;
using Microsoft.AspNetCore.Mvc;

namespace FixIt.Controllers
{
    public class FixItController : Controller
    {
        private readonly IFixItService fixitService;
        public FixItController(IFixItService _fixitService)
        {
            fixitService = _fixitService;
        }
        public async Task<IActionResult> Index()
        {
            // hämta todo items
            var items = await fixitService.GetIncompleteAsync();
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
            var successful = await fixitService.AddItemAsync(newitem);
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
            var successful = await fixitService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }


    }
}
