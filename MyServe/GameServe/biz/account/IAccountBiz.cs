using NetFrame;

namespace ControlConterServe.biz.accaount
{
    public interface IAccountBiz
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accaount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int Login(UserToken token, string account);
        /// <summary>
        /// 用户断开
        /// </summary>
        /// <param name="token"></param>
        void Close(UserToken token);
        /// <summary>
        /// 获取链接对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetAccountId(UserToken token);
    }
}
