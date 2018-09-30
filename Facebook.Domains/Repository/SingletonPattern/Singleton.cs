using Facebook.Domains.Concrete.Ctx;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Repository.SingletonPattern
{
    public static class Singleton
    {
        public static FaceContext _context { get; set; }

        public static object lobj = new object();




        public static DbContext CreateContex()
        {

            if (_context==null)
            {
                lock (lobj)
                {
                    if (_context == null)
                    {
                        return _context = new FaceContext();
                    }
                }
            }

            return _context;
            
        }

    }
}
