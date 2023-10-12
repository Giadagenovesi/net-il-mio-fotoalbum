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
        public bool IsVisible { get; set; }
        public string? ImageUrl { get; set; }

        public byte[]? ImageFile { get; set; }

        public string ImageSrc => ImageFile is null ? (ImageUrl is null ? "" : ImageUrl) : $"data:image/png;base64, {Convert.ToBase64String(ImageFile)}";


        // Creiamo la relazione N:N con le categorie
        public List<Category>? Categories { get; set; }

        public Image() {
        
        }

        public Image(string title, string description, string image) 
        { 
            this.Title = title;
            this.Description = description;
            this.ImageUrl = image;
        }

    }
}
