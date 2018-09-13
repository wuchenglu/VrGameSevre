using NetFrame;

namespace ControlConterServe
{
    interface HandlerInterface
    {
        void ClientClose(UserToken token, string error);

        //   void ClientConnect(UserToken token);

        void MessageReceive(UserToken token, string message);
    }
}
