using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class LiveSearchViewModel
    {
        public LiveSearchViewModel()
        {

        }

        public LiveSearchViewModel(Users user)
        {
            Firstname = user.Name;
            Lastname = user.LastName;
            Username = user.Username;
            UserId = user.Id;

        }

        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

    }
}