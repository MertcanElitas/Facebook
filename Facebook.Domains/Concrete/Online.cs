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
    [Table("Online")]
    public class Online:BaseEntity
    {
        public virtual Users User { get; set; }

        public int UserId { get; set; }

        [MaxLength(100)]
        public string ConnId { get; set; }

    }
}
