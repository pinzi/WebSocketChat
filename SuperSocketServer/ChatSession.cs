using SuperSocket.SocketBase;

namespace SuperSocketServer
{
    public class ChatSession : AppSession<ChatSession>
    {
        protected override void OnSessionStarted()
        {
            this.Send("Welcome to SuperSocket Telnet Server");
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            this.Send("Bye-Bye");
        }
    }
}
