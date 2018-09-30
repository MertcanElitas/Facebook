using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Concrete.Ctx
{
    public class FaceContext:DbContext
    {


        public FaceContext():base("FaceContext")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FaceContext>(null);
            modelBuilder.Entity<UserFriend>()
                .HasRequired(x => x.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserFriend>()
               .HasRequired(x => x.Friend)
               .WithMany()
               .HasForeignKey(a => a.FriendId)
               .WillCascadeOnDelete(false);



            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Users> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Likes> Likes { get; set; }

        public DbSet<Messages> Messages { get; set; }


        public DbSet<Online> Onlines { get; set; }

        public DbSet<Wall> Walls { get; set; }


        public DbSet<UserFriend> UserFriends { get; set; }

        public DbSet<Post> Posts { get; set; }       


    }
}
