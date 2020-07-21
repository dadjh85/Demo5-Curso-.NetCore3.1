using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        public string CodeClient { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
