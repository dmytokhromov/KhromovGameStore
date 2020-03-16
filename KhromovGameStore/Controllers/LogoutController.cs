using System.Web.Mvc;

namespace KhromovGameStore.Controllers
{
    public class LogoutController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            Session["UserId"] = null;
            Session["UserTypeId"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}
