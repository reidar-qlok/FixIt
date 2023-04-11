using Microsoft.AspNetCore.Mvc;

namespace FixIt.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            // hämta todo items

            //Skicka items till en modell

            //Skicka resultatet till en view
            return View();
        }
    }
}
