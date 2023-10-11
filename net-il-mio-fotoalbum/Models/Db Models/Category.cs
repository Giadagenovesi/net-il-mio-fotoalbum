using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models.Db_Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo della categoria è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Il titolo della categoria non può superare i 50 caratteri")]
        public string Title { get; set; }

        // Creiamo la relazione N:N con le immagini
        public List<Image>? Images { get; set; }


        public Category()
        {

        }
    }
}
