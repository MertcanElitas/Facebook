using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class UserViewModel
    {

        public UserViewModel()
        {

        }

        public UserViewModel(Users user)
        {
            Id = user.Id;
            Firstname = user.Name;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Password;
           
            Username = user.Username;
        }


        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string UplodaName { get; set; }

    }
}