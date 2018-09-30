using Facebook.Bussines.Abstract;
using Facebook.Bussines.Services;
using Facebook.Domains.Concrete;
using Facebook.Domains.Concrete.Ctx;
using Facebook.WebUI.SessionSetting;
using Facebook.WebUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private IUserService userService;
        private IUserFriendService userFriendService;
        private IMessageService messageService;
        private IWallService wallService;
        private IPostService postservice;
        private ICommentService commentService;
        public ProfileController()
        {
            userService = new UserService();
            userFriendService = new UserFriendService();
            messageService = new MessageService();
            wallService = new WallService();
            postservice = new PostService();
            commentService = new CommentService();


        }
        // GET: Profile
        string sessionname = SessionSet<Users>.CurrentUser("login").Username;
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult LiveSearch(string searchVal)
        {


            List<LiveSearchViewModel> usernames = userService.GetAll(x => x.Username.Contains(searchVal) && x.Username != User.Identity.Name).ToArray().Select(x => new LiveSearchViewModel(x)).ToList();


            return Json(usernames);
        }
       

        [HttpPost]
        public void AddFriend(string friend) //Sayfasına girdiğim kişinin username ni alıp gerekli işlemlerin yapıldığı alan
        {
           

            Users user = userService.GetBy(x => x.Username == sessionname);

            int userId = user.Id; //Login Olan Kişinin Idsi



            Users user2 = userService.GetBy(x => x.Username == friend); //Sayfasına girilen kişinin ıdsi yani furkan
            int friendId = user2.Id;


            UserFriend userFriend = new UserFriend()
            {
                UserId = userId,
                FriendId = friendId,
                Active = false,
                Engelle = false,
                IsDeleted = false
            };



            userFriendService.Add(userFriend);



        }



        public JsonResult DisplayFriendRequests()
        {

            Users userDto = userService.GetBy(x => x.Username == sessionname);
            int userId = userDto.Id;


            List<FriendRequestViewModel> list = userService.Query<UserFriend>().Where(x => x.FriendId == userId && x.Active == false).ToArray().Select(x => new FriendRequestViewModel(x)).ToList();

            List<Users> users = new List<Users>();


            foreach (var item in list)
            {
                var user = userService.Query<Users>().Where(x => x.Id == item.UserId).FirstOrDefault();
                users.Add(user);
            }

            return Json(users,JsonRequestBehavior.AllowGet);
        }

        public void AcceptFriendRequest(int friendid)
        {


            Users user = userService.GetBy(x => x.Username == sessionname);
            int userid = user.Id;

            UserFriend userFriend = userFriendService.GetBy(x => x.UserId == friendid && x.FriendId == userid);
            userFriend.Active = true;


            UserFriend newuserfriend = new UserFriend()
            {
                UserId = userid,
                FriendId = friendid,
                CreatedDate = DateTime.Now,
                Active = true,
                Engelle = false,
                IsDeleted = false,

            };

            userFriendService.Add(newuserfriend);

            userFriendService.Update(userFriend);



            
        }


        public void DeclineFrinedRequest(int friendid)
        {

           Users user= SessionSet<Users>.CurrentUser("login");


            UserFriend users= userFriendService.GetBy(x => x.FriendId == user.Id && x.UserId == friendid && x.Active==false);
            int userid = users.Id;

            userFriendService.Delete(userid);


            

        }



        public void SenMessage(string friend, string text)
        {
            //Sessiondaki kişi 
            Users user = userService.GetBy(x => x.Username == sessionname);
            int userId = user.Id;

            //Get mesaj atılacak kişi
            Users user2 = userService.GetBy(x => x.Username == friend);
            int userId2 = user2.Id;


            Messages m = new Messages();

            m.FromId = userId;
            m.ToId = userId2;
            m.message = text;
            m.Datasent = DateTime.Now;
            m.Read = false;

            messageService.Add(m);





        }


        public JsonResult DisplayUnreadMessages()
        {

            Users user = SessionSet<Users>.CurrentUser("login");

            //Okunmamış Mesaj Listesi
            List<MessagesViewModel> message = messageService.GetAll(x => x.ToId == user.Id && x.Read == false).ToArray().Select(x => new MessagesViewModel(x)).ToList();


            messageService.GetAll(x => x.ToId == user.Id && x.Read == false).ForEach(x => x.Read = true);
            FaceContext context = new FaceContext();
            context.SaveChanges();


            return Json(message);

        }


        public ActionResult AddPost(int id,string text)
        {

            //Wall wall = wallService.GetBy(x => x.UserId == id);

            //wall.Message = text;
            //wall.DateEdited = DateTime.Now;


            //wallService.Update(wall);
            var deleteuser = SessionSet<Users>.CurrentUser("login");
            Post po = new Post();

            po.Text = text;
            po.UserId = id;
            po.LikeCount = 0;
            po.IsDeleted = false;

            postservice.Add(po);

            List<PostListViewModel> postlist = (from use in userService.Query<Users>()
                                                join pos in userService.Query<Post>() on use.Id equals pos.UserId
                                                where use.Id == deleteuser.Id
                                                select new PostListViewModel
                                                {

                                                    username = use.Username,
                                                    Text = pos.Text,
                                                    LikeCount = pos.LikeCount,
                                                    UserId = use.Id,
                                                    CreatedDate = pos.CreatedDate,
                                                    postId = pos.Id




                                                }).ToList();


            //List<PostListViewModel> postlist2 = postservice.GetAll(x => allfriendsIds.Contains(x.UserId)).Select(x => new PostListViewModel(x)).ToList();


            var postlist2 = (from uf in userFriendService.Query<UserFriend>()
                             join p in userFriendService.Query<Post>() on uf.FriendId equals p.UserId
                             join use in userFriendService.Query<Users>() on p.UserId equals use.Id
                             where uf.UserId == deleteuser.Id && uf.Active == true
                             select new PostListViewModel
                             {

                                 Text = p.Text,
                                 CreatedDate = p.CreatedDate,
                                 username = use.Username,
                                 LikeCount = p.LikeCount,
                                 UserId = use.Id,
                                 postId = p.Id


                             }).ToList(); ;


            List<PostListViewModel> wallList = postlist.Concat(postlist2).OrderByDescending(x => x.CreatedDate).ToList();



            return PartialView("_AddPartial", wallList);

        }

        public ActionResult DeletPost(int postid)
        {

            Post post = postservice.GetBy(x => x.Id == postid);

           var deleteuser= SessionSet<Users>.CurrentUser("login");

            postservice.Delete(post.Id);



            List<PostListViewModel> postlist = (from use in userService.Query<Users>()
                                                join pos in userService.Query<Post>() on use.Id equals pos.UserId
                                                where use.Id == deleteuser.Id
                                                select new PostListViewModel
                                                {

                                                    username = use.Username,
                                                    Text = pos.Text,
                                                    LikeCount = pos.LikeCount,
                                                    UserId = use.Id,
                                                    CreatedDate = pos.CreatedDate,
                                                    postId = pos.Id




                                                }).ToList();


            //List<PostListViewModel> postlist2 = postservice.GetAll(x => allfriendsIds.Contains(x.UserId)).Select(x => new PostListViewModel(x)).ToList();


            var postlist2 = (from uf in userFriendService.Query<UserFriend>()
                             join p in userFriendService.Query<Post>() on uf.FriendId equals p.UserId
                             join use in userFriendService.Query<Users>() on p.UserId equals use.Id
                             where uf.UserId == deleteuser.Id && uf.Active == true
                             select new PostListViewModel
                             {

                                 Text = p.Text,
                                 CreatedDate = p.CreatedDate,
                                 username = use.Username,
                                 LikeCount = p.LikeCount,
                                 UserId = use.Id,
                                 postId = p.Id


                             }).ToList(); ;


            List<PostListViewModel> wallList = postlist.Concat(postlist2).ToList();



            return PartialView("_DeletePartial",wallList);
        }



        public ActionResult ShowComments(int id)
        {

            var model = (from po in userService.Query<Post>()
                         join
                          com in userService.Query<Comment>() on po.Id equals com.PostId
                         join use in userService.Query<Users>() on com.UserId equals use.Id
                         where po.Id == id
                         select new CommentListViewModel
                         {
                             PostId = com.PostId,
                             ComText = com.Body,
                             username = use.Username,
                             CreatedDate = com.CreatedDate,
                             UserId = com.UserId,
                             CommentId=com.Id
                         }

                       ).ToList();

            return View("_PartialComment",model);
        }


        public ActionResult UpdateComment(int? id ,string text)
        {

            Comment com = commentService.GetBy(x => x.Id == id);

            com.Body = text;

            commentService.Update(com);
            



            return Json(new { result=true},JsonRequestBehavior.AllowGet);
        }


        public void DeleteComment(int id)
        {

            Comment com = commentService.GetBy(x => x.Id == id);

            if (com!=null)
            {
                commentService.Delete(com.Id);
            }

        }

        public void AddComment(string text,int id)
        {
            var user = SessionSet<Users>.CurrentUser("login");

            Comment com = new Comment()
            {
                Body = text,
                UserId = user.Id,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                PostId = id


            };
            commentService.Add(com);

        }


    }
}