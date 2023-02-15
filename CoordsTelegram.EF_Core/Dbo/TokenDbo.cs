using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoordsTelegram.EF_Core.Dbo
{
    public class TokenDbo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Key { get; set; }

        public string ChatId { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public DateTime Expired { get; set; }
    }
}
