using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("Comments")]
   public class Comment:BaseEntity
    {

        public string Body { get; set; }

      

        public int PostId { get; set; }

       

        public int UserId { get; set; }




    }
}
