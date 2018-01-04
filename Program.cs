using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperWebSocket;
using SuperSocket.SocketBase.Config;
using Newtonsoft.Json;
namespace ControlServer
{

    public enum UserType
    {
        Teacher=0,
        Student=1,
    }
    

    public class UserData
    {
        [JsonIgnore]
        public  WebSocketSession session;
        public UserType userType;
        public string netId;
        public string bindId;
    }
    public class ProtocolKey
    {
        public const int MsgType_Heart = -1;
        public const int MsgType_Login = 0;
        
        public const int MsgType_ChangeScene = 1;
        public const int MsgType_Shutdown = 2;
        public const int MsgType_DownloadPercent = 3;
        public const int MsgType_DownloadOver = 4;
    }
    public class MessageData
    {
        public MessageData()
        {

        }
        public MessageData(int type,string body)
        {
            t = type;
            b = body;
        }
        public int t;
        public string b;
    }

    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer server=null;
            try
            {
                int port = 3001;
                 server= new WebSocketServer();
                server.NewMessageReceived += server_NewMessageReceived;
                server.NewSessionConnected += server_NewSessionConnected;
                server.SessionClosed += server_SessionClosed;
                var config = new ServerConfig();
                config.Port = port;
                server.Setup(config, null, null, new SuperSocket.SocketBase.Logging.ConsoleLogFactory(), null, null);
                server.Start();
            }
            catch(Exception e)
            {

            }
            string msg=Console.ReadLine();
            while(msg!="exit")
            {
                string[] command = msg.Split(new char[]{ ':'});
                if(command[0]=="app")
                {
                    var sessions=server.GetAllSessions();
                    foreach(var s in sessions)
                    {
                        s.Send(JsonConvert.SerializeObject(new MessageData()));
                    }
                }
                msg = Console.ReadLine();
            }
        }

        static void server_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason reason)
        {
            Util.LOG("sessionClosed");
        }

        static void server_NewMessageReceived(WebSocketSession session, string msg)
        {
            Util.LOG("new message received"+msg);
            try
            {
                MessageData data = JsonConvert.DeserializeObject<MessageData>(msg);
                Util.LOG("t:"+data.t+" b:"+data.b);
            }catch(Exception e)
            {
                Util.LOGError("wrong msg struct" + e.Message);
            }
           
        }

        static void server_NewSessionConnected(WebSocketSession session)
        {
            Util.LOG("new user connect to server");
        }
    }
}
