using Facebook.Domains.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class PostListViewModel
    {

        public PostListViewModel()
        {

        }


        public int postId { get; set; }
        public string Text { get; set; }

        public int LikeCount { get; set; }


        public int UserId { get; set; }

        public string username { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}