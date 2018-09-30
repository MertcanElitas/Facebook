using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("Post")]
   public  class Post:BaseEntity
    {

        public string Text { get; set; }

        public string Imagepath { get; set; }

       

        public int LikeCount { get; set; }

       


        public int? ParentId { get; set; }



        
     
        //public Users User { get; set; }

        public int UserId { get; set; }

       

        public  List<Likes> Likes { get; set; }



    }
}
