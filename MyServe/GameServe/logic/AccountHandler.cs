using ControlConterServe.biz;
using ControlConterServe.biz.accaount;
using ControlConterServe.tool;
using NetFrame;

namespace ControlConterServe.logic
{
    public class AccountHandler : AbsOnceHandler, HandlerInterface
    {
        private IAccountBiz accaount = BizFactory.accountBiz;
        public void ClientClose(UserToken token, string error)
        {
            ExecutorPool.Instance.Executor(
                    delegate ()
                    {
                        accaount.Close(token);
                    }
                    );
        }
        public void MessageReceive(UserToken token, string message)
        {
            switch (message)
            {
                case "":
                    accaount.Login(token, "1");
                    break;
                case "  ":
                    accaount.Login(token, "2");
                    break;
                case " ":
                    Login();
                    break;
            }
        }
        void Login()
        {
            ExecutorPool.Instance.Executor(delegate
            {
                Write("1", "123456");
            });
        }
    }
}
