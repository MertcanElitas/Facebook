using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("Walls")]
    public class Wall:BaseEntity
    {

        public string Message { get; set; }

        public DateTime DateEdited { get; set; }

        public virtual Users User { get; set; }

        public int UserId { get; set; }




    }
}
