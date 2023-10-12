using Microsoft.AspNetCore.Mvc;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult SendMessage()
        {
            return View("SendMessage");
        }
    }
}
