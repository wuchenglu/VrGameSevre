using System.Collections.Generic;
using NetFrame;

namespace ControlConterServe.cache.accaount
{
    public class AccountCache : IAccountCache 
    {
        public int index = 1;
        public Dictionary<UserToken, string> OnLineDictionary = new Dictionary<UserToken, string>();

        public Dictionary<string, UserToken> IdTokenCache = new Dictionary<string, UserToken>();

        public Dictionary<string, bool> userIsOnLine = new Dictionary<string, bool>();

        /// <summary>
        /// 查找是否有此账号
        /// </summary>
        /// <param name="accaount"></param>
        /// <returns></returns>
        public bool HasAccaount(string accaount)
        {
            return IdTokenCache.ContainsKey(accaount);
        }
        /// <summary>
        /// 账号是否在线
        /// </summary>
        /// <param name="accaount"></param>
        /// <returns></returns>
        public bool IsOnline(string account)
        {
            if (userIsOnLine.ContainsKey(account))
                return userIsOnLine[account];
            return false;

        }
        /// <summary>
        /// 账号上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accaount"></param>
        public void OnLine(NetFrame.UserToken token, string account)
        {
            OnLineDictionary.Add(token, account);

            if (IdTokenCache.ContainsKey(account))
            {
                IdTokenCache[account] = token;
            }
            else
            {
                IdTokenCache.Add(account, token);
            }

            if (userIsOnLine.ContainsKey(account))
            {
                userIsOnLine[account] = true;
            }
            else
            {
                userIsOnLine.Add(account, true);
            }
        }
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        public void OffLine(NetFrame.UserToken token)
        {
            string account="";

            if (OnLineDictionary.ContainsKey(token))
            {
                account = OnLineDictionary[token];
                OnLineDictionary.Remove(token);
            }
            if (IdTokenCache.ContainsKey(account))
            {
                IdTokenCache.Remove(account);
            }
            if (userIsOnLine.ContainsKey(account))
            {
                userIsOnLine[account] = false;
            }
            else
            {
                userIsOnLine.Add(account, false);
            }
        }
        /// <summary>
        /// 获取链接对象id
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetAccountId(NetFrame.UserToken token)
        {
            if (!OnLineDictionary.ContainsKey(token)) return "-1";//没有此用户上线获取不到id
            return OnLineDictionary[token];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserToken GeTokenById(string account)
        {
            if (IdTokenCache.ContainsKey(account))
            {
                return IdTokenCache[account];
            }
            return null;
        }
    }
}
