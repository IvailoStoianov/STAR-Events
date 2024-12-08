using Microsoft.AspNetCore.Mvc;

namespace STAREvents.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("Error404");
                case 500:
                    return View("Error");
                default:
                    return View("Error");
            }
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
