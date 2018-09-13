using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using NetFrame.Auto;

namespace ControlConterServe.logic
{
    public class AbsMulitHandler
    {
        List<UserToken> tokens = new List<UserToken>();
        public bool Enter(UserToken token)
        {
            if (tokens.Contains(token))
            {
                return false;
            }
            tokens.Add(token);
            return true;
        }

        public bool IsEnter(UserToken token)
        {
            if (tokens.Contains(token))
            {
                return true;
            }
            return false;
        }

        public bool Leave(UserToken token)
        {
            if (tokens.Contains(token))
            {
                tokens.Remove(token);
                return true;
            }
            return false;
        }
        public void Brocast(string message)
        {
            byte[] value = Encoding.UTF8.GetBytes(message);
            int itemLenth = value.Length;
            foreach (UserToken item in tokens)
            {
                if (item != null)
                {
                    byte[] bs = new byte[itemLenth];
                    Array.Copy(value, 0, bs, 0, itemLenth);
                    item.write(bs);
                }
            }
        }
    }
}
