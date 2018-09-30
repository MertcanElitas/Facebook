using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("Messages")]
   public class Messages:BaseEntity
    {


        public virtual Users From { get; set; }

        public int FromId { get; set; }

        public virtual Users To { get; set; }

        public int ToId { get; set; }

        public DateTime Datasent { get; set; }


        [MaxLength(500)]
        public string message { get; set; }

        public bool Read { get; set; }




    }
}
