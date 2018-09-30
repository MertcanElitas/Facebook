using Facebook.Domains.Concrete;
using Facebook.Domains.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.DataAccess.Abstract
{
    public interface IMessageDal:IEntityRepository<Messages>
    {
    }
}
