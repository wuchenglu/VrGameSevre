using System.Text;
using NetFrame;
using ControlConterServe.cache.accaount;
using ControlConterServe.cache;

namespace ControlConterServe.logic
{
    public class AbsOnceHandler
    {
        public IAccountCache userCache = cacheFactory.AccaountCache;
        /// <summary>
        /// 通过用户ID获取连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserToken getToken(string id)
        {
            return userCache.GeTokenById(id);
        }

        #region 通过链接对象发送
        public void Write(UserToken token, string message)
        {
            byte[] send = Encoding.UTF8.GetBytes(message);
            token.write(send);
        }
        #endregion
        #region 通过ID发送
        public void Write(string id, string message)
        {
            UserToken token = getToken(id);
            if (token == null) return;
            Write(token, message);
        }



        #endregion

    }
}
