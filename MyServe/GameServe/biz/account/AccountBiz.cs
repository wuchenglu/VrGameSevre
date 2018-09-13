using ControlConterServe.cache;
using ControlConterServe.cache.accaount;

namespace ControlConterServe.biz.accaount
{
    public class AccountBiz:IAccountBiz
    {
        private IAccountCache accountCache = cacheFactory.AccaountCache;
        public int Login(NetFrame.UserToken token, string account)
        {
            //if (account.Length < 3) return -1;//账号密码格式错误

            accountCache.OnLine(token,account); return 1;//登录成功
        }
        public void Close(NetFrame.UserToken token)
        {
            accountCache.OffLine(token);
        }
        public string GetAccountId(NetFrame.UserToken token)
        {
            return accountCache.GetAccountId(token);
        }
    }
}
