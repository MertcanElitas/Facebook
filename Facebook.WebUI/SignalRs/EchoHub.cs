using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Facebook.Bussines.Abstract;
using Facebook.Bussines.Services;
using Facebook.Domains.Concrete;
using Facebook.WebUI.SessionSetting;
using Facebook.WebUI.ViewModel;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace Facebook.WebUI.SignalRs
{
    [HubName("echo")]
    public class EchoHub : Hub
    {
        private IUserService userService;
        private IUserFriendService userfriendsrevice;
        private IMessageService messageService;
        private IOnlineService onlineService;
        private IPostService postservice;
        public EchoHub()
        {
            userService = new UserService();
            userfriendsrevice = new UserFriendService();
            messageService = new MessageService();
            onlineService = new OnlineService();
            postservice = new PostService();
        }


       
        public void Hello(string message)
        {
            //Clients.All.hello();
            Trace.WriteLine(message);

            var client = Clients.All;

            client.test("SelamınAleyküm Signalr");

        }

        public void Notify(string friend)
        {

            Users users = userService.GetBy(x => x.Username == friend);
            int frinedId = users.Id;


            var friendCount = userfriendsrevice.Query<UserFriend>().Count(x => x.FriendId == frinedId && x.Active==false);

            var client = Clients.Others;

            client.frnotify(friend,friendCount);


        }


        public void GetFrcount() //Sadece Anlık olarak gözüken arkadaşlık bildirimleri alanındaki sayıyı güncellemek içşn yazıldı. Furkan 0 .mertcan kabul ettiği an .Furkan 1
        {
            Users u = userService.GetBy(x => x.Id == 4);  //Sessiondaki user bulundu
            Users user = userService.GetBy(x => x.Username == u.Username);  //User ı user tablosundan çekerek teyit ettik
            int userid = user.Id;


            var friendReqCount = userfriendsrevice.Query<UserFriend>().Count(x => x.FriendId == userid && x.Active == false);

            var clients = Clients.Caller;  //Caller da geri dönüş sadece login olan usera yani sadece kendine dönyo

            clients.frcount(u.Username, friendReqCount);


        }


        public void GetFcount(int friendid)
        {

            Users u = userService.GetBy(x => x.Id == 4); //Sessiondaki user yani furkan
            

            //Session daki user
            Users user = userService.GetBy(x => x.Username == u.Username);
            int userId = user.Id;


            //sessiondaki userin arkadaş sayısı
            var friendCount1 = userService.Query<UserFriend>().Count(x => x.UserId == userId && x.Active == true /*|| x.FriendId == userId && x.Active == true*/); //Furkanın Arkadaş sayısı


            Users user2 = userService.GetBy(x => x.Id == friendid); //Arkadaşlık isteğini atan kişi Mertcanıda yakaladık

            string username = user2.Username; //Mertcanın Username i


            var friendcount2 = userfriendsrevice.Query<UserFriend>().Count(x => x.UserId == friendid && x.Active == true /*|| x.FriendId == friendid && x.Active == true*/); //Mertcanın Arkadaş sayısı


            var clients = Clients.All; //Tüm istemciler için işlemi gerçekleştir


            clients.fcount(user.Username, username, friendCount1, friendcount2); //Html taraafında yakalıyacağımız method


        }


        public void NotifyOfMessage(string friend) //Viewbagusername bilgisiyle gelen kişinin okunmamış mesaj sayısı 
        {

            Users user = userService.GetBy(x => x.Username == friend);
            int friendId = user.Id;


            var messagecount = messageService.Query<Messages>().Count(x => x.ToId == friendId && x.Read == false);


            var clients = Clients.Others;

            clients.msgcount(friend, messagecount);



        }


        public void Removepost(string dv)
        {

            var clients = Clients.All;

            clients.deletep(dv);

        }



        public void NotifyOfMessegeOwner() //Mesajları Okumak için tıklandığı anda  Sessiondakii kullanıcının okunmamış mesajlarının sayısının bulunduğu fonksiyon
        {


           

            Users u = userService.GetBy(x => x.Username =="mertcan" );
            int userId = u.Id;


            var messageCount = userService.Query<Messages>().Count(x => x.ToId == userId && x.Read==false);

            var clients = Clients.Caller;

            clients.msgcount(u.Username, messageCount);


        }


        public void Guncelwall(int id)
        {

            List<int> friendlist = userfriendsrevice.GetAll(x => x.UserId == id && x.Active == true).ToArray().Select(x => x.FriendId).ToList();


            List<int> friendlist2 = userfriendsrevice.GetAll(x => x.FriendId == id && x.Active == true).ToArray().Select(x => x.UserId).ToList();


            List<int> allfriendsIds = friendlist.Concat(friendlist2).ToList();


            //List<WallViewModel> wallList = wallservice.GetAll(x => allfriendsIds.Contains(x.UserId)).ToArray().OrderByDescending(x=>x.DateEdited).Select(x=> new WallViewModel(x)).ToList();

            List<PostListViewModel> postlist = (from use in userService.Query<Users>()
                                                join post in userService.Query<Post>() on use.Id equals post.UserId
                                                where use.Id == id
                                                select new PostListViewModel
                                                {

                                                    username = use.Username,
                                                    Text = post.Text,
                                                    LikeCount = post.LikeCount,
                                                    UserId = use.Id,
                                                    CreatedDate = post.CreatedDate,
                                                    postId=post.Id


                                                }).ToList();


            //List<PostListViewModel> postlist2 = postservice.GetAll(x => allfriendsIds.Contains(x.UserId)).Select(x => new PostListViewModel(x)).ToList();


            var postlist2 = (from uf in userfriendsrevice.Query<UserFriend>()
                             join p in userfriendsrevice.Query<Post>() on uf.FriendId equals p.UserId
                             join use in userfriendsrevice.Query<Users>() on p.UserId equals use.Id
                             where uf.UserId == id && uf.Active == true
                             select new PostListViewModel
                             {

                                 Text = p.Text,
                                 CreatedDate = p.CreatedDate,
                                 username = use.Username,
                                 LikeCount = p.LikeCount,
                                 UserId = use.Id,
                                 postId=p.Id
                                 

                             }).ToList(); ;


            List<PostListViewModel> wallList = postlist.Concat(postlist2).ToList();

            PostListViewModel model = wallList.OrderByDescending(x => x.CreatedDate).Take(1).FirstOrDefault();



            var clients = Clients.All;

            clients.guncelwallpost(model.UserId,model.Text,model.CreatedDate,model.username,model.postId);

        }



      

      //public override Task OnConnected()
      //  {

        //      Trace.WriteLine("Here I Am" + Context.ConnectionId);


        //      //Users user = SessionSet<Users>.CurrentUser("login");
        //      Users user = userService.GetBy(x => x.Id == 3);
        //      int userId = user.Id;

        //      //Get Conn İd
        //      string connId = Context.ConnectionId;

        //      //Any belirtilen koşula uygun kayıt varsa true veya false döner
        //      //if (onlineService.Query<Online>().Any(x => x.Id == userId))
        //      //{
        //          Online online = new Online();

        //          online.UserId = userId;
        //          online.ConnId = connId;

        //          onlineService.Add(online);
        //      //}




        //      //get All Online ids

        //      List<int> onlindeIds = onlineService.GetAll().ToArray().Select(x => x.UserId).ToList();

        //      //Get friend Ids

        //      List<int> friendlist = userfriendsrevice.GetAll(x => x.FriendId == userId && x.Active == true).ToArray().Select(x => x.UserId).ToList();


        //      List<int> friendlist2 = userfriendsrevice.GetAll(x => x.UserId == userId && x.Active == true).ToArray().Select(X => X.FriendId).ToList();


        //      List<int> allfriends = friendlist.Concat(friendlist2).ToList();


        //      //get final set of ıds

        //      //List<int> resultlist = onlindeIds.Where((x) => allfriends.Contains(x)).ToList();

        //      //Create a dict of friend ids and usernames

        //      Dictionary<int, string> disctfriends = new Dictionary<int, string>();

        //      foreach (var id in allfriends)
        //      {
        //          var users = userService.GetBy(x => x.Id == id);
        //          string friends = users.Username;

        //          if (!disctfriends.ContainsKey(id))
        //          {
        //              disctfriends.Add(id, friends);
        //          }





        //      }

        //      var transformed = from key in disctfriends.Keys select new { id = key, friend = disctfriends[key] };



        //      string json = JsonConvert.SerializeObject(transformed);



        //      var clients = Clients.Caller;



        //      clients.getonlinefriends(user.Username, json);


        //      //UpdateChat();

        //      return base.OnConnected();
        //  }


        //public override Task OnDisconnected(bool stopCalled)
        //{


        //    Online online = onlineService.GetBy(x => x.UserId == 3);
        //    int id = online.Id;
        //    onlineService.Delete(id);


        //    UpdateChat();




        //    return base.OnDisconnected(stopCalled);
        //}


        //public void UpdateChat()
        //{

        //    List<int> onlineIds = onlineService.Query<Online>().ToArray().Select(x => x.UserId).ToList();






        //    foreach (var userid in onlineIds)
        //    {
        //        Users u = userService.GetBy(x => x.Id == userid);
        //        string username = u.Username;


        //        List<int> friendlist = userfriendsrevice.GetAll(x => x.FriendId == userid && x.Active == true).Select(x => x.UserId).ToList();


        //        List<int> friendlist2 = userfriendsrevice.GetAll(x => x.UserId == userid && x.Active == true).Select(X => X.FriendId).ToList();


        //        List<int> allfriends = friendlist.Concat(friendlist2).ToList();

        //        //List<int> resultlist = onlindeIds.Where((x) => allfriends.Contains(x)).ToList();


        //        Dictionary<int, string> disctfriends = new Dictionary<int, string>();

        //        foreach (var id in allfriends)
        //        {
        //            var users = userService.GetBy(x => x.Id == id);
        //            string friends = users.Username;

        //            if (!disctfriends.ContainsKey(id))
        //            {
        //                disctfriends.Add(id, friends);
        //            }





        //        }

        //        var transformed = from key in disctfriends.Keys select new { id = key, friend = disctfriends[key] };



        //        string json = JsonConvert.SerializeObject(transformed);

        //        var clients = Clients.All;


        //        clients.updatechat(username, json);
        //    }

        //}


    }
}