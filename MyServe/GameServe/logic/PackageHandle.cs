using ControlConterServe.biz;
using ControlConterServe.biz.accaount;
using ControlConterServe.tool;
using NetFrame;

namespace ControlConterServe.logic
{
    public class PackageHandle: AbsMulitHandler,HandlerInterface
    {
        private IAccountBiz accaount = BizFactory.accountBiz;
        public void ClientClose(UserToken token, string error)
        {
            ExecutorPool.Instance.Executor(
                    delegate ()
                    {
                        accaount.Close(token);
                        Leave(token);
                    }
                    );
        }
        public void MessageReceive(UserToken token, string message)
        {
            switch (message)
            {
                case "":
                    accaount.Login(token, "1");
                    Enter(token);
                    break;
                case " ":
                    accaount.Login(token, "2");
                    Enter(token);
                    break;
                case "  ":
                    Send();
                    break;
            }
        }
        void Send()
        {
            ExecutorPool.Instance.Executor(delegate
            {
                Brocast("123456");
            });
        }
    }
}
