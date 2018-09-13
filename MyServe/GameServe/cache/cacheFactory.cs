using ControlConterServe.cache.accaount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ControlConterServe.cache
{
    public class cacheFactory
    {
        public static readonly IAccountCache AccaountCache;

        static cacheFactory()
        {
            AccaountCache = new AccountCache();
        }
    }
}
