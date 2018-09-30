using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class FriendRequestViewModel
    {

        public FriendRequestViewModel()
        {

        }


        public FriendRequestViewModel(UserFriend userFriend)
        {

            UserId = userFriend.UserId;
            FriendId = userFriend.FriendId;
            Active = userFriend.Active;

        }


        public int UserId { get; set; }
        public int FriendId { get; set; }
        public bool Active { get; set; }



    }
}