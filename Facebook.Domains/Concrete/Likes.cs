using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("Likes")]
   public  class Likes:BaseEntity
    {

        
        public Users User { get; set; }

        public int UserId { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }



    }
}
