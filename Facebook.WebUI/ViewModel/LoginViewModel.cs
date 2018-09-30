using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class LoginViewModel
    {

        [Required]
        public string Username { get; set; }


        [Required]
        public string Passwword { get; set; }


    }
}