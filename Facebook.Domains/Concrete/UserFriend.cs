using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("UserFriend")]
   public class UserFriend:BaseEntity
    {

        public Users User { get; set; }

        public int UserId { get; set; }

        public Users Friend { get; set; }

        public int FriendId { get; set; }

        public bool Engelle { get; set; }

        public bool Active { get; set; }


    }
}
