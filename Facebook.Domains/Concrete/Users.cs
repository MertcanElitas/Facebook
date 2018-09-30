using Facebook.Domains.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete
{
    [Table("Users")]
   public  class Users:BaseEntity
    {

        public string Name { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Imagepath { get; set; }


       // public List<Post> Posts { get; set; }

        public List<Likes> Likes { get; set; }

        public List<Users> Friends { get; set; }

        //public List<Comment> Comment { get; set; }


    }
}
