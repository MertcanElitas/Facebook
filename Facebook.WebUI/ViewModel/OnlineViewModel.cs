using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class OnlineViewModel
    {

        public OnlineViewModel()
        {

        }

        public OnlineViewModel(Online online)
        {
            UserId = online.UserId;
            ConnId = online.ConnId;

        }


        public int UserId { get; set; }

        public string ConnId { get; set; }


    }
}