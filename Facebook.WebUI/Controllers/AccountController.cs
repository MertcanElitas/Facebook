using Facebook.Bussines.Abstract;
using Facebook.Bussines.Services;
using Facebook.Domains.Concrete;
using Facebook.WebUI.SessionSetting;
using Facebook.WebUI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Facebook.WebUI.Controllers
{
    public class AccountController : Controller
    {

        private IUserService userService;
        private IUserFriendService userFriendService;
        private IWallService wallservice;
        private IPostService postService;

        public AccountController()
        {
            userService = new UserService();
            userFriendService = new UserFriendService();
            wallservice = new WallService();
            postService = new PostService();

        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult CreateAccount()
        {
            return View();
        }

      [HttpPost]
        public ActionResult CreateAccount(UserViewModel userViewModel,HttpPostedFileBase file)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", userViewModel);
            }


            var model = userService.GetBy(x => x.Username == userViewModel.Username);

            if (model!=null)
            {
                ModelState.AddModelError("", "Username" + userViewModel.Username + "taken");
                userViewModel.Username = "";

                return View("Index", userViewModel);
            }


            Users u = new Users
            {
                Username = userViewModel.Username,
                Name = userViewModel.Firstname,
                LastName = userViewModel.LastName,
                Password = userViewModel.Password,
                Email = userViewModel.Email,
                IsDeleted = false
            };


            userService.Add(u);

            int userId = u.Id;


            SessionSet<Users>.SetSession(u, "login");


            var uploadsDir = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\"))); //Uploda Dosyasını bulabilmekl için assembly içinde Pathi verdik Yani: C/user/repos/Facebook.WebUI/Uploads....


            if (file!=null && file.ContentLength>0)
            {
                string ext = file.ContentType.ToLower(); //Content type ı yani image.jpg veya image.png gibi yakaladık ve küçük harfe çevirdik.Aşağıda kontrol edebilmek için



                if (ext!="image/jpg" && ext!="image/jpeg" && ext!="image/pjeg" && ext!="image/gif" && ext!="image/x-png" && ext!="image/png")
                {


                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    userViewModel.Username = "";

                    return View("Index", userViewModel);

                }

                string imagename = userId + ".jpg"; //Fotografin adını id le bağladık 3.jpg 2.jpg


                var path = string.Format("{0}\\{1}", uploadsDir, imagename); //Dosya pathını ve fotograf adını birleştirip bir butun path haline getirdik

                file.SaveAs(path); //Uplodas dosyasın kayıt etttik


                Wall wall = new Wall();


                wall.UserId = userId;
                wall.Message = "";
                wall.DateEdited = DateTime.Now;

                wallservice.Add(wall);

            }

            return Redirect("~/"+userViewModel.Username); //Yönlendirdik

        }


        //Get:/{username}
        public ActionResult Username(string username = "")
        {
            var model = userService.GetBy(x => x.Username == username);

            if (model==null)
            {
                return Redirect("~/");
            }

            //Viewbag.username
            ViewBag.username = username;


            //Login olan user name
            string user = SessionSet<Users>.CurrentUser("login").Username;


            //ViewBag Users Fullname
            Users user1 = userService.GetBy(x => x.Username == user); //Burda Sessiondan Ben Kendi Hesabımla Giriş Yaptığım için beni yakalıyo
            ViewBag.Fullname = user1.Name + " " + user1.LastName;


            int userId = user1.Id;


            //Viewbag Userıd
            ViewBag.UserId = userId;



            Users user2 = userService.GetBy(x => x.Username == username); //Burda Furkanın sayfasına yönlendirildiğim için furkanı yakalıyo ve onu getiriyo
            ViewBag.ViewingFullName = user2.Name + " " + user2.LastName;


            //Get usernams ımage
            ViewBag.userimage = model.Id + ".jpg";


            string userType = "guest";

            if (username==user)  //Parametre olarak gelen username ile Sessiomdaki username aynı ise kişi owner Değil seguest yani guest durumu:
            {                     //Merrtcan Frukanın sayfasında geziyo gibi
                userType = "owner";
            }

            ViewBag.userType = userType;



            if (userType=="guest")
            {

                Users u1 = userService.GetBy(x => x.Username == user); //Burası yine sessiondaki kulanıcı yani Mertcan 
                int id1 = u1.Id;


                Users u2 = userService.GetBy(x => x.Username == username); //Burasıda sayfasını açtığım kişi yani furkan
                int id2 = u2.Id;


                UserFriend f1 = userFriendService.GetBy(x => x.UserId == id1 && x.FriendId == id2); //mertcan furkanla ilişki 
                UserFriend f2 = userFriendService.GetBy(x => x.FriendId == id1 && x.UserId == id2); //furkan mertcanla ilişki

                if (f1==null && f2==null ) //İkiside null sa arkadaş değiller 
                {
                    ViewBag.NotFirends = "True";
                }


                if (f1 != null) //Sadece ilk sorgu varsa mertcan isteği atmış furkanı bekliyo
                {
                    if (!f1.Active)
                    {
                        ViewBag.NotFirends = "Pending";
                    }
                    
                }



                if (f2 != null) //Sadece ikincisi varsa Furkan atmış mertcani bekliyo
                {
                    if (!f2.Active)
                    {
                        ViewBag.NotFirends = "Pending";
                    }

                }


            }



            var friendCount = userFriendService.Query<UserFriend>().Count(x => x.FriendId == userId && x.Active==false);   //Bana Gelen Arkadaşlık istekllerinin sayısı için UserFriend tablosunda FriendId si Benim Idme eşit olan kayıtları yakaladık
                                                                                                                           //ZAten eğerki benim Id im  UserFriend Tablosunda Bir FriendId değeriyle Eşleişiyorsa bana bir istek Atılmış Demektir.
                                                                                                                           //Or neğin 2 - 1  burada bir ben 2 furkan , furkan bana istekte bulunmuş demektir.
            if (friendCount>0)
            {
                ViewBag.FrCount = friendCount;
            }


            Users u = userService.GetBy(x => x.Username == user); //Sessiondaki kullanıcıya erişildi
            int usernameId = u.Id;



            var friendCount2 = userFriendService.Query<UserFriend>().Count(x => x.FriendId == usernameId && x.Active == true /*|| x.UserId==usernameId && x.Active==true*/); //Arkadaş olduklarının sayısı alındı


            ViewBag.FCount = friendCount2;



            //ViewBag MessageCount
            var messageCount = userService.Query<Messages>().Count(x => x.ToId == usernameId);

            ViewBag.MsgCount = messageCount;



            //Viewbag user wall

            ViewBag.WallMessage = wallservice.GetAll(x => x.Id == userId).Select(x => x.Message).FirstOrDefault();


            //Viewbag friend walls

            List<int> friendlist = userFriendService.GetAll(x => x.UserId == userId && x.Active == true).ToArray().Select(x => x.FriendId).ToList();


            List<int> friendlist2 = userFriendService.GetAll(x => x.FriendId == userId && x.Active == true).ToArray().Select(x => x.UserId).ToList();


            List<int> allfriendsIds = friendlist.Concat(friendlist2).ToList();


            //List<WallViewModel> wallList = wallservice.GetAll(x => allfriendsIds.Contains(x.UserId)).ToArray().OrderByDescending(x => x.DateEdited).Select(x => new WallViewModel(x)).ToList();

            //List<PostListViewModel> postlist = postService.GetAll(x => x.UserId == userId).Select(x => new PostListViewModel
            //{

            //    CreatedDate = x.CreatedDate,
            //    Text = x.Text,
            //    LikeCount = x.LikeCount,
            //    //username=x.User.Username,
            //    // UserId=x.UserId


            //}).ToList();


            List<PostListViewModel> postlist = (from use in userService.Query<Users>() join post in userService.Query<Post>() on use.Id equals post.UserId where use.Id == userId select new PostListViewModel {

                username=use.Username,
                Text=post.Text,
                LikeCount=post.LikeCount,
                UserId=use.Id,
                CreatedDate=post.CreatedDate,
                postId=post.Id

                


            }).ToList();




            //List<PostListViewModel> postlist2 = postService.GetAll(x => allfriendsIds.Contains(x.UserId)).Select(x => new PostListViewModel(x)).ToList();


            var postlist2 = (from uf in userFriendService.Query<UserFriend>()
                             join p in userFriendService.Query<Post>() on uf.FriendId equals p.UserId
                             join use in userFriendService.Query<Users>() on p.UserId equals use.Id
                             where uf.UserId == userId && uf.Active==true
                             select new PostListViewModel
                             {

                                 Text = p.Text,
                                 CreatedDate = p.CreatedDate,
                                 username=use.Username,
                                 LikeCount = p.LikeCount,
                                 UserId=use.Id,
                                 postId=p.Id


                             }).ToList(); ;


            List<PostListViewModel> wallList = postlist.Concat(postlist2).OrderByDescending(x=>x.CreatedDate).ToList();


            ViewBag.Walls = wallList;

            return View();
        }

     
        public ActionResult Logout()
        {

            SessionSet<Users>.Remove("login");

            return Redirect("~/");
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            

            var model = userService.GetBy(x => x.Username == loginViewModel.Username && x.Password == loginViewModel.Passwword);

            if (model==null)
            {
                return View(loginViewModel);
            }
            else
            {

                SessionSet<Users>.SetSession(model, "login");

             
              return Redirect("~/"+model.Username);
            }
           
        }


        public ActionResult MyPage(Users user)
        {

           

            return View(user);
        }

    }
}