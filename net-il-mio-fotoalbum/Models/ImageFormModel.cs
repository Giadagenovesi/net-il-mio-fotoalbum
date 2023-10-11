using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Models.Db_Models;

namespace net_il_mio_fotoalbum.Models
{
    public class ImageFormModel
    {
        
        public Image Image { get; set; }
       

        //Gestisco le proprietà  per gestire la select multipla nelle view
        public List<SelectListItem>? Categories { get; set; }
        public List<string>? SelectedCategoriesId { get; set; }


    }
}
