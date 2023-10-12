using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models.Db_Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "Inserire il testo del messaggio")]
        public string TextMessage { get; set; }
    }
}
