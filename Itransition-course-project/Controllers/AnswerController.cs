using Microsoft.AspNetCore.Mvc;

namespace Itransition_course_project.Controllers
{
    public class AnswerController : Controller
    {
        // GET
        public IActionResult Right()
        {
            return View();
        }
        public IActionResult Wrong()
        {
            return View();
        }
    }
}