using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.WebUI.ViewModel
{
    public class WallViewModel
    {
        public WallViewModel()
        {

        }

        public WallViewModel(Wall wall) //Biz select le atama işlemi yaparken yazıcağımıza burda bir kere yazıyoruz
        {
            Id = wall.UserId;
            Message = wall.Message;
            DateEdited = wall.DateEdited;

        }

        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateEdited { get; set; }


    }
}