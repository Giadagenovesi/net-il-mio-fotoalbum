using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models.Db_Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private PhotoBookContext _myDb;

        public MessagesController(PhotoBookContext db)
        {
            _myDb = db;
        }

        [HttpPost]

        public IActionResult SendMessage([FromBody]Message message)
        {
            _myDb.Add(message);
            _myDb.SaveChanges();
            return Ok(message);
        }
    }
}
