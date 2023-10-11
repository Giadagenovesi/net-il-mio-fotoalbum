using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models.Db_Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'immagine deve per forza avere un titolo")]
        [MaxLength(100, ErrorMessage = "La massima lunghezza del titolo è di 100 caratteri")]
        public string Title { get; set; }

        [Required(ErrorMessage = "L'immagine deve per forza avere una descrizione")]
        public string Description { get; set; }

        public string Img { get; set; }

        public bool IsVisible { get; set; }

        // Creiamo la relazione N:N con le categorie
        public List<Category>? Categories { get; set; }

        public Image() {
        
        }

        public Image(string title, string description, string img) 
        { 
            this.Title = title;
            this.Description = description;
            this.Img = img;
        }

    }
}
