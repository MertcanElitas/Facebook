using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete.Base
{
    public interface IBaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         int Id { get; set; }

         DateTime CreatedDate { get; set; }

         bool IsDeleted { get; set; }




    }
}
