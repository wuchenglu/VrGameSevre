using NetFrame;

namespace ControlConterServe.cache.accaount
{
    public interface IAccountCache
    {
        /// <summary>
        /// 是否已有此账号
        /// </summary>
        /// <returns></returns>
        bool HasAccaount(string account);
        /// <summary>
        /// 是否在线
        /// </summary>
        /// <returns></returns>
        bool IsOnline(string account);
        /// <summary>
        /// 上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        void OnLine(UserToken token, string account);
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        void OffLine(UserToken token);
        /// <summary>
        /// 获取链接对象Id
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetAccountId(UserToken token);
        /// <summary>
        /// 通过账号获取token
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        UserToken GeTokenById(string account);

    }
}
