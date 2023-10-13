using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models.Db_Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class ImagesController : ControllerBase
    {

        private PhotoBookContext _myDatabase;

        public ImagesController(PhotoBookContext db)
        {
            _myDatabase = db;
        }

        //Faccio una chiamata API che mi restituisce la lista di tutte le mie foto
        [HttpGet]
        public IActionResult GetImages()
        {

            List<Image> images = _myDatabase.Images.Include(image => image.Categories).ToList();

            return Ok(images);

        }

        //Faccio una chiamata API che mi restituisce la lista di tutte le mie foto che contengono quella determinata stringa nel titolo
        [HttpGet]
        public IActionResult GetImagesByTitle(string? research)
        {
            if (research == null)
            {
                return BadRequest();
            }



            List<Image> imagesResult = _myDatabase.Images.Include(image => image.Categories).Where(image => image.Title.ToLower().Contains(research.ToLower())).ToList();

            return Ok(imagesResult);

        }

    }
}
