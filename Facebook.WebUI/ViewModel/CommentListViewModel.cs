using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class CommentListViewModel
    {

        public int PostId { get; set; }

        public int UserId { get; set; }

        public int CommentId { get; set; }

        public string ComText { get; set; }

        public DateTime CreatedDate { get; set; }

        public string username { get; set; }


    }
}