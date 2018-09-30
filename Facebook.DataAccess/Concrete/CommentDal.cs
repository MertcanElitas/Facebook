using Facebook.DataAccess.Abstract;
using Facebook.Domains.Concrete;
using Facebook.Domains.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.DataAccess.Concrete
{
   public class CommentDal:EfEntityRepositoryBase<Comment>,ICommentDal
    {

       
    }
}
