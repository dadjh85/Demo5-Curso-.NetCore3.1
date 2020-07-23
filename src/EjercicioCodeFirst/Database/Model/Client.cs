using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Client
    {
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string CodeClient { get; set; }
        [MaxLength(150), Required]
        public string ClientName { get; set; }

        #region Relations Properties

        public int IdUser { get; set; }

        #endregion


        #region Relations

        public User UserNavigation { get; set; }

        #endregion

    }
}
