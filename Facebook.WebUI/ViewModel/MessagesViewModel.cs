using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class MessagesViewModel
    {

        public MessagesViewModel()
        {

        }


        public MessagesViewModel(Messages row)
        {
            Id = row.Id;
            From = row.FromId;
            To = row.ToId;
            Message = row.message;
            DataSent = row.Datasent;
            Read = row.Read;
            FromUsername = row.From.Username;
            FromFirstname = row.From.Name;
            FromLastname = row.From.LastName;




        }


        public int Id { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public string Message { get; set; }

        public DateTime DataSent { get; set; }

        public bool Read { get; set; }

        public int FromId { get; set; }

        public string FromUsername { get; set; }

        public string FromFirstname { get; set; }

        public string FromLastname { get; set; }





    }
}